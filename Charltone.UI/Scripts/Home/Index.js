;(function($) {

    window.home = {};

    home.index = {
        init: function() {
            $('#fileupload').fileupload({
                dataType: 'json',
                add: function(e, data) {
                    data.context = $('<p/>').text('Uploading...').appendTo(document.body);
                    $.blockUI({
                        css: { width: '350px' },
                        message: $(data.context)
                    });
                    data.submit();
                },
                acceptFileTypes: /(\.|\/)(jpg)$/i,
                done: function(e, data) {
                    data.context.text('');
                    window.location.reload();
                }
            }).on('fileuploadprogressall', function(e, data) {
                var progress = parseInt(data.loaded / data.total * 100, 10);
                $('#progressbar').text(progress + '%').css('color', '#fff');
                if (progress === 100) {
                    $('#progressbar').text('');
                    $.unblockUI();
                }
            });
        }
    }
})(jQuery);