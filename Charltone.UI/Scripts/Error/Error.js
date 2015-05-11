/*!
 * Charltone error.show
 * Author: John Charlton
 * Date: 2015-04
 */

; (function ($) {

    window.error = {};

    error.show = {

        dialog: function (options) {

            var config = {
                title: "",
                message: ""
            };

            $.extend(config, options);

            $("#error").text(config.message);
            $("#error").addClass("text-danger");
            $("#error").dialog(
            {
                resizable: false,
                modal: true,
                title: config.title,
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