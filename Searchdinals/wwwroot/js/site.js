// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $('.loadingButton').on('click', function () { $("#loadingoverlay").fadeIn(); });

    $(".more").click(function () {
        var datavalue = $(this).data('complete');
        $(this).text("less..").siblings(".complete_" + datavalue).show();
    }, function () {
        var datavalue = $(this).data('complete');
        $(this).text("more..").siblings(".complete_" + datavalue).hide();
    });
});