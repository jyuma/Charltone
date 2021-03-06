﻿/*!
* Charltone site.getUrl
* Author: John Charlton
* Date: 2015-04
*/

var site = (function () {
    var self = {
        getUrl: function () {
            var baseUrl = location.href;
            var rootUrl = baseUrl.substring(0, baseUrl.indexOf('/', 7));

            if (rootUrl.indexOf("localhost") > 0) {
                return rootUrl + "/Charltone/";
            } else {
                return "/";
            }
        }
    };
    return {
        url: self.getUrl()
    }
})();
