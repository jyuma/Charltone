var position = function() {
    var self = {
        set: function() {
            $(document).scroll(function() {
                localStorage['page'] = document.URL;
                localStorage['scrollTop'] = $(document).scrollTop();
            });

            self.setScrollPosition();
        },

        setScrollPosition: function() {
            if (localStorage['page'] == document.URL) {
                $(document).scrollTop(localStorage['scrollTop']);
            }
        }
    };

    return {
        set: self.set
    };
}();