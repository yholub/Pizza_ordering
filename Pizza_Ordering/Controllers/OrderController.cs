﻿using Pizza_Ordering.Models;
using Pizza_Ordering.Models.OrderViewModel;
using Pizza_Ordering.Services.BLs;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Models.Order;
using Pizza_Ordering.Common;

namespace Pizza_Ordering.Controllers
{
    public class OrderController : BaseController
    {
        private IOrderBL _orders;
        private IPizzasBL _pizzas;
        private IPizzaHouseBL _houses;

        public OrderController(IOrderBL service, IPizzaHouseBL houses, IPizzasBL pizzas)
        {
            _orders = service;
            _houses = houses;
            _pizzas = pizzas;
        }

        [Authorize(Roles = "Moderator")]
        [Route("api/order/GetCurrent")]
        [HttpGet]
        public IHttpActionResult GetCurrent()
        {
            long userId = this.User.Identity.GetUserId<long>();
            long houseId = _houses.GetPizzaHouses().First(h => h.ModeratorId == userId).Id;
            DateTime now = DateTime.Now;
            int min = DateTime.Now.Minute;
            DateTime start = now - TimeSpan.FromMinutes((min / 10 * 10) + 10);
            return Json(_orders.GetOrderItemsSince(start, houseId).Select(o => new OrderViewModel(o)).ToList());
        }

        [Authorize(Roles = "Moderator")]
        [Route("api/order/GetNew")]
        [HttpGet]
        public IHttpActionResult GetNew()
        {
            long userId = this.User.Identity.GetUserId<long>();
            long houseId = _houses.GetPizzaHouses().First(h => h.ModeratorId == userId).Id;
            DateTime now = DateTime.Now;
            int min = DateTime.Now.Minute;
            DateTime start = now - TimeSpan.FromMinutes((min / 10 * 10) + 10);
            return Json(_orders.GetOrderItemsSince(start, houseId, true).Select(o => new OrderViewModel(o)).ToList());
        }

        [Route("api/order/accept/{id:long}")]
        [HttpPost]
        public void Accept(int id)
        {
            _orders.Accept(id);
        }

        [Route("api/order/reject/{id:long}")]
        [HttpPost]
        public void Reject(int id)
        {
            _orders.Reject(id);
        }

        [Route("api/order/gettime")]
        [HttpPost]
        public Dictionary<long, PizzaHouseTimeViewModel> GetFreeTime(OrderBindingModel model)
        {
            Dictionary<long, PizzaHouseTimeViewModel> res = new Dictionary<long, PizzaHouseTimeViewModel>();
            int qty = model.OrderItems.Sum(o => o.Count);
            var houses = _houses.GetPizzaHouses();

            var checkIngs = model.OrderItems
               .SelectMany(o => o.Ingredients
                   .Select(i => new { Ing = i, Am = o.Count }))
               .GroupBy(i => i.Ing.Id)
               .Select(g => new
               {
                   Id = g.Key,
                   Count = g.Sum(i => i.Ing.Count * i.Am)
               })
               .Where(g => g.Count > 0);

            var filteredHouses = houses.Where(h =>
            {
                return checkIngs.All(i =>
                {
                    var found = h.InStock.FirstOrDefault(el => el.IngredientDto.Id == i.Id);
                    if (found == null)
                        return false;

                    return found.Quantity >= i.Count;
                });

            });

            DateTime now = DateTime.Now;
            int min = DateTime.Now.Minute;
            DateTime start = now - TimeSpan.FromMinutes((min / 10 * 10) + 40);

            foreach (var house in filteredHouses)
            {
                var settings = _houses.GetPizzaHouseById(house.Id);
                DateTime dayStart = DateTime.Today + TimeSpan.FromHours(settings.Open);
                DateTime endStart = DateTime.Today + TimeSpan.FromHours(settings.Close);

                if (endStart < dayStart)
                {
                    endStart = endStart + TimeSpan.FromDays(1);
                }
                TimeSpan step = TimeSpan.FromMinutes(5);
                TimeSpan interval = endStart - dayStart;

                int[] counts = new int[(int)Math.Round(interval.TotalMinutes / step.TotalMinutes)];

                PizzaHouseTimeViewModel resModel = new PizzaHouseTimeViewModel
                {
                    PizzaHouseId = house.Id
                };

                var orders = _orders.GetOrderItemsSince(start, house.Id, false);

                foreach (var order in orders)
                {
                    for (DateTime j = order.StartTime; j < order.EndTime; j += step)
                    {
                        counts[(int)Math.Round((j - dayStart).TotalMinutes / step.TotalMinutes)]++;
                    }
                }

                int k = 0;
                for (DateTime i = dayStart; i < endStart; i += step, k++)
                {
                    //4 * 5 == 20 minutes of preparation - make dynamic later
                    if (i >= start && CheckTimeArrBack(counts, k, settings.Capacity, qty, 4))
                    {
                        resModel.Set(i.Hour, i.Minute);
                    }
                }

                res[resModel.PizzaHouseId] = resModel;
            }

            return res;
        }

        private static bool CheckTimeArrBack(int[] arr, int ind, int cap, int qty, int dist)
        {
            if (ind - dist < 0)
                return false;

            for (int i = 1; i <= dist; ++i)
            {
                if (arr[ind - i] + qty > cap)
                    return false;
            }

            return true;
        }

        [Route("api/order/addNew")]
        [HttpGet]
        public IHttpActionResult AddNew()
        {
            _orders.CreateOrder(new Services.DTOs.OrderDto
            {
                PizzaHouseId = 1,
                Name = "something",
                Status = Common.PizzaStatusType.Processed,
                Price = 200,
                Items = new List<Services.DTOs.OrderItemDto> {
                    new OrderItemDto {
                        PizzaName = "something",
                        StartTime = DateTime.Today + TimeSpan.FromHours(20),
                        EndTime = DateTime.Today + TimeSpan.FromHours(20) + TimeSpan.FromMinutes(20),
                        Price = 200,
                        Status = Common.PizzaStatusType.Processed,
                        PizzaId = 2
                   }
                },

                TimeToTake = DateTime.Today + TimeSpan.FromHours(10) + TimeSpan.FromMinutes(20)
            });

            return Ok();
        }

        [Route("api/order")]
        [HttpPost]
        public IHttpActionResult Post(OrderCreateModel model)
        {
            var listOfOrderItemDtos = new List<OrderItemDto>(model.Items.Count);

            foreach (var x in model.Items)
            {
                var fixPizzaDto = _pizzas.GetPizzaById(PizzaType.Fix, x.PizzaId);

                bool isFixPizza = fixPizzaDto.Ingredients.All(fi => x.Ingredients.Any(mi => mi.Id == fi.Id && mi.Count == fi.Quantity));

                if (!isFixPizza)
                {
                    long modifiedPizzaId = _pizzas.CreateModifiedPizza(new PizzaDto
                    {
                        BasePizzaId = x.PizzaId,
                        UserId = UserId,
                        Ingredients = x.Ingredients.Select(i => new IngredientDto
                        {
                            Id = i.Id,
                            Quantity = i.Count
                        }).ToList(),
                        Name = x.Name
                    });

                    var modifiedPizzaDto = _pizzas.GetPizzaById(PizzaType.Modified, modifiedPizzaId);

                    var orderItemDto = new OrderItemDto
                    {
                        PizzaId = modifiedPizzaDto.Id,
                        PizzaName = modifiedPizzaDto.Name,
                        StartTime = model.TimeToTake - TimeSpan.FromMinutes(20),
                        EndTime = model.TimeToTake,
                        Price = modifiedPizzaDto.Price,
                        Status = Common.PizzaStatusType.Processed,
                        IsModified = true,
                        Quantity = x.Count
                    };

                    listOfOrderItemDtos.Add(orderItemDto);
                }
                else
                {
                    var orderItemDto = new OrderItemDto
                    {
                        PizzaId = fixPizzaDto.Id,
                        PizzaName = fixPizzaDto.Name,
                        StartTime = model.TimeToTake - TimeSpan.FromMinutes(20),
                        EndTime = model.TimeToTake,
                        Price = fixPizzaDto.Price,
                        Status = Common.PizzaStatusType.Processed,
                        IsModified = false,
                        Quantity = x.Count
                    };

                    listOfOrderItemDtos.Add(orderItemDto);
                }
            }

            _orders.CreateOrder(new Services.DTOs.OrderDto
            {
                UserId = UserId,
                // Price is set in the BL level
                PizzaHouseId = model.PizzaHouseId,
                TimeToTake = model.TimeToTake,
                Status = Common.PizzaStatusType.Processed,
                Items = listOfOrderItemDtos
            });

            return Ok();
        }
    }
}
