$.sammy("#main", function() {
    
    this.use('Handlebars', 'html');
    this.get('#/', function() {
        var self = this;
        
        $.get('/api/Order/GetCurrent')
            .then(function (data) {
                var list = [];
                var now = new Date();
                var stHour = now.getHours();
                var stMin = now.getMinutes();
                if (stHour < 9) {
                    stHour = 9;
                }
                for (var i = stHour; i < 21; ++i) {
                    for (var j = 0; j < 60; j = j + 10) {
                        var jText = '' + j;
                        if (j < 10) {
                            jText = '0' + jText;
                        }
                        list.push('' + i + ':' + jText);
                    }
                }
                self.render('orders.html', { hours: list }).replace("#main").then(function () {
                   
                    $('#calendar').height((1260 - stHour * 60) * 3);
                    var layOutDay = function (cal_events) {
                        $('#calendar').addEvents(cal_events);
                    };
                  
                    $('#calendar').layOutDay({
                        'calendar_start': stHour * 60,
                        'calendar_end': 1260,
                        'time_selector': 'body .time'
                    });
                   
                    var list = $.map(data, function (d) {
                        return {
                            event: {
                                dateStart: new Date(now.getYear(), /* month */ now.getMonth(),  /* day */ now.getDay(), /* hour */ d.StHour, /* minute */ d.StMinute, 0, 0),
                                dateEnd: new Date(now.getYear(), /* month */ now.getMonth(),  /* day */ now.getDay(), /* hour */ d.EndHour, /* minute */ d.EndMinute, 0, 0),
                                title: d.Name,
                                description: "Price: " + d.Price + " Start: " + d.StartStr + " End: " + d.EndStr,
                                ordId: d.Id
                            }
                        }
                    });
                  

                    layOutDay(list);


                    function OrderViewModel(data) {
                        this.dict = {};
                        var self = this;
                        this.selected= ko.observable(data[0]);
                        data.forEach(function (el) {
                            self.dict[el.Id] = el;
                        });

                        this.changeSelected = function(id) {
                            self.selected(self.dict[id]);
                        }

                        this.accept = function (id) {
                            $.post('api/order/accept/' + id);
                        }

                        this.reject = function (id) {
                            $.post('api/order/reject' + id);
                        }
                    }

                    var model = new OrderViewModel(data);
                    $('.event[data-order-id="' + data[0].Id +'"]').addClass('active');
                    ko.applyBindings(model, $("#main").get(0));

                    $(".event").click(function () {
                        $(".event.active").removeClass('active');
                        $(this).addClass('active');
                        model.changeSelected($(this).attr("data-order-id"));
                    });
                 
            });
        
            
        });

    });

  

  

}).run();