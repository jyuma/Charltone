var detail = (function () {
    var self = {
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
        },

        createThumbnails: function () {
            $(self.config.photos).each(function (index, value) {
               self.appendThumbnail(index + 1, value.Id);  // make 1-based
            });
        },

        appendThumbnail: function (index, id) {
            var divThumbnail = document.createElement('div');
            $(divThumbnail).attr('id', 'instrdetailthumbnail_' + id);
            $(divThumbnail).addClass('instrdetailthumbnail');

            var image = '<a id=' + "lnk-show-image_" + id + ' href="javascript:;"><img class="thumbnail" src=' + site.url + 'Instrument/GetThumbnail/' + id + ' alt="" /></a>';
            $(divThumbnail).append(image);

            if (self.config.isAuthenticated) {
                $(divThumbnail).append(self.createPhotoButtons(index, id));
            }

            $("#instrdetailthumbnailcontainer").append(divThumbnail);
        },

        createPhotoButtons: function(index, id) {
            var divButtons = document.createElement('div');
            $(divButtons).attr('id', 'instrdetailbuttoncontainer');

            // Default button
            var buttonDefault = document.createElement("button");
            $(buttonDefault).text('D');
            $(buttonDefault).attr('id', 'default_' + id);
            $(buttonDefault).addClass('thumbnailbutton');
            if (id === self.config.defaultPhotoId) {
                $(buttonDefault).attr('disabled', 'disabled');
            } else {
                $(buttonDefault).removeAttr('disabled');
            }
            $(buttonDefault).click(function () { self.setAsDefault(id); });

            // Remove button
            var buttonRemove = document.createElement('button');
            $(buttonRemove).text('R');
            $(buttonRemove).attr('id', 'remove_' + id);
            $(buttonRemove).addClass('thumbnailbutton');
            $(buttonRemove).click(function () { self.removePhoto(id); });

            // Move Left button
            var buttonMoveLeft = document.createElement("button");
            $(buttonMoveLeft).text('<');
            $(buttonMoveLeft).attr('id', 'moveleft_' + id);
            $(buttonMoveLeft).addClass('thumbnailbutton');
            if (index === 1) {
                $(buttonMoveLeft).attr('disabled', 'disabled');
            } else {
                $(buttonMoveLeft).removeAttr('disabled');
            }
            $(buttonMoveLeft).click(function () { self.movePhoto(id, index, -1); });

            // Move Right button
            var buttonMoveRight = document.createElement('button');
            $(buttonMoveRight).text('>');
            $(buttonMoveRight).attr('id', 'moveright_' + id);
            $(buttonMoveRight).addClass('thumbnailbutton');
            if (index === self.config.photos.length) {
                $(buttonMoveRight).attr('disabled', 'disabled');
            } else {
                $(buttonMoveRight).removeAttr('disabled');
            }
            $(buttonMoveRight).click(function () { self.movePhoto(id, index, 1); });

            $(divButtons).append(buttonDefault);
            $(divButtons).append(buttonRemove);
            $(divButtons).append(buttonMoveLeft);
            $(divButtons).append(buttonMoveRight);

            return divButtons;
        },

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

        bindThumbnails: function() {
            $(self.config.photos).each(function (index, value) {
                $("#lnk-show-image_" + value.Id).click(function () {
                    self.displayPhoto(value.Id);
                });
            });
        },

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

        setAsDefault: function (id) {
            $.post(site.url + "Instrument/SetDefaultPhoto", { "id": self.config.productId, "photoId": id }, function() {
                $("#default_" + self.config.defaultPhotoId).removeAttr('disabled');
                self.config.defaultPhotoId = id;
                $("#default_" + id).attr('disabled', 'disabled');
                self.displayPhoto(id);
            });
        },

        removePhoto: function (id) {
            if (confirm("Delete photo?")) {
                $.post(site.url + "Instrument/RemovePhoto", { "id": self.config.productId, "photoId": id }, function() {
                    $("#instrdetailthumbnail_" + id).remove();
                    self.totalPhotos--;
                });
            }
        },

        movePhoto: function (id, index, increment) {
            var thumbnail = $("#instrdetailthumbnailcontainer").find("#instrdetailthumbnail_" + id);

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
                self.enableMoveButtons();
            });
        },

        enableMoveButtons: function () {
            var insideMoveButtons = $("#instrdetailthumbnailcontainer div[id*='instrdetailthumbnail']").not(':first').not(':last').find("[id^=move]");
            var firstMovelLeft = $("#instrdetailthumbnailcontainer div[id^='instrdetailthumbnail'] button[id^='moveleft']").first();
            var lastMoveRight = $("#instrdetailthumbnailcontainer div[id^='instrdetailthumbnail'] button[id^='moveright']").last();

            $(insideMoveButtons).removeAttr('disabled');
            $(firstMovelLeft).attr('disabled', 'disabled');
            $(lastMoveRight).attr('disabled', 'disabled');
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

