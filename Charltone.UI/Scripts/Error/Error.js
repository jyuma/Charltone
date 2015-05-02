/*!
 * Charltone error.show
 * Author: John Charlton
 * Date: 2015-04
 */

; (function ($) {

    window.error = {};

    error.show = {
        dialog: function (message) {
                $("#error").text(message);
                $("#error").dialog(
                {
                    resizable: false,
                    modal: true,
                    title: 'Alert',
                    height: 150,
                    buttons: {
                        "OK": function () {
                            $(this).dialog('close');
                        }
                    }
                });
            }
    }
})(jQuery);