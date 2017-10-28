$(function () {

    function getPizzas() {
        var pizzasArr = [];
        $.get("../../../api/pizzas/fix", function (data) {
            
            for (var i = 0; i < data.length; i++) {
                var pizza = data[i];
                var ingredients = [];
                for (var j = 0; j < pizza.Ingredients.length; j++) {
                    ingredients.push(pizza.Ingredients[j].Name);
                }

                pizzasArr.push({ id: pizza.Id, imgUrl: "../../assets/images/" + pizza.Name + ".jpg", name: pizza.Name, price: pizza.Price, ingredients: ingredients });
            }

        });

        return pizzasArr;
    }

    function ViewModel() {
        var self = this;

        self.pizzas = getPizzas();

        /*self.pizzas = [
           { id: 0, imgUrl: "../../assets/images/food_icon01.jpg", name: "Основа", price: 20, ingredients: [] },
           { id: 1, imgUrl: "../../assets/images/zesare.jpeg", name: "Цезаре", price: 120, ingredients: ["моцарела", "айсберг", "пармезан", "курка"] },
           { id: 2, imgUrl: "../../assets/images/veg.jpg", name: "Вегетеріанська", price: 80, ingredients: ["пармезан", "рукола", "спаржа", "базилік"] },
           { id: 3, imgUrl: "../../assets/images/margarita.jpg", name: "Маргарита", price: 55, ingredients: ["моцарела", "базилік", "помідори"] },
           { id: 4, imgUrl: "../../assets/images/havai.jpg", name: "Гавайська", price: 85, ingredients: ["моцарела", "ананаси", "кукурудза", "курка"] },
           { id: 5, imgUrl: "../../assets/images/ukr.png", name: "Українська", price: 110, ingredients: ["моцарела", "оливки", "кукурудза", "шинка", "гриби", "помідори"] },
           { id: 6, imgUrl: "../../assets/images/capri.jpg", name: "Капрічоза", price: 95, ingredients: ["моцарела", "гриби", "оливки", "шинка"] }
        ];*/
        console.log(self.pizzas);


        self.selectedProducts = ko.observableArray([]),
        self.addToCard = function (data, event) {
            var pizzaId = Number(event.target.id);
            var pizza = self.pizzas[pizzaId];
            self.selectedProducts.push({ name: pizza.name, price: pizza.price, ingredients: pizza.ingredients });
        }
    }

    ko.applyBindings(new ViewModel());

});


