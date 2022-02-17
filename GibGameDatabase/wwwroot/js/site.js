// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showImageToolTip(e) {
    var dataItem = $("#gameGrid").getKendoGrid().dataItem(e.target.closest("tr"));
    return kendo.template($("#template").html())(dataItem);
}

function setPlayedVisible(dataItem) {
    if (dataItem.DatePlayed) return false;
    return true;
}

function unsetPlayedVisible(dataItem) {
    if (dataItem.DatePlayed) return true;
    return false;
}