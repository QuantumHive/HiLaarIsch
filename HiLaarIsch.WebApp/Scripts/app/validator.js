$(function ()
{
    $(document).on("invalid.bs.validator", "#customer-form", function (e)
    {
        var $element = $(e.relatedTarget);
        if ($element.is(":radio"))
        {
            $element.closest(".btn-group").find("label.btn").addClass("btn-outline-danger");
        }
    });

    $(document).on("valid.bs.validator", "#customer-form", function (e)
    {
        var $element = $(e.relatedTarget);
        if ($element.is(":radio"))
        {
            $element.closest(".btn-group").find("label.btn").removeClass("btn-outline-danger");
        }
    });
});