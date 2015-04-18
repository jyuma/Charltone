"use strict";

var ApplyZooming = function (list, url) {
    createImageDisplayDialog();
    bindPhotos(list, url);
};

function bindPhotos(list, url) {
    $(list).each(function (index, value) {
        if (value.DefaultPhotoId > 0) {
            $("#img_" + value.DefaultPhotoId).click(function () {
               displayImageDialog(value.DefaultPhotoId, url);
            });
        }
    });
}

function displayImageDialog(imageId, url) {
    $.getJSON(url, { "id": imageId },
        function (data) {
            var html = '<a id="lnk-dlgimage_' + imageId + '" href="javascript:;"><img id="listdialogimg_' + imageId + '" class="listdialogimg" src="data:image/jpg;base64,' + data + '" /></a>';
            $("#dialog-image-form").html(html);
            $("#lnk-dlgimage_" + imageId).click(function () {
                $("#dialog-image-form").html('');
                $.unblockUI();
            });
        });
    $.blockUI({
        message: $("#dialog-image-form"),
        backgroundColor: '#000',
        color: '#000',
        css: {
            top: 30,
            width: 0,
            height: 0
        }
    });
}

function createImageDisplayDialog() {
    var html = "<div id='dialog-image-form'></div>";
    $("#dialog-wrapper").append(html);
    $("#dialog-image-form").hide();
}

