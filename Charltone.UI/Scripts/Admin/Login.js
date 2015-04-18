"use strict";

var BindAdminLogInForm = function (loginUrl, logoffUrl) {
    $("#btnCancelAdminLogin").click(function () {
        $("#validation-container-admin-login").html("");
        $.unblockUI();
    });

    $("#btnLogInAdmin").click(function () {
        logInAdmin(loginUrl);
    });

    $('#admin-login-form').keyup(function (e) {
        if (e.keyCode == 13) {
            logInAdmin(loginUrl);
        }
    });

    $("#lnk-admin-login").click(function () {
        displayAdminLoginDialog();
    });

    $("#lnk-admin-logoff").click(function () {
        $.getJSON(logoffUrl, null, function (data) {
            if (data.success) {
                window.location.reload(true);
            }
        });
    });
}

function displayAdminLoginDialog() {
    $("#txtPassword").val('');
    $("button").button();
    $.blockUI({
        css: { width: '350px' },
        message: $("#admin-login-form")
    });

    $("button").button();
    $("#validation-container-admin-login").hide();
}

function logInAdmin(url) {
    var pword = $("#txtPassword").val();
    var model = { password: pword };

    $("#validation-container-admin-login").html("");

    $.getJSON(url, model,
        function (data) {
            if (data.success) {
                $.unblockUI();
                window.location.reload(true);
            } else {
                if (data.messages.length > 0) {
                    $("#validation-container-admin-login").show();
                    $("#validation-container-admin-login").html("");
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

var CreateForm = function() {
    var html =   "<div id='admin-login-form' hidden>" +
                    "<div id='admin-login'></div>" +
                    "div class='dialog-header'><h3>Admin Log In</h3></div>" +
                        "<div id='admin-login-credentials'>" +
                        "<div>" +
                            "<div>Password</div>" +
                            "<div><input type='password' id='txtPassword' maxlength=20/></div>" +
                        "</div>" +
                    "</div>" +
                    "<div id='admin-login-cancel-button-container'>" +
                        "<span id='login-button'>" +
                            "<button id='btnLogInAdmin' type='button'>Log In</button>" +
                        "</span>" +
                        "<span id='cancel-button'>" +
                            "<button id='btnCancelAdminLogin' type='button'>Cancel</button>" +
                        "</span>" +
                    "</div>" +
                    "<div id='validation-container-admin-login' class='validation-container'></div>" +
                "</div>";

    $("#dialog-wrapper").append(html);
}