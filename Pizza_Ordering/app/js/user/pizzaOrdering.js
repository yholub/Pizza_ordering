var pizzaOrdering = (function ($) {
    return {
        init: init,
        getPizzas: getPizzas,
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
                        ingredients.push(pizza.Ingredients[j].Name);
                    }

                    pizzasArr.push({ id: pizza.Id, imgUrl: "../assets/images/" + pizza.Name + ".jpg", name: pizza.Name, price: pizza.Price, ingredients: ingredients });
                }

                return pizzasArr;
            });

        return promise;

       
    }

    function ViewModel(pizzas) {
        var self = this;

        self.selectedPizzaCount = 0;
        self.pizzas = pizzas;

        self.selectedProducts = ko.observableArray([]);
        self.addToCard = function (data, event) {
            var pizzaId = Number(event.target.id);
            var pizza = self.pizzas[pizzaId];
            self.selectedProducts.push({ id: self.selectedPizzaCount, name: pizza.name, price: pizza.price, ingredients: pizza.ingredients });
            self.selectedPizzaCount += 1;
        };

        self.deletePizzaItem = function (data, event) {
            self.selectedProducts.remove(data);
            console.log(data);
        }
    }

    function init() {
        getPizzas().then(function (pizzas) {
            ko.applyBindings(new ViewModel(pizzas), document.getElementById('pizzaOrderView'));
        });
        
    }
    
  
})(jQuery);


