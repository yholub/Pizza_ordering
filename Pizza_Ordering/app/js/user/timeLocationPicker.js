var timeAndLoc = (function ($) {
    var icons = [
      'http://maps.google.com/mapfiles/markerA.png',
      'http://maps.google.com/mapfiles/markerB.png',
      'http://maps.google.com/mapfiles/markerC.png'
    ];

    var model = null;
    var lastData = null;

    return {
        init: init,
        initMap: initMap
    };

  
    function init() {
        var locInfo = $.get("/api/pizza")
        
        $.when(locInfo)
            .then(function (data) {
                lastData = data;
                model = new TimeLocViewModel(data);
                $("#gmap").on("change", "select", function () {
                    var val = parseInt($(this).val());
                    if (val > 0) {
                        model.selectedHouse(model.houses[val - 1]);
                    }
                    else {
                        model.selectedHouse(null);
                    }
                });
                $('#timepicker').timepicker({
                    altField: "#timepickerInput"
                });

                ko.applyBindings(model,
                    document.getElementById("timeAndLocView"));

               
            });

    }

    function TimeLocViewModel(locs) {
        this.houses = locs;
        this.houses.forEach(function (el, i) {
            el.index = i;
        });

        this.selectedHouse = ko.observable(null);
        this.showMap = ko.observable(false);
        this.map = null;

        this.loaded = ko.observable(window.google === undefined || window.google === null);

        var self = this;
        this.toggle = function () {
            if (!self.map) {
                self.map = createMapPlace(self.houses);
            }

            //self.showMap(!self.showMap());
            if (self.showMap()) {
                self.map.Load();
                if (self.selectedHouse()) {
                    self.map.ViewOnMap(self.selectedHouse().index + 1);
                } else {
                    self.map.ViewOnMap(0);
                }
            }
                

           
        }
    }

  
    function createMapPlace(locs) {
        return new Maplace({
            locations: $.map(locs, function (el, i) {
                return {
                    lat: el.Location.Lat,
                    lon: el.Location.Lon,
                    zoom: 15,
                    title: el.Location.StreetName + ' ' + el.Location.HouseNumber,
                    html: '<h5>' + el.Location.StreetName + ' ' + el.Location.HouseNumber + '</h5>',
                    icon: icons[i]

                };

            }),
            map_div: '#gmap',
            controls_on_map: true,
            controls_type: 'dropdown',
            force_generate_controls: true,
            controls_applycss: true,
            controls_title: 'Choose a location:',
        });
    }

    function initMap() {
        if(model)
            model.loaded(true);
    }


})(jQuery);