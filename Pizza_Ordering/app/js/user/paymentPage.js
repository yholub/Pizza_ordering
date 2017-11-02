﻿var payment = (function ($) {
    return {
        init: init,
        ViewModel: ViewModel
    };

    function ViewModel(pizzas) {
        var self = this;

        self.cardNumber = ko.observable('cardNumber');
        self.cardCvc = ko.observable('cardCvc');
        self.cardExp = ko.observable('cardExp');
        self.cardExpMonth = ko.observable('cardExpMonth');
        self.cardExpYear = ko.observable('cardExpYear');

        this.cardExpMonth = ko.computed(function () {
            var cardExp = this.cardExp();
            if (cardExp.length >= 2) {
                return cardExp.substring(0, 2);
            }
        }, this);

        this.cardExpYear = ko.computed(function () {
            var cardExp = this.cardExp();
            if (cardExp.length == 5) {
                return cardExp.substring(3, 5);
            }
        }, this);

        this.payWithStripe = function (e) {
            e.preventDefault();

            /* Visual feedback */
            $form.find('[type=submit]').html('Validating <i class="fa fa-spinner fa-pulse"></i>');

            var PublishableKey = 'pk_test_aJg3RiIHme0PcBL3abE0DqnL'; // Replace with your API publishable key
            Stripe.setPublishableKey(PublishableKey);

            /* Create token */
            var expiry = $form.find('[name=cardExpiry]').payment('cardExpiryVal');
            var ccData = {
                number: $form.find('[name=cardNumber]').val().replace(/\s/g, ''),
                cvc: $form.find('[name=cardCVC]').val(),
                exp_month: expiry.month,
                exp_year: expiry.year
            };

            Stripe.card.createToken(ccData, function stripeResponseHandler(status, response) {
                if (response.error) {
                    /* Visual feedback */
                    $form.find('[type=submit]').html('Try again');
                    /* Show Stripe errors on the form */
                    $form.find('.payment-errors').text(response.error.message);
                    $form.find('.payment-errors').closest('.row').show();
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

        this.submitPayment = function (e) {
            payWithStripe(e);
        }
    }

    function init() {
        applyJQueryValidations();
        ko.applyBindings(new ViewModel(), document.getElementById('paymentView'));
    }

    function applyJQueryValidations() {
        /* Fancy restrictive input formatting via jQuery.payment library*/
        $('input[name=cardNumber]').payment('formatCardNumber');
        $('input[name=cardCVC]').payment('formatCardCVC');
        $('input[name=cardExpiry').payment('formatCardExpiry');

        /* Form validation using Stripe client-side validation helpers */
        jQuery.validator.addMethod("cardNumber", function (value, element) {
            return this.optional(element) || Stripe.card.validateCardNumber(value);
        }, "Please specify a valid credit card number.");

        jQuery.validator.addMethod("cardExpiry", function (value, element) {
            /* Parsing month/year uses jQuery.payment library */
            value = $.payment.cardExpiryVal(value);
            return this.optional(element) || Stripe.card.validateExpiry(value.month, value.year);
        }, "Invalid expiration date.");

        jQuery.validator.addMethod("cardCVC", function (value, element) {
            return this.optional(element) || Stripe.card.validateCVC(value);
        }, "Invalid CVC.");

        validator = $form.validate({
            rules: {
                cardNumber: {
                    required: true,
                    cardNumber: true
                },
                cardExpiry: {
                    required: true,
                    cardExpiry: true
                },
                cardCVC: {
                    required: true,
                    cardCVC: true
                }
            },
            highlight: function (element) {
                $(element).closest('.form-control').removeClass('success').addClass('error');
            },
            unhighlight: function (element) {
                $(element).closest('.form-control').removeClass('error').addClass('success');
            },
            errorPlacement: function (error, element) {
                $(element).closest('.form-group').append(error);
            }
        });

        paymentFormReady = function () {
            if ($form.find('[name=cardNumber]').hasClass("success") &&
                $form.find('[name=cardExpiry]').hasClass("success") &&
                $form.find('[name=cardCVC]').val().length > 1) {
                return true;
            } else {
                return false;
            }
        }

        $form.find('[type=submit]').prop('disabled', true);
        var readyInterval = setInterval(function () {
            if (paymentFormReady()) {
                $form.find('[type=submit]').prop('disabled', false);
                clearInterval(readyInterval);
            }
        }, 250);
    }
})(jQuery);




//var $form = $('#payment-form');
//$form.on('submit', payWithStripe);

///* If you're using Stripe for payments */
//function payWithStripe(e) {
//    e.preventDefault();

//    /* Visual feedback */
//    $form.find('[type=submit]').html('Validating <i class="fa fa-spinner fa-pulse"></i>');

//    var PublishableKey = 'pk_test_aJg3RiIHme0PcBL3abE0DqnL'; // Replace with your API publishable key
//    Stripe.setPublishableKey(PublishableKey);

//    /* Create token */
//    var expiry = $form.find('[name=cardExpiry]').payment('cardExpiryVal');
//    var ccData = {
//        number: $form.find('[name=cardNumber]').val().replace(/\s/g, ''),
//        cvc: $form.find('[name=cardCVC]').val(),
//        exp_month: expiry.month,
//        exp_year: expiry.year
//    };

//    Stripe.card.createToken(ccData, function stripeResponseHandler(status, response) {
//        if (response.error) {
//            /* Visual feedback */
//            $form.find('[type=submit]').html('Try again');
//            /* Show Stripe errors on the form */
//            $form.find('.payment-errors').text(response.error.message);
//            $form.find('.payment-errors').closest('.row').show();
//        } else {
//            /* Visual feedback */
//            $form.find('[type=submit]').html('Processing <i class="fa fa-spinner fa-pulse"></i>');
//            /* Hide Stripe errors on the form */
//            $form.find('.payment-errors').closest('.row').hide();
//            $form.find('.payment-errors').text("");
//            // response contains id and card, which contains additional card details            
//            console.log(response.id);
//            console.log(response.card);
//            var token = response.id;
//            // AJAX - you would send 'token' to your server here.
//            $.post('/account/stripe_card_token', {
//                token: token
//            })
//                // Assign handlers immediately after making the request,
//                .done(function (data, textStatus, jqXHR) {
//                    $form.find('[type=submit]').html('Payment successful <i class="fa fa-check"></i>').prop('disabled', true);
//                })
//                .fail(function (jqXHR, textStatus, errorThrown) {
//                    $form.find('[type=submit]').html('There was a problem').removeClass('success').addClass('error');
//                    /* Show Stripe errors on the form */
//                    $form.find('.payment-errors').text('Try refreshing the page and trying again.');
//                    $form.find('.payment-errors').closest('.row').show();
//                });
//        }
//    });
//}
///* Fancy restrictive input formatting via jQuery.payment library*/
//$('input[name=cardNumber]').payment('formatCardNumber');
//$('input[name=cardCVC]').payment('formatCardCVC');
//$('input[name=cardExpiry').payment('formatCardExpiry');

///* Form validation using Stripe client-side validation helpers */
//jQuery.validator.addMethod("cardNumber", function (value, element) {
//    return this.optional(element) || Stripe.card.validateCardNumber(value);
//}, "Please specify a valid credit card number.");

//jQuery.validator.addMethod("cardExpiry", function (value, element) {
//    /* Parsing month/year uses jQuery.payment library */
//    value = $.payment.cardExpiryVal(value);
//    return this.optional(element) || Stripe.card.validateExpiry(value.month, value.year);
//}, "Invalid expiration date.");

//jQuery.validator.addMethod("cardCVC", function (value, element) {
//    return this.optional(element) || Stripe.card.validateCVC(value);
//}, "Invalid CVC.");

//validator = $form.validate({
//    rules: {
//        cardNumber: {
//            required: true,
//            cardNumber: true
//        },
//        cardExpiry: {
//            required: true,
//            cardExpiry: true
//        },
//        cardCVC: {
//            required: true,
//            cardCVC: true
//        }
//    },
//    highlight: function (element) {
//        $(element).closest('.form-control').removeClass('success').addClass('error');
//    },
//    unhighlight: function (element) {
//        $(element).closest('.form-control').removeClass('error').addClass('success');
//    },
//    errorPlacement: function (error, element) {
//        $(element).closest('.form-group').append(error);
//    }
//});

//paymentFormReady = function () {
//    if ($form.find('[name=cardNumber]').hasClass("success") &&
//        $form.find('[name=cardExpiry]').hasClass("success") &&
//        $form.find('[name=cardCVC]').val().length > 1) {
//        return true;
//    } else {
//        return false;
//    }
//}

//$form.find('[type=submit]').prop('disabled', true);
//var readyInterval = setInterval(function () {
//    if (paymentFormReady()) {
//        $form.find('[type=submit]').prop('disabled', false);
//        clearInterval(readyInterval);
//    }
//}, 250);


///*
//https://goo.gl/PLbrBK
//*/