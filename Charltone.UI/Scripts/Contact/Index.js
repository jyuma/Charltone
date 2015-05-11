/*!
 * Charltone contact.index
 * Author: John Charlton
 * Date: 2015-04
 */

; (function ($) {

    window.contact = {};

    contact.index = {
        init: function () {

            // This is so the ajax validation events still occur
            $("form#contact-form").submit(function () { eval($(this).attr("submit")); return false; });

            // Now we can submit the form manually
            $("form#contact-form").find("button").click(function () {
                if ($('#contact-form').valid()) {
                    var dialogContainer = $("#dialog-container");

                    $(dialogContainer).html('<p>One moment...</p>');
                    $(dialogContainer).dialog({
                        title: "Sending Message"
                    });

                    $.post(site.url + "Contact/Index", {
                        ContactName: $("#ContactName").val(),
                        ContactEmail: $("#ContactEmail").val(),
                        ContactPhone: $("#ContactPhone").val(),
                        ContactMessage: $("#ContactMessage").val()

                    }, function (data) {

                        $(dialogContainer).dialog('close');
                        if (data.success) {
                            $("#ContactName").val('');
                            $("#ContactEmail").val('');
                            $("#ContactPhone").val('');
                            $("#ContactMessage").val('');
                            $(dialogContainer).html('<div><p>Thank you, we received your message.</p></div><div><button id="btnOK" class="btn btn-primary">OK</button></div>');
                            $("#btnOK").click(function () {
                                $("#dialog-container").html('');
                                $("#dialog-container").dialog('close');
                            });

                            $(dialogContainer).dialog({
                                title: "Message Sent"
                            });

                        } else {
                            error.show.dialog(
                            {
                                title: "Error Sending Email",
                                message: data.message
                            });
                        }
                    });
                }
            });

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