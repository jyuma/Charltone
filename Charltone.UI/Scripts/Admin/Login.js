var DisplayAdminLoginDialog = function (route) {
    'use strict';

    $.get(route + "Admin/LoginForm", function (data) {
        if ($("#admin-login-form").length === 0)
        {
            $("body").append(data);
        }

        $('#admin-login-form').keyup(function (e) {
            if (e.keyCode == 13) {
                logInAdmin(route);
            }
        });

        $("#btnCancelAdminLogin").click(function () {
            $.unblockUI();
        });

        $("#btnLogInAdmin").click(function () {
            logInAdmin(route);
        });

        $("#txtPassword").val('');
        $("button").button();

        $.blockUI({
            css: { width: '350px' },
            message: $("#admin-login-form")
        });

        $("button").button();
        $("#validation-container-admin-login").hide();
    });
}

function logInAdmin(route) {
    'use strict';

    var pword = $("#txtPassword").val();
    var model = { password: pword };

    $("#validation-container-admin-login").html('');

    $.getJSON(route + "Admin/Login", model,
        function (data) {
            if (data.success) {
                $.unblockUI();
                window.location.reload(true);
            } else {
                if (data.messages.length > 0) {
                    $("#validation-container-admin-login").show();
                    $("#validation-container-admin-login").html('');
                    var html = "<ul>";
                    for (var i = 0; i < data.messages.length; i++) {
                        html = html + "<li>" + data.messages[i] + "</li>";
                    }
                    html = html + "</ul>";
                    $("#validation-container-admin-login").append(html);
                }
            }
        });
}
