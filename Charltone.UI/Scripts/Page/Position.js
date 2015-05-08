/*!
* Charltone page.position
* Author: John Charlton
* Date: 2015-04
*/

; (function ($) {

    window.position = {};

    position.set = {
        init: function () {

            $(document).scroll(function () {
                localStorage.setItem('page', document.URL);
                localStorage['scrollTop'] = $(document).scrollTop();
            });

            setScrollPosition();

            function setScrollPosition() {
                if (localStorage.getItem('page') == document.URL) {
                    $(document).scrollTop(localStorage['scrollTop']);
                }
            }
        }
    }
})(jQuery);