$(function ()
{
    $("#index-container").empty();
    $(".loader").removeClass("hidden");

    var source = $("#index-container").data("source");
    $.ajax({
        url: "/" + source + "/list",
        cache: false,
        method: "get",
        success: function (data)
        {
            $(".loader").addClass("hidden");
            $("#index-container").html(data);
        }
    });
});