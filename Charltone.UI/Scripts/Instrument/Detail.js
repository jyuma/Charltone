var detail = (function () {
    var self = {
        maxPhotos: 8,
        cssInputDisabled: { opacity: 0.5, cursor: 'default' },
        cssInputEnabled: { opacity: 1, cursor: 'pointer' },

        totalPhotos: 0,
        config: {
            productId: 0,
            photos: [],
            defaultPhotoId: 0,
            comments: "",
            funfacts: "",
            isAuthenticated: false
        },

        init: function (options) {
            jQuery.extend(self.config, options);
            self.totalPhotos = self.config.photos.length;
            self.bindTooltips();
            self.createThumbnails();
            self.bindThumbnails();
            self.bindZoom();
            self.showHideFileUpload();
        },

        // Thumbnails
        createThumbnails: function () {
            $(self.config.photos).each(function (index, value) {
                self.appendThumbnail(value.Id);
                self.enableInputDefault();
                self.enableInputDelete();
            });
            self.enableInputMove();
        },

        appendThumbnail: function (id) {
            var divThumbnail = document.createElement('div');
            $(divThumbnail).attr('id', 'instrdetailthumbnail_' + id);
            $(divThumbnail).addClass('instrdetailthumbnail');

            var image = '<a id=' + "lnk-show-image_" + id + ' href="javascript:;"><img class="thumbnail" src=' + site.url + 'Instrument/GetThumbnail/' + id + ' alt="" /></a>';
            $(divThumbnail).append(image);

            if (self.config.isAuthenticated) {
                $(divThumbnail).append(self.createThumbnailImageInputs(id));
            }

            $("#instrdetailthumbnailcontainer").append(divThumbnail);
        },

        createThumbnailImageInputs: function (id) {
            var divButtons = $("<div id='instrdetailbuttoncontainer'>");

            // Set Default
            var inputDefault = $("<input type='image' src='" + site.url + "Content/images/Check.ico' id='default_" + id + "' class='thumbnailbutton' alt=''/>");
            $(inputDefault).click(function () { self.setAsDefault(id); });
            $(divButtons).append(inputDefault);

            // Remove
            var inputRemove = $("<input type='image' src='" + site.url + "Content/images/Delete.ico' id='delete_" + id + "' class='thumbnailbutton' alt=''/>");
            $(inputRemove).click(function () { self.deletePhoto(id); });
            $(divButtons).append(inputRemove);

            // Move Left
            var inputMoveLeft = $("<input type='image' src='" + site.url + "Content/images/Previous.ico' id='moveleft_" + id + "' class='thumbnailbutton' alt=''/>");
            $(inputMoveLeft).click(function () { self.movePhoto(id, -1); });
            $(divButtons).append(inputMoveLeft);

            // Move Right
            var inputMoveRight = $("<input type='image' src='" + site.url + "Content/images/Next.ico' id='moveright_" + id + "' class='thumbnailbutton' alt=''/>");
            $(inputMoveRight).click(function () { self.movePhoto(id, 1); });
            $(divButtons).append(inputMoveRight);

            return divButtons;
        },

        bindThumbnails: function() {
            $(self.config.photos).each(function (index, value) {
                $("#lnk-show-image_" + value.Id).click(function () {
                    self.displayPhoto(value.Id);
                });
            });
        },

        // Display
        bindZoom: function() {
            $("#instrdetailmainphotolink").click(function () {
                zoom.display(self.config.defaultPhotoId);
            });
        },

        displayPhoto: function (id) {
            $.getJSON(site.url + "Instrument/GetPhotoJson", { "id": id },
                function(data) {
                    $("#currentphoto").attr('src', 'data:image/jpg;base64,' + data + '');
                    self.config.defaultPhotoId = id;
                });
        },

        // Photo Control Buttons
        setAsDefault: function (id) {
            $.post(site.url + "Instrument/SetDefaultPhoto", { "id": self.config.productId, "photoId": id }, function () {
                self.displayPhoto(id);
                self.config.defaultPhotoId = id;
                self.enableInputDefault();
                self.enableInputDelete();
            });
        },

        deletePhoto: function (id) {
            var divDialog = $("<div id='dialog-confirm-delete'>Delete photo?</div>");

            $("#instrdetailcontent").append(divDialog);
            $("#dialog-confirm-delete").dialog(
            {
                resizable: false,
                modal: true,
                title: "Confirm Delete",
                height: 140,
                width: 200,
                buttons: {
                    "Yes": function() {
                        $(this).dialog('close');
                        $.post(site.url + "Instrument/DeletePhoto", { "id": self.config.productId, "photoId": id }, function() {
                            $("#instrdetailthumbnail_" + id).remove();
                            self.totalPhotos--;
                            self.showHideFileUpload();
                        });
                    },
                    "No": function() {
                        $(this).dialog('close');
                    }
                }
            });
        },

        movePhoto: function (id, increment) {
            var thumbnail = $("#instrdetailthumbnailcontainer").find("#instrdetailthumbnail_" + id);
            var index = $("#instrdetailthumbnailcontainer div[id^='instrdetailthumbnail']").index(thumbnail) + 1;  // make sortOrder 1-based

            var adjacent;
            if (increment > 0) {
                adjacent = $(thumbnail).next("div");
            } else {
                adjacent = $(thumbnail).prev("div");
            }

            var thumbnailId = parseInt(thumbnail.attr('id').substring(thumbnail.attr('id').indexOf("_") + 1, thumbnail.attr('id').length));
            var adjacentId = parseInt(adjacent.attr('id').substring(adjacent.attr('id').indexOf("_") + 1, adjacent.attr('id').length));
            
            $.post(site.url + "Instrument/MovePhoto", { "id": thumbnailId, "adjacentId": adjacentId, "sortOrder": index + increment }, function () {
                if (increment > 0) {
                    $(thumbnail).insertAfter(adjacent);
                } else {
                    $(thumbnail).insertBefore(adjacent);
                }
                self.enableInputMove();
            });
        },

        enableInputMove: function () {
            var firsttMovelLeft = $("#instrdetailthumbnailcontainer div[id^='instrdetailthumbnail'] input[id^='moveleft']").first();
            var lastMoveRight = $("#instrdetailthumbnailcontainer div[id^='instrdetailthumbnail'] input[id^='moveright']").last();
            var allOthertMoves = $("#instrdetailthumbnailcontainer div[id^='instrdetailthumbnail']").not(':first').not(':last').find("[id^=move]");

            $(allOthertMoves).removeAttr('disabled').css(self.cssInputEnabled);
            $(firsttMovelLeft).attr('disabled', 'disabled').css(self.cssInputDisabled);
            $(lastMoveRight).attr('disabled', 'disabled').css(self.cssInputDisabled);
        },

        enableInputDefault: function () {
            var id = self.config.defaultPhotoId;

            var inputDefault = $("#instrdetailthumbnailcontainer div[id='instrdetailthumbnail_" + id + "'] input[id='default_" + id + "']");
            var allOtherDefaults = $("#instrdetailthumbnailcontainer div[id^='instrdetailthumbnail'] input[id^='default']").not(inputDefault);

            $(allOtherDefaults).removeAttr('disabled').css(self.cssInputEnabled);
            $(inputDefault).attr('disabled', 'disabled').css(self.cssInputDisabled);
        },

        enableInputDelete: function () {
            var id = self.config.defaultPhotoId;

            var inputDelete = $("#instrdetailthumbnailcontainer div[id='instrdetailthumbnail_" + id + "'] input[id='delete_" + id + "']");
            var allOtherDeletes = $("#instrdetailthumbnailcontainer div[id^='instrdetailthumbnail'] input[id^='delete']").not(inputDelete);

            $(allOtherDeletes).removeAttr('disabled').css(self.cssInputEnabled);
            $(inputDelete).attr('disabled', 'disabled').css(self.cssInputDisabled);
        },

        showHideFileUpload: function () {
            if (self.totalPhotos === self.maxPhotos)
            {
                $("#instrdetailuploaphoto").hide();
            } else {
                $("#instrdetailuploaphoto").show();
            }
        },

        // Tool Tips
        bindTooltips: function () {
            $("a#commentshint").bind(
            {
                mousemove: self.changeCommentsToolTipPos,
                mouseenter: self.showCommentsToolTip,
                mouseleave: self.hideCommentsToolTip
            });

            $("a#funfactshint").bind(
            {
                mousemove: self.changeFunFactsToolTipPos,
                mouseenter: self.showFunFactsToolTip,
                mouseleave: self.hideFunFactsToolTip
            });
        },

        changeFunFactsToolTipPos: function(event) {
            var tooltipX = event.pageX - 8;
            var tooltipY = event.pageY + 8;
            $('div.funfactstooltip').css({ top: tooltipY, left: tooltipX });
        },

        showFunFactsToolTip: function(event) {
            $('div.funfactstooltip').remove();
            $('<div class="funfactstooltip">' + self.config.funfacts + '</div>')
                .appendTo('body');
            self.changeFunFactsToolTipPos(event);
        },

        hideFunFactsToolTip: function() {
            $('div.funfactstooltip').remove();
        },

        changeCommentsToolTipPos: function(event) {
            var tooltipX = event.pageX - 8;
            var tooltipY = event.pageY + 8;
            $('div.commentstooltip').css({ top: tooltipY, left: tooltipX });
        },

        showCommentsToolTip: function(event) {
            $('div.commentstooltip').remove();
            $('<div class="commentstooltip">' + self.config.comments + '</div>')
                .appendTo('body');
            self.changeCommentsToolTipPos(event);
        },

        hideCommentsToolTip: function() {
            $('div.commentstooltip').remove();
        },
    };

    return {
        init: self.init
    };

})();

