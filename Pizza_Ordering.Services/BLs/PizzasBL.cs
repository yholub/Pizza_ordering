using Pizza_Ordering.Common;
using Pizza_Ordering.DataProvider.UnitOfWork;
using Pizza_Ordering.Domain.Entities;
using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.BLs
{
    public class PizzasBL : BaseBL, IPizzasBL
    {
        public PizzasBL(IUnitOfWorkFactory factory)
            : base(factory)
        {
        }

        public List<PizzaDto> GetFixPizzas()
        {
            return UseDb(uow =>
            {
                var dtos = uow.FixPizzas.Query().Select(x => new PizzaDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    PizzaType = PizzaType.Fix,
                    Ingredients = x.IngredientItems.Select(i => new IngredientDto
                    {
                        Id = i.Ingredient.Id,
                        Name = i.Ingredient.Name,
                        Price = i.Ingredient.Price,
                        Weight = i.Ingredient.Weight,
                        Quantity = i.Quantity
                    }).ToList()
                }).ToList();

                return dtos;
            });
        }

        public List<PizzaDto> GetSavedPizzas(long userId)
        {
            return UseDb(uow =>
            {
                var dtos = uow.SavedPizzas.Query()
                .Where(x => x.UserId == userId)
                .Select(x => new PizzaDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.BasePizza.Price > x.IngredientItems.Sum(i => i.Quantity * i.Ingredient.Price)
                                ? x.BasePizza.Price
                                : x.IngredientItems.Sum(i => i.Quantity * i.Ingredient.Price),
                    PizzaType = PizzaType.Saved,
                    Ingredients = x.IngredientItems.Select(i => new IngredientDto
                    {
                        Id = i.Ingredient.Id,
                        Name = i.Ingredient.Name,
                        Price = i.Ingredient.Price,
                        Weight = i.Ingredient.Weight,
                        Quantity = i.Quantity
                    }).ToList()
                }).ToList();

                return dtos;
            });
        }

        public PizzaDto GetPizzaById(PizzaType pizzaType, long pizzaId)
        {
            return UseDb(uow =>
            {
                if (pizzaType == PizzaType.Fix)
                {
                    var entity = uow.FixPizzas.GetById(pizzaId);

                    var dto = new PizzaDto
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Price = entity.Price,
                        PizzaType = PizzaType.Fix,
                        Ingredients = entity.IngredientItems.Select(i => new IngredientDto
                        {
                            Id = i.Ingredient.Id,
                            Name = i.Ingredient.Name,
                            Price = i.Ingredient.Price,
                            Weight = i.Ingredient.Weight,
                            Quantity = i.Quantity
                        }).ToList()
                    };

                    return dto;
                }
                else if (pizzaType == PizzaType.Saved)
                {
                    var entity = uow.SavedPizzas.GetById(pizzaId);

                    var dto = new PizzaDto
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Price = entity.BasePizza.Price > entity.IngredientItems.Sum(i => i.Quantity * i.Ingredient.Price)
                                ? entity.BasePizza.Price
                                : entity.IngredientItems.Sum(i => i.Quantity * i.Ingredient.Price),
                        PizzaType = PizzaType.Saved,
                        Ingredients = entity.IngredientItems.Select(i => new IngredientDto
                        {
                            Id = i.Ingredient.Id,
                            Name = i.Ingredient.Name,
                            Price = i.Ingredient.Price,
                            Weight = i.Ingredient.Weight,
                            Quantity = i.Quantity
                        }).ToList()
                    };

                    return dto;
                }
                else if (pizzaType == PizzaType.Modified)
                {
                    var entity = uow.ModifiedPizzas.GetById(pizzaId);

                    decimal price = entity.BasePizza.Price;
                    // add price for each extra ingredient
                    var basePizzaIngredients = entity.BasePizza.IngredientItems.ToList();
                    foreach (var thisIngr in entity.IngredientItems)
                    {
                        var baseIngr = basePizzaIngredients.FirstOrDefault(x => x.IngredientId == thisIngr.IngredientId);
                        int thisCount = thisIngr.Quantity;
                        int baseCount = baseIngr?.Quantity ?? 0;
                        int extraIngredientsCount = thisCount > baseCount
                            ? thisCount - baseCount
                            : 0;

                        price += thisIngr.Ingredient.Price * extraIngredientsCount;
                    }

                    var dto = new PizzaDto
                    {
                        Id = entity.Id,
                        BasePizzaId = entity.FixPizzaId,
                        // Here should be entity.UserId which is not in the database yet
                        // UserId =
                        Name = $"{entity.BasePizza} (змінена)",
                        Price = price,
                        PizzaType = PizzaType.Modified,
                        Ingredients = entity.IngredientItems.Select(i => new IngredientDto
                        {
                            Id = i.Ingredient.Id,
                            Name = i.Ingredient.Name,
                            Price = i.Ingredient.Price,
                            Weight = i.Ingredient.Weight,
                            Quantity = i.Quantity
                        }).ToList()
                    };

                    return dto;
                }

                return null;
            });
        }

        public long CreateFixPizza(PizzaDto pizzaDto)
        {
            return UseDb(uow =>
            {
                var entity = new FixPizza
                {
                    Name = pizzaDto.Name,
                    Price = pizzaDto.Price,
                    IngredientItems = pizzaDto.Ingredients
                        .Select(x => new IngredientItem
                        {
                            IngredientId = x.Id,
                            Quantity = x.Quantity
                        })
                        .ToList()
                };

                uow.FixPizzas.Create(entity);

                uow.Save();

                return entity.Id;
            });
        }

        public long CreateModifiedPizza(PizzaDto pizzaDto)
        {
            return UseDb(uow =>
            {
                var ingredientItems = pizzaDto.Ingredients.Select(x => new IngredientItem
                {
                    IngredientId = x.Id,
                    Quantity = x.Quantity
                }).ToList();

                foreach (var item in ingredientItems)
                {
                    uow.IngredientItems.Create(item);
                }

                uow.Save();

                var entity = new ModifiedPizza
                {
                    FixPizzaId = pizzaDto.BasePizzaId,
                    IngredientItems = ingredientItems
                };

                uow.ModifiedPizzas.Create(entity);

                uow.Save();

                return entity.Id;
            });
        }

        public long CreateSavedPizza(long userId, PizzaDto pizzaDto)
        {
            return UseDb(uow =>
            {
                var ingredientItems = pizzaDto.Ingredients.Select(x => new IngredientItem
                {
                    IngredientId = x.Id,
                    Quantity = x.Quantity
                }).ToList();

                foreach (var item in ingredientItems)
                {
                    uow.IngredientItems.Create(item);
                }

                uow.Save();

                var entity = new SavedPizza
                {
                    FixPizzaId = pizzaDto.BasePizzaId,
                    UserId = userId,
                    Name = pizzaDto.Name,
                    IngredientItems = ingredientItems,
                };

                uow.SavedPizzas.Create(entity);

                uow.Save();

                return entity.Id;
            });
        }

        public void DeleteFixPizza(long pizzaId)
        {
            UseDb(uow =>
            {
                uow.FixPizzas.Delete(pizzaId);

                uow.Save();
            });
        }
    }
}
