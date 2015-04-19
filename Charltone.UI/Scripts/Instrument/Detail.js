var Comments;
var FunFacts;

var BindPhotos = function (route, photos) {
    $(photos).each(function (index, value) {
        $("#lnk-show-image_" + value.Id).click(function () {
            showImage(value.Id, route);
        });
    });
};        

var BindTooltips = function(comments, funfacts) {
    Comments = comments;
    FunFacts = funfacts;

    $("a#commentshint").bind(
    {
        mousemove: changeCommentsToolTipPos,
        mouseenter: showCommentsToolTip,
        mouseleave: hideCommentsToolTip
    });

    $("a#funfactshint").bind(
    {
        mousemove: changeFunFactsToolTipPos,
        mouseenter: showFunFactsToolTip,
        mouseleave: hideFunFactsToolTip
    });
}

var GetInstrumentPhotos = function (route, productId) {
    var result;
    $.ajax({
        method: 'get',
        async: false,
        url: route + "Instrument/GetInstrumentPhotos",
        data: { "id": productId },
        dataType: "json",
        success: function (data) {
            result = data;
        }
    });
    return result;
}

function showImage(photoId, route) {
    $.getJSON(route + "Instrument/GetPhotoJson", { "id": photoId },
        function (data) {
            $("#currentphoto").attr('src', 'data:image/jpg;base64,' + data + '');
        });
}

function changeFunFactsToolTipPos (event) 
{
    var tooltipX = event.pageX - 8;
    var tooltipY = event.pageY + 8;
    $('div.funfactstooltip').css({ top: tooltipY, left: tooltipX });
};

function showFunFactsToolTip (event)
{
    $('div.funfactstooltip').remove();
    $('<div class="funfactstooltip">' + FunFacts + '</div>')
        .appendTo('body');
    changeFunFactsToolTipPos(event);
};

function hideFunFactsToolTip()
{
    $('div.funfactstooltip').remove();
};

function changeCommentsToolTipPos(event) {
    var tooltipX = event.pageX - 8;
    var tooltipY = event.pageY + 8;
    $('div.commentstooltip').css({ top: tooltipY, left: tooltipX });
};

function showCommentsToolTip(event) {
    $('div.commentstooltip').remove();
    $('<div class="commentstooltip">' + Comments + '</div>')
        .appendTo('body');
    changeCommentsToolTipPos(event);
};

function hideCommentsToolTip() {
    $('div.commentstooltip').remove();
};

// disable/enable upload button
$('input:file').change(
   function () {
       if ($(this).val()) {
           $('input:submit').removeAttr('disabled');
       }
   });

