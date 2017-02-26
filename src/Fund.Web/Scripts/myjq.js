/// <reference path="jquery-2.1.4.js" />
jQuery(window).load(function () {
    $("#preloader").fadeOut();
});

function splitBy3(x) {
    x += '';
    x = x.replace(/ /g, "");
    var isManfi = false;
    if (x.toString().substr(0, 1) == "-") {
        isManfi = true;
        x = x.replace(/-/g, "");
    }

    if (x.length < 3) { var parts = [x]; return parts; }
    var startPos = (x.length % 3);
    var newStr = x.substr(startPos);
    var remainingStr = x.substr(0, startPos);

    var parts = newStr.match(/.{1,3}/g);
    if (remainingStr != '') { var length = parts.unshift(remainingStr); }

    if (isManfi)
        return "-" + parts;

    return parts;

}

$(function () {

    $("input[type='number'].Percent").change(function () {
        $("input[type='range'][data-id=" + $(this).attr("data-id") + "]").val($(this).val());
    });

    $("input[type='range'].Percent").change(function () {
        $("input[type='number'][data-id=" + $(this).attr("data-id") + "]").val($(this).val());
    });


    $("#CategoryID").change(function ()
    {
        $.getJSON("/Costs/GetPercents", { ID: $(this).val() }, function (data)
        {
            $(data).each(function (index, element)
            {
                $("#Percent_" + element.partner).val(element.percent).change();

            });
        });
    });


    $("#FrmCost").submit(function (e) {
       

        if ($(this).attr("valid") != "true") {
            e.preventDefault();
            var Percents = 0;
            $("input[type='number'].Percent", this).each(function (index, element) {
                Percents += parseFloat($(element).val());
            });

            if (Percents != 100) {
                alert("مجموع درصد ها به صد نمی رسد !");
                return;
            } else {
                $(this).attr("valid", true);
                setTimeout(function () {
                    $(this).submit();
                }, 100);
            }

        } else {
            $(this).submit();
        }



    });

    $(".price").focus(function () {
        $(this).maskMoney({ thousands: ',', decimal: '.', precision: 0, allowZero: true, suffix: ' ریال' });
    });

    $(".price").blur(function (e) {
        var _this = this;
        $(this).maskMoney('destroy');
        var newVal = $(this).val().replace(/,/g, "").replace(" ریال", "");
        $(this).val("");
        setTimeout(function () {
            $(_this).val(newVal);

            $(".field-validation-error", $(_this).parents("div")).remove();

        }, 1);
    });

   
    $('.modal-trigger').leanModal();

    $(".button-collapse").sideNav();

    $("span.splitBy3").each(function (index, elem) {
        $(elem).text(splitBy3($(elem).text()));
    });

    var Page = (function () {

        var $navArrows = $('#nav-arrows').hide(),
            $shadow = $('#shadow').hide(),
            slicebox = $('#sb-slider').slicebox({
                onReady: function () {

                    $navArrows.show();
                    $shadow.show();

                },
                orientation: 'r',
                cuboidsRandom: true,
                autoplay: true,
                interval : 8000,
                disperseFactor: 30
            }),

            init = function () {

                initEvents();

            },
            initEvents = function () {

                // add navigation events
                $navArrows.children(':first').on('click', function () {

                    slicebox.next();
                    return false;

                });

                $navArrows.children(':last').on('click', function () {

                    slicebox.previous();
                    return false;

                });

            };

        return { init: init };

    })();

    Page.init();

});

