$(function () {
    /*var pizzasArr = [
           { id: 0, imgUrl: "../../assets/images/food_icon03.jpg", name: "Основа", price: 20, ingredients: [] },
           { id: 1, imgUrl: "../../assets/images/img1.png",        name: "Цезаре", price: 120, ingredients: ["моцарела", "айсберг", "пармезан", "курка"] },
           { id: 2, imgUrl: "../../assets/images/food_icon02.jpg", name: "Вегетеріанська", price: 80, ingredients: ["пармезан", "рукола", "спаржа", "базилік"] },
           { id: 3, imgUrl: "../../assets/images/food_icon04.jpg", name: "Маргарита", price: 55, ingredients: ["моцарела", "базилік", "помідори"] },
           { id: 4, imgUrl: "../../assets/images/food_icon05.jpg", name: "Гавайська", price: 85, ingredients: ["моцарела", "ананаси", "кукурудза", "курка"] },
           { id: 5, imgUrl: "../../assets/images/food_icon06.jpg", name: "Українська", price: 110, ingredients: ["моцарела", "оливки", "кукурудза", "шинка", "гриби", "помідори"] },
           { id: 6, imgUrl: "../../assets/images/food_icon07.jpg", name: "Капрічоза", price: 95, ingredients: ["моцарела", "гриби", "оливки", "шинка"] }
    ];
    var viewModel = {
        pizzas: ko.observableArray(pizzasArr),
        selectedProducts: ko.observableArray([]),
        addToCard: function (data, event) {
            var pizzaId = event.target.id;
            console.log(pizzasArr);
            var pizza = pizzasArr[Number(pizzaId)];
            this.selectedProducts.push({ name: pizza.name, price: pizza.price, ingredients: pizza.ingredients });
        }


    };


    ko.applyBindings(viewModel);*/

    function ViewModel() {
        var self = this;

        self.pizzas = [
           { id: 0, imgUrl: "../../assets/images/food_icon01.jpg", name: "Основа", price: 20, ingredients: [] },
           { id: 1, imgUrl: "../../assets/images/zesare.jpeg", name: "Цезаре", price: 120, ingredients: ["моцарела", "айсберг", "пармезан", "курка"] },
           { id: 2, imgUrl: "../../assets/images/veg.jpg", name: "Вегетеріанська", price: 80, ingredients: ["пармезан", "рукола", "спаржа", "базилік"] },
           { id: 3, imgUrl: "../../assets/images/margarita.jpg", name: "Маргарита", price: 55, ingredients: ["моцарела", "базилік", "помідори"] },
           { id: 4, imgUrl: "../../assets/images/havai.jpg", name: "Гавайська", price: 85, ingredients: ["моцарела", "ананаси", "кукурудза", "курка"] },
           { id: 5, imgUrl: "../../assets/images/ukr.png", name: "Українська", price: 110, ingredients: ["моцарела", "оливки", "кукурудза", "шинка", "гриби", "помідори"] },
           { id: 6, imgUrl: "../../assets/images/capri.jpg", name: "Капрічоза", price: 95, ingredients: ["моцарела", "гриби", "оливки", "шинка"] }
        ];

        self.selectedProducts = ko.observableArray([]),
        self.addToCard = function (data, event) {
            var pizzaId = Number(event.target.id);
            var pizza = self.pizzas[pizzaId];
            self.selectedProducts.push({ name: pizza.name, price: pizza.price, ingredients: pizza.ingredients });
        }
    }

    ko.applyBindings(new ViewModel());

});


