﻿/*!
 * Charltone admin.login
 * Author: John Charlton
 * Date: 2015-04
 */

; (function ($) {

    window.admin = {};

    admin.login = {
        init: function (options) {
            var config = {
                isAuthenticated: false
            };

            $.extend(config, options);

            if (!config.isAuthenticated) {
                $("body").css("cursor", "wait");
                $.get(site.url + "Admin/LoginForm", function(data) {
                    if ($("#admin-login-form").length === 0) {
                        $("body").append(data);
                    }

                    $('#admin-login-form').keyup(function(e) {
                        if (13 === e.keyCode) {
                            logInAdmin();
                        }
                    });

                    $("#btnCancelAdminLogin").click(function() {
                        $.unblockUI();
                    });

                    $("#btnLogInAdmin").click(function() {
                        logInAdmin();
                    });

                    $("#txtPassword").val('');
                    $("button").button();

                    $("body").css("cursor", "default");
                    $.blockUI({
                        css: {
                            width: '350px',
                            background: '#333',
                            cursor: 'default'
                        },
                        message: $("#admin-login-form")
                    });

                    $("button").button();
                    $("#validation-container").hide();
                });
            } else {
                $.post(site.url + "Admin/LogOff", function () {
                    window.location.reload();
                });
            }

            function logInAdmin() {
                var pword = $("#txtPassword").val();
                var model = { password: pword };

                $("#validation-container").html('');

                $.getJSON(site.url + "Admin/Login", model,
                    function(data) {
                        if (data.success) {
                            $.unblockUI();
                            window.location.reload(true);
                        } else {
                            if (data.messages.length > 0) {
                                $("#validation-container").show();
                                $("#validation-container").html('');
                                var html = "<ul>";
                                for (var i = 0; i < data.messages.length; i++) {
                                    html = html + "<li>" + data.messages[i] + "</li>";
                                }
                                html = html + "</ul>";
                                $("#validation-container").append(html);
                            }
                        }
                    });
            }
        }
    }
})(jQuery);