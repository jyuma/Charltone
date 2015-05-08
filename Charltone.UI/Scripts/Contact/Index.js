/*!
 * Charltone error.show
 * Author: John Charlton
 * Date: 2015-04
 */

; (function ($) {

    window.contact = {};

    contact.index = {
        init: function () {
            $('#contact-form').validate({
                rules: {
                    ContactName: {
                        required: true
                    },
                    ContactEmail: {
                        required: true,
                        email: true
                    },
                    ContactMessage: {
                        required: true
                    }
                },
                messages: {
                    ContactName: {
                        required: "Name is required"
                    },
                    ContactEmail: {
                        required: "Email address is required",
                        email: "Invalid email address"
                    },
                    ContactMessage: {
                        required: "Message is required"
                    }
                },
                highlight: function(element) {
                    $(element).closest('.control-group').removeClass('has-success').addClass('has-error');
                },
                success: function(element) {
                    element.text('').closest('.control-group').removeClass('has-error').addClass('has-success');
                }
            });
        }
    }
})(jQuery);