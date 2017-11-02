var pizzaOrdering = (function ($) {
    return {
        init: init,
        getPizzas: getPizzas,
        getIngredients: getIngredients,
        ViewModel: ViewModel
    };


    function getPizzas() {
        
        var promise = 
            $.get("../../../api/pizzas/fix")
            .then(function(data) {
                var pizzasArr = [];
                for (var i = 0; i < data.length; i++) {
                    var pizza = data[i];
                    var ingredients = [];
                    for (var j = 0; j < pizza.Ingredients.length; j++) {
                        ingredients.push({name: pizza.Ingredients[j].Name, count: 1});
                    }

                    pizzasArr.push({ id: pizza.Id, imgUrl: "../assets/images/" + pizza.Name + ".jpg", name: pizza.Name, price: pizza.Price, ingredients: ingredients });
                }

                return pizzasArr;
            });

        return promise;
    }


    function getIngredients() {

        var promise =
            $.get("../../../api/ingredients")
            .then(function (data) {
                var ingredientsArr = [];
                for (var i = 0; i < data.length; i++) {
                    var ingredient = data[i];
                    ingredientsArr.push({ id: ingredient.Id, imgUrl: "../assets/images/" + ingredient.Name + ".png", name: ingredient.Name, price: ko.observable(ingredient.Price), weight: ingredient.Weight, count: ko.observable(0), totalPrice: ko.observable(ingredient.Price), initCount: 0 });
                }

                return ingredientsArr;
            });

        return promise;
    }

    function ViewModel(pizzas, ingredientsArr) {
        var self = this;

        self.selectedPizzaCount = 0;
        self.pizzas = pizzas;
        self.allIngredients = ko.observableArray(ingredientsArr);


        self.order = function () {
            window.cacheOrders = $.map(self.selectedProducts(), function (el) { return {id: el.id}});
            location.href = "#/time";
        }

        self.selectedProducts = ko.observableArray([]);

        self.addToCard = function (data, event) {
            var pizzaId = Number(event.target.id);
            var pizza = self.pizzas[pizzaId];
            self.selectedProducts.push({ id: self.selectedPizzaCount, name: pizza.name, price: ko.observable(pizza.price), ingredients: ko.observable(pizza.ingredients), countOfPizzas: ko.observable(1) });
            self.selectedPizzaCount += 1;
        };

        self.showIngredients = function (selectedPizza) {
            console.log(selectedPizza.ingredients());
            return ko.computed(function () {
                var ingredientsInRightFormat = selectedPizza.ingredients().reduce(function (y, x) {
                    return ((y == "") ? y : y + ", ") + x.name + ((x.count > 1) ? "(" + x.count + ")" : "");
                }, "");

                return ingredientsInRightFormat;
            });
        };

        self.allIngredientsForPizza = ko.observableArray([]);

        self.totalPrice = ko.computed(function () {
            var total = self.selectedProducts().reduce(function (y, x) {
                return y + (x.price() * x.countOfPizzas());
            }, 0);

            return total;
        });

        self.deletePizzaItem = function (data, event) {
            if (data.countOfPizzas() <= 1)
                self.selectedProducts.remove(data);
            else {
                var newData = data;
                newData.countOfPizzas(newData.countOfPizzas() - 1)
                self.selectedProducts.replace(data, newData);
            }

            event.stopPropagation();

        }

        self.addPizzaItem = function (data, event) {
            console.log(data);
            if (data.countOfPizzas() <= 4) {
                var newData = data;
                newData.countOfPizzas(newData.countOfPizzas() + 1)
                self.selectedProducts.replace(data, newData);
            }

            event.stopPropagation();
            
        }


        self.totalCountForIngr = function (item) {
            self.allIngredients[item.id] =  Number(item.price) * Number(item.count);
        }

        self.ingredientsSetCount = function (data, event) {
            var selectedPizza = data;
            var ingredientWithCount = self.allIngredients();
            ingredientWithCount.forEach(function (ingredient) {
                if (selectedPizza.ingredients().some(e => e.name == ingredient.name)) {
                    ingredient.count(selectedPizza.ingredients().filter(x => x.name == ingredient.name)[0].count);
                    ingredient.initCount = 1;
                }
                else {
                    ingredient.count(0);
                }
            });

            self.allIngredientsForPizza(ingredientWithCount);
        }

        self.calculatePizzaPriceWithAdditionalIngredients = ko.computed(function () {
            var total = self.selectedProducts().reduce(function (y, x) {
                return y + (x.price() * x.countOfPizzas());
            }, 0);

            return total;
        });

        //Math.max(initCount, Number(count())) * Number(price())
    }

    function init() {
        getPizzas().then(function (pizzas) {
            getIngredients().then(function (ingredients) {
                ko.applyBindings(new ViewModel(pizzas, ingredients), document.getElementById('pizzaOrderView'));
            });
        });
        
    }
    
  
})(jQuery);


