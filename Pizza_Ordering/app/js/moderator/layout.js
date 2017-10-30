$.sammy("#main", function () {
    this._checkFormSubmission = function (form) {
        return false;
    };


    this.use('Handlebars', 'html');
    this.get('#/settings', function (context) {
        var self = context;
        
        $.get('/api/settings').then(function (data) {
            $("#sHref").addClass("navactive");
            $("#oHref").removeClass("navactive");
            self.render('settings.html').replace("#main").then(function() {
                
                function SettingsFormModel(data) {
                    this.start = ko.observable(data.StartHour);
                    this.end = ko.observable(data.EndHour);
                    this.cap = ko.observable(data.Capacity);
                    var list = $.map(data.Locked, function(el) {
                        return {
                            name: el.Name,
                            price: el.Price,
                            id: el.Id,
                            lock: ko.observable(el.IsLocked)
                        };
                    });

                    this.ing = list;

                    var self = this;
                    this.submit = function (data, event) {
                       
                        $.post("/api/settings", {
                            StartHour: self.start(),
                            EndHour: self.end(),
                            Capacity: self.cap(),
                            IngState: $.map(self.ing, function(el) {
                                return {
                                    Id: el.id,
                                    IsLocked: el.lock()
                                }
                            })
                        }).then(function() {
                            $.notify({
                                message: 'Налаштування оновлено',
                                type: 'success'
                            });
                        });
                    }
                }
                
                var model = new SettingsFormModel(data);
                ko.applyBindings(model, document.getElementById('setview'));
               
            });

        });




    });

   

    this.get('#/', function(context) {
        var self = context;
        $("#oHref").addClass("navactive");
        $("#sHref").removeClass("navactive");
        var getOrds = $.get('/api/Order/GetCurrent');
        var getSet = $.get('/api/settings');
        $.when(getOrds, getSet).done(function (concatData, concatSettings) {
            var data = concatData[0];
            var settings = concatSettings[0];
                var list = [];
                var now = new Date();
                var stHour = now.getHours();
                var stMin = now.getMinutes();
                if (stHour < settings.StartHour) {
                    stHour = settings.StartHour;
                }
                for (var i = stHour; i < settings.EndHour; ++i) {
                    for (var j = 0; j < 60; j = j + 10) {
                        var jText = '' + j;
                        if (j < 10) {
                            jText = '0' + jText;
                        }
                        list.push('' + i + ':' + jText);
                    }
                }
                
                self.partial('orders.html', { hours: list, cache: false }).then(function () {
                    
                    $('#calendar').height(((settings.EndHour - stHour) * 60) * 3);
                    
                    var layOutDay = function (cal_events) {
                        $('#calendar').addEvents(cal_events);
                    };
                  
                    $('#calendar').layOutDay({
                        'calendar_start': stHour * 60,
                        'calendar_end': settings.EndHour * 60,
                        'time_selector': 'body .time'
                    });
                   
                    var list = $.map(data, function (d) {
                        return {
                            event: {
                                dateStart: new Date(now.getYear(), /* month */ now.getMonth(),  /* day */ now.getDay(), /* hour */ d.StHour, /* minute */ d.StMinute, 0, 0),
                                dateEnd: new Date(now.getYear(), /* month */ now.getMonth(),  /* day */ now.getDay(), /* hour */ d.EndHour, /* minute */ d.EndMinute, 0, 0),
                                title: d.Name,
                                state: d.State,
                                description: "Price: " + d.Price + " Start: " + d.StartStr + " End: " + d.EndStr,
                                ordId: d.Id
                            }
                        }
                    });
                  

                    layOutDay(list);


                    function OrderViewModel(data) {
                        this.dict = {};
                        var self = this;
                        this.selected = ko.observable(null);
                        self.orders = ko.observableArray([]);
                        data.forEach(function (el) {
                            self.dict[el.Id] = el;
                            el.State = ko.observable(el.State);
                            self.orders.push(el);
                        });

                      

                        this.changeSelected = function(id) {
                            self.selected(self.dict[id]);
                        }

                        this.accept = function (el) {
                            $.post('/api/order/accept/' + el.OrderId);
                            $('.event[data-order-id="' + el.Id + '"]').addClass('accepted');
                            el.State(1);

                        }

                        this.acceptSelected = function () {
                            var el = self.selected();
                            $.post('/api/order/accept/' + el.OrderId);
                            $('.event[data-order-id="' + el.Id + '"]').addClass('accepted');
                            el.State(1);
                        }

                        this.reject = function (el) {
                            $.post('/api/order/reject/' + el.OrderId);
                            $('.event[data-order-id="' + el.Id + '"]').remove();
                            self.orders.remove(el);
                        }
                        this.rejectSelected = function () {
                            var el = self.selected();
                            $.post('/api/order/reject/' + el.OrderId);
                            $('.event[data-order-id="' + el.Id + '"]').remove();
                            self.orders.remove(el);
                            self.selected(null);
                        }
                    }

                    var model = new OrderViewModel(data);
                    ko.applyBindings(model, document.getElementById('view'));

                    $(".event").click(function () {
                        $(".event.active").removeClass('active');
                        $(this).addClass('active');
                        model.changeSelected($(this).attr("data-order-id"));
                    });
                 
            });
        
            
        });

    });


    


}).run("#/");


function isBound(id) {
    if (document.getElementById(id) != null)
        return !!ko.dataFor(document.getElementById(id));
    else
        return false;
}