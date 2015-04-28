;(function($) {

    window.file = {};

    file.upload = {
        init: function (callback) {
            var fileIndex = 1;
            var totalFiles = 0;

            $('#fileupload').fileupload({
                    autoUpload: true,
                    acceptFileTypes: /(\.|\/)(jpe?g)$/i,
                    maxFileSize: 5000000, // 5 MB
                    minFileSize: undefined,
                    maxNumberOfFiles: 10,
                    sequentialUploads: true,
                    processfail: function(e, data) {
                        $.unblockUI();
                        error.show.dialog(data.files[data.index].error);
                    },
                    done: function(e, data) {
                        callback(data.result);
                    }
                })
                .on('fileuploadprogress', function(e, data) {
                    var progress = parseInt(data.loaded / data.total * 100, 10);
                    $('#progress .bar').css('width', progress + '%');
                })
                .on('fileuploaddone', function (e, data) {
                    if (fileIndex == totalFiles) {
                        $.unblockUI();
                    } else {
                        fileIndex++;
                        $("#progressmsg").text("Uploading file " + fileIndex + " of " + totalFiles + "...");
                    }
                })
                .on('fileuploadchange', function (e, data) {
                    fileIndex = 1;
                    totalFiles = data.files.length;
                    $.blockUI({
                        css: { width: '350px', border: 'none' },
                        message: $('<div id="progress"><div id="progressmsg"></div><div class="bar" style="width: 0; color: #fff"></div></div>')
                    });
                    $("#progressmsg").text("Uploading file 1 of " + totalFiles + "...");
                });
        }
    }
})(jQuery);