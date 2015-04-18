"use strict";

$(document).scroll(function () {
    localStorage['page'] = document.URL;
    localStorage['scrollTop'] = $(document).scrollTop();
});

var SetScrollPosition = function () {
    if (localStorage['page'] == document.URL) {
        $(document).scrollTop(localStorage['scrollTop']);
    }
};
