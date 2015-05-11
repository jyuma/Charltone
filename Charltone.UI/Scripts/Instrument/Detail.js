/*!
 * Charltone instrument.detail
 * Author: John Charlton
 * Date: 2015-04
 */

; (function ($) {

    window.instrument = {};

    instrument.detail = {
        init: function (options) {
            var config = {
                productId: 0,
                defaultPhotoId: 0,
                isAuthenticated: false,
                maxSaveImageWidth: 0,
                maxSaveImageHeight: 0,
                maxDisplayImageWidth: 0,
                maxDisplayImageHeight: 0,
                maxZoomImageWidth: 0,
                maxZoomImageHeight: 0
            };

            $.extend(config, options);

            var photos;
            var cssInputDisabled = { opacity: 0.5, cursor: 'default' };
            var cssInputEnabled = { opacity: 1, cursor: 'pointer' };
            var totalPhotos = 0;
            var currentPhotoId = config.defaultPhotoId;

            $.getJSON(site.url + "Instrument/GetInstrumentPhotos", { "id": config.productId },
                function (data) {
                    photos = data;
                    totalPhotos = photos.length;
                    createThumbnails();
                    bindZoom(currentPhotoId);
                    file.upload.init(
                        {
                            maxImageWidth: config.maxSaveImageWidth,
                            maxImageHeight: config.maxSaveImageHeight
                        },
                        function (result) {
                        appendThumbnail(result.id);
                        displayPhoto(result.id);
                        photos.push({ Id: result.id, IsDefault: false });
                        totalPhotos++;
                        enableInputMove();
                    });
                });

            // Thumbnails
            function createThumbnails() {
                $(photos).each(function(index, value) {
                    appendThumbnail(value.Id);
                    enableInputDefault();
                    enableInputDelete();
                });
                enableInputMove();
            };

            function appendThumbnail(id) {
                var divThumbnail = document.createElement('div');
                $(divThumbnail).attr('id', 'instrdetailthumbnail_' + id);
                $(divThumbnail).addClass('instrdetailthumbnail');

                var image = '<a id=' + "img_" + id + ' href="javascript:;"><img class="img-rounded" src=' + site.url + 'Instrument/GetThumbnail/' + id + ' alt="" /></a>';
                $(divThumbnail).append(image);

                if (config.isAuthenticated) {
                    $(divThumbnail).append(createThumbnailImageInputs(id));
                }

                $("#instrdetailthumbnailcontainer").append(divThumbnail);

                bindThumbnail(id);
            };

            function createThumbnailImageInputs(id) {
                var divButtons = $("<div id='instrdetailbuttoncontainer'>");

                // Set Default
                var inputDefault = $("<input type='image' src='" + site.url + "Content/images/Check.ico' id='default_" + id + "' class='thumbnailbutton' alt=''/>");
                $(inputDefault).click(function() { setAsDefault(id); });
                $(divButtons).append(inputDefault);

                // Remove
                var inputRemove = $("<input type='image' src='" + site.url + "Content/images/Delete.ico' id='delete_" + id + "' class='thumbnailbutton' alt=''/>");
                $(inputRemove).click(function() { deletePhoto(id); });
                $(divButtons).append(inputRemove);

                // Move Left
                var inputMoveLeft = $("<input type='image' src='" + site.url + "Content/images/Previous.ico' id='moveleft_" + id + "' class='thumbnailbutton' alt=''/>");
                $(inputMoveLeft).click(function() { movePhoto(id, -1); });
                $(divButtons).append(inputMoveLeft);

                // Move Right
                var inputMoveRight = $("<input type='image' src='" + site.url + "Content/images/Next.ico' id='moveright_" + id + "' class='thumbnailbutton' alt=''/>");
                $(inputMoveRight).click(function() { movePhoto(id, 1); });
                $(divButtons).append(inputMoveRight);

                return divButtons;
            };

            function bindThumbnail(id) {
                $("#img_" + id).click(function () {
                    displayPhoto(id);
                });
            };

            // Zooming
            function bindZoom() {
                $("#instrdetailmainphotolink").click(function() {
                    instrument.zoom.init({
                        photoId: currentPhotoId,
                        maxImageWidth: config.maxZoomImageWidth,
                        maxImageHeight: config.maxZoomImageHeight
                    });
                });
            };

            function displayPhoto(id) {
                $.getJSON(site.url + "Instrument/GetPhotoJson", { "PhotoId": id, "Width": config.maxDisplayImageWidth, "Height": config.maxDisplayImageHeight },
                    function(data) {
                        $("#currentphoto").attr('src', 'data:image/jpg;base64,' + data + '');
                        currentPhotoId = id;
                        enableInputDelete();
                    });
            };

            // Photo Control Buttons
            function setAsDefault(id) {
                $.post(site.url + "Instrument/SetDefaultPhoto", { "id": config.productId, "photoId": id }, function() {
                    displayPhoto(id);
                    config.defaultPhotoId = id;
                    enableInputDefault();
                    enableInputDelete();
                });
            };

            function deletePhoto(id) {
                if ($("body").find("#dialog-confirm-delete").length === 0) {
                    $("#instrdetail").append("<div id='dialog-confirm-delete'>Delete photo?</div>");
                    $("#dialog-confirm-delete").addClass("text-danger");
                }
                
                $("#dialog-confirm-delete").dialog(
                {
                    resizable: false,
                    modal: true,
                    title: "Confirm Delete",
                    height: 160,
                    width: 200,
                    buttons: {
                        "Yes": function() {
                            $(this).dialog('close');
                            $.post(site.url + "Instrument/DeletePhoto", { "id": config.productId, "photoId": id }, function() {
                                $("#instrdetailthumbnail_" + id).remove();
                                totalPhotos--;
                                enableInputMove();
                            });
                        },
                        "No": function() {
                            $(this).dialog('close');
                        }
                    }
                });
            };

            function movePhoto(id, increment) {
                var thumbnail = $("#instrdetailthumbnailcontainer").find("#instrdetailthumbnail_" + id);
                var index = $("#instrdetailthumbnailcontainer div[id^='instrdetailthumbnail']").index(thumbnail) + 1; // make sortOrder 1-based

                var adjacent;
                if (increment > 0) {
                    adjacent = $(thumbnail).next("div");
                } else {
                    adjacent = $(thumbnail).prev("div");
                }

                var thumbnailId = parseInt(thumbnail.attr('id').substring(thumbnail.attr('id').indexOf("_") + 1, thumbnail.attr('id').length));
                var adjacentId = parseInt(adjacent.attr('id').substring(adjacent.attr('id').indexOf("_") + 1, adjacent.attr('id').length));

                $.post(site.url + "Instrument/MovePhoto", { "id": thumbnailId, "adjacentId": adjacentId, "sortOrder": index + increment }, function() {
                    if (increment > 0) {
                        $(thumbnail).insertAfter(adjacent);
                    } else {
                        $(thumbnail).insertBefore(adjacent);
                    }
                    enableInputMove();
                });
            };

            function enableInputMove() {
                var firsttMovelLeft = $("#instrdetailthumbnailcontainer div[id^='instrdetailthumbnail'] input[id^='moveleft']").first();
                var lastMoveRight = $("#instrdetailthumbnailcontainer div[id^='instrdetailthumbnail'] input[id^='moveright']").last();
                var allOthertMoves = $("#instrdetailthumbnailcontainer div[id^='instrdetailthumbnail']").not(':first').not(':last').find("[id^=move]");

                $(allOthertMoves).removeAttr('disabled').css(cssInputEnabled);
                $(firsttMovelLeft).attr('disabled', 'disabled').css(cssInputDisabled);
                $(lastMoveRight).attr('disabled', 'disabled').css(cssInputDisabled);
            };

            function enableInputDefault() {
                var id = config.defaultPhotoId;

                var inputDefault = $("#instrdetailthumbnailcontainer div[id='instrdetailthumbnail_" + id + "'] input[id='default_" + id + "']");
                var allOtherDefaults = $("#instrdetailthumbnailcontainer div[id^='instrdetailthumbnail'] input[id^='default']").not(inputDefault);

                $(allOtherDefaults).removeAttr('disabled').css(cssInputEnabled);
                $(inputDefault).attr('disabled', 'disabled').css(cssInputDisabled);
            };

            function enableInputDelete() {
                var inputDefaultDelete = $("#instrdetailthumbnailcontainer div[id='instrdetailthumbnail_" + config.defaultPhotoId + "'] input[id='delete_" + config.defaultPhotoId + "']");
                var inputCurrentDelete = $("#instrdetailthumbnailcontainer div[id='instrdetailthumbnail_" + currentPhotoId + "'] input[id='delete_" + currentPhotoId + "']");
                var allOtherDeletes = $("#instrdetailthumbnailcontainer div[id^='instrdetailthumbnail'] input[id^='delete']").not(inputDefaultDelete);

                $(allOtherDeletes).removeAttr('disabled').css(cssInputEnabled);

                $(inputCurrentDelete).attr('disabled', 'disabled').css(cssInputDisabled);
                $(inputDefaultDelete).attr('disabled', 'disabled').css(cssInputDisabled);
            };
        }
    }
})(jQuery);

