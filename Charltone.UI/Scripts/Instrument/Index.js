var ApplyZooming = function (route) {
    'use strict';

    $.getJSON(route + "Instrument/GetDefaultPhotoIds", function (data)
    {
        $(data).each(function (index, value) {
            if (value.Id > 0) {
                $("#img_" + value.Id).click(function () {
                    displayImageDialog(route, value.Id);
                });
            }
        });
    });
};

function displayImageDialog(route, imageId) {
    'use strict';

    $.getJSON(route + "Instrument/GetPhotoJson", { 'id': imageId },
        function (data) {

            $("#dialog-image-container").append("<a id='lnk-dlgimage_" + imageId + "' href='javascript:;'><img id='listdialogimg_" + imageId + "' class='listdialogimg' src='data:image/jpg;base64," + data + "' /></a>");

            $("#lnk-dlgimage_" + imageId).click(function () {
                $("#dialog-image-container").html('');
                $.unblockUI();
            });

            $.blockUI({
                message: $("#dialog-image-container"),
                backgroundColor: '#000',
                color: '#000',
                css: {
                    top: 30,
                    width: 0,
                    height: 0
                }
            });
        });
}