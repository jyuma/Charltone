var detail = (function() {
    var self = {
        config: {
            photoIds: [],
            comments: "",
            funfacts: "",
        },

        init: function (options) {
            jQuery.extend(self.config, options);
            self.bindTooltips();
            self.bindPhotos();
            self.bindUploadButton();
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

        bindPhotos: function() {
            $(self.config.photoIds).each(function (index, value) {
                $("#lnk-show-image_" + value).click(function() {
                    self.showImage(value);
                });
            });
        },

        showImage: function (id) {
            $.getJSON(site.url + "Instrument/GetPhotoJson", { "id": id },
                function(data) {
                    $("#currentphoto").attr('src', 'data:image/jpg;base64,' + data + '');
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

        bindUploadButton: function () {
            $('input:file').change(
                function() {
                    if ($(this).val()) {
                        $('input:submit').removeAttr('disabled');
                    }
                });
            }
    };

    return {
        init: self.init
    };

})();

