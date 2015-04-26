var index = (function () {
    var self = {

        init: function() {
            //self.bindZoom();
        },

        bindZoom: function () {
            $.getJSON(site.url + "Instrument/GetDefaultPhotoIds", function (data) {
                $(data).each(function(index, value) {
                    if (value.Id > 0) {
                        $("#img_" + value.Id).click(function() {
                            zoom.display(value.Id);
                        });
                    }
                });
            });
        }
    };

    return {
        init: self.init
    };
})();