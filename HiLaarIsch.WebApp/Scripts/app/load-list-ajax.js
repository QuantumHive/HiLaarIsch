$(function () {
    var source = $("#index-container").data("source");
    $.ajax({
        url: "/" + source + "/list",
        cache: false,
        method: "get",
        success: function (data)
        {
            $("#index-container").html(data);
        }
    });
});