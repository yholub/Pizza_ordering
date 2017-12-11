var payment = (function ($) {
    return {
        init: init,
        ViewModel: ViewModel
    };

    function ViewModel(pizzas) {
        var self = this;
        
        self.cardNumber = ko.observable();
        self.cardCvc = ko.observable();
        self.cardExpMonth = ko.observable();
        self.cardExpYear = ko.observable();

        self.bonusesHas = ko.observable(500);
        self.bonusesToPay = ko.observable(750);
        self.moneyToPay = pizzas.items.totalPrice;

        self.submitBonusesPayment = function (e) {
            if (self.bonusesHas() >= self.bonusesToPay()) {
                // TODO
                createOrder();
            }
            else {
                failedToPay('У вас недостатньо бонусів =(');
            }
        }

        self.submitMoneyPayment = function (e) {
            //payWithStripe(e);
            createOrder();
        }

        function payWithStripe(e) {
            $form = $("money-payment-form");
            /* Visual feedback */
            $form.find('[type=submit]').html('Validating <i class="fa fa-spinner fa-pulse"></i>');

            var PublishableKey = 'pk_test_aJg3RiIHme0PcBL3abE0DqnL'; // Replace with your API publishable key
            Stripe.setPublishableKey(PublishableKey);

            /* Create token */
            var ccData = {
                number: self.cardNumber().replace(/\s/g, ''),
                cvc: self.cardCvc(),
                exp_month: self.cardExpMonth(),
                exp_year: self.cardExpYear()
            };

            Stripe.card.createToken(ccData, function stripeResponseHandler(status, response) {
                if (response.error) {
                    /* Visual feedback */
                    $form.find('[type=submit]').html('Try again');
                    /* Show Stripe errors on the form */
                    failedToPay(response.error.message);
                } else {
                    /* Visual feedback */
                    $form.find('[type=submit]').html('Processing <i class="fa fa-spinner fa-pulse"></i>');
                    /* Hide Stripe errors on the form */
                    $form.find('.payment-errors').closest('.row').hide();
                    $form.find('.payment-errors').text("");
                    // response contains id and card, which contains additional card details            
                    console.log(response.id);
                    console.log(response.card);
                    var token = response.id;
                    // AJAX - you would send 'token' to your server here.
                    $.post('/account/stripe_card_token', {
                        token: token
                    })
                        // Assign handlers immediately after making the request,
                        .done(function (data, textStatus, jqXHR) {
                            $form.find('[type=submit]').html('Payment successful <i class="fa fa-check"></i>').prop('disabled', true);
                        })
                        .fail(function (jqXHR, textStatus, errorThrown) {
                            $form.find('[type=submit]').html('There was a problem').removeClass('success').addClass('error');
                            /* Show Stripe errors on the form */
                            $form.find('.payment-errors').text('Try refreshing the page and trying again.');
                            $form.find('.payment-errors').closest('.row').show();
                        });
                }
            });
        }
    }

    function init() {
        applyJQueryInputMasks();
        ko.applyBindings(new ViewModel(), document.getElementById('paymentView'));
    }

    function applyJQueryInputMasks() {
        ko.bindingHandlers.masked = {
            init: function (element, valueAccessor, allBindingsAccessor) {
                var mask = allBindingsAccessor().mask || {};
                $(element).mask(mask, {
                    completed: function () {
                        var $next = $(".payment-field").eq($(".payment-field").index(element) + 1);
                        $next.focus();
                    }
                });
                $(element).focus(function () {
                    var val = $(this).val();
                    if (!val || val.trim() == "") {
                        if ($(element).is("input[type=text]")) {
                            $(element).caret(0);
                        }
                    }
                });
                ko.utils.registerEventHandler(element, 'focusout', function () {
                    var observable = valueAccessor();
                    observable($(element).val());
                });
            },
            update: function (element, valueAccessor) {
                var value = ko.utils.unwrapObservable(valueAccessor());
                $(element).val(value);
            }
        };
    }

    function createOrder() {

        var receivedFromPrevStep = window.cacheOrder;

        var postObj = receivedFromPrevStep

        $.post('/order', postObj)
            // Assign handlers immediately after making the request,
            .done(function (data, textStatus, jqXHR) {
                successfullyPaid();
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                failedToPay();
            });
    }

    function successfullyPaid() {
        location.href = '#/paymentSuccessful';
        $.notify({
            message: 'Оплата пройшла успішно',
            type: 'success'
        });
    }

    function failedToPay(message) {
        $.notify({
            // options
            message: message
        }, {
                // settings
                type: 'danger'
            });
    }
})(jQuery);