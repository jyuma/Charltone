/*!
 * Charltone file.upload
 * Author: John Charlton
 * Date: 2015-04
 */

; (function ($) {

    window.file = {};

    file.upload = {
        init: function (options, callback) {

            var config = {
                maxImageWidth: 0,
                maxImageHeight: 0,
            };

            $.extend(config, options);

            var fileIndex = 1;
            var totalFiles = 0;

            $('#fileupload').fileupload({
                autoUpload: true,
                acceptFileTypes: /(\.|\/)(jpe?g)$/i,
                maxFileSize: 5000000, // 5 MB
                minFileSize: undefined,
                maxNumberOfFiles: 10,
                disableImageResize: false,
                imageMaxWidth: config.maxImageWidth,
                imageMaxHeight: config.maxImageHeight,
                imageCrop: true,
                sequentialUploads: true,
                processfail: function(e, data) {
                    $("#progress").dialog('close');
                    error.show.dialog(
                    {
                        title: "Invalid File Type",
                        message: data.files[data.index].error
                    });
                },
                done: function(e, data) {
                    callback(data.result);
                }
            })
            .on('fileuploadprogressall', function(e, data) {
                var progress = parseInt(data.loaded / data.total * 100, 10);
                $('#progress .bar').css('width', progress + '%');
            })
            .on('fileuploaddone', function () {
                if (fileIndex == totalFiles) {
                    $("#progress").dialog('close');
                } else {
                    fileIndex++;
                    $("#progressmsg").text("Uploading file " + fileIndex + " of " + totalFiles + "...");
                }
            })
            .on('fileuploadchange', function (e, data) {
                fileIndex = 1;
                totalFiles = data.files.length;

                if ($("body").find("#progress").length === 0) {
                    $("body").append('<div id="progress"><div id="progressmsg"></div><div class="bar" style="width: 0; color: #fff;"></div></div>');
                }

                $("#progress").dialog(
                {
                    resizable: false,
                    modal: true,
                    title: "Photo Uploader",
                    height: 160,
                    width: 350,
                });

                $("#progressmsg").text("Uploading file 1 of " + totalFiles + "...");
                $('#progress .bar').css('width', '0');
            });
        }
    }
})(jQuery);