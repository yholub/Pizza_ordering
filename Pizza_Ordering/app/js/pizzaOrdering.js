$(function () {

    var viewModel = {
        pizzas: [
           { imgUrl: "../../assets/images/img1.png", name: "pizza 1" },
           { imgUrl: "../../assets/images/food_icon02.jpg", name: "pizza 2" },
           { imgUrl: "../../assets/images/food_icon03.jpg", name: "pizza 3" },
           { imgUrl: "../../assets/images/food_icon04.jpg", name: "pizza 4" },
           { imgUrl: "../../assets/images/food_icon05.jpg", name: "pizza 5" },
           { imgUrl: "../../assets/images/food_icon06.jpg", name: "pizza 6" },
           { imgUrl: "../../assets/images/food_icon07.jpg", name: "pizza 7" }
        ],
        selectedProducts: [
           { imgUrl: "../../assets/images/food_icon01.jpg", name: "pizza 1" },
           { imgUrl: "../../assets/images/food_icon02.jpg", name: "pizza 2" },
           { imgUrl: "../../assets/images/food_icon03.jpg", name: "pizza 3" },
           { imgUrl: "../../assets/images/food_icon04.jpg", name: "pizza 4" },
           { imgUrl: "../../assets/images/food_icon05.jpg", name: "pizza 5" }
        ],
        productCount: 5,
        addToCard: function (data, event) {
            alert(event.target.id);
        }

    }



    ko.applyBindings(viewModel);

});


