;(function($) {

    window.file = {};

    file.upload = {
        init: function (callback) {
            $('#fileupload').fileupload({
                dataType: 'json',
                add: function (e, data) {
                    data.context = $('<div id="progress"><div id="progressmsg">0%</div><div class="bar" style="width: 0; color: #fff"></div></div>');
                    $.blockUI({
                        css: { width: '350px', border: 'none' },
                        message: $(data.context)
                    });
                    data.submit();
                },
                acceptFileTypes: /(\.|\/)(jpg)$/i,
                done: function (e, data) {
                    if (data.result.success) {
                        callback(data.result);
                    } else {
                        error.show.dialog(data.result.message);
                    }
                },
                progressall: function (e, data) {
                    var progress = parseInt(data.loaded / data.total * 100, 10);
                    $('#progress .bar').css('width', progress + '%');
                    $("#progressmsg").text(progress + '% complete');
                    if (progress === 100) {
                        $.unblockUI();
                    }
                }
            });
        }
    }
})(jQuery);