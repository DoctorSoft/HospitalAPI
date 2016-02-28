using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HtmlHelpers.GeneralHtmlHelper
{
    public class ModalWindowsHelper
    {
        public static HtmlString AddActionModalWindow(HtmlHelper helper, string headText, string bodyText, string actionName,
            string controlName, object paramsList, string applyButtonName)
        {
            var a = helper.ActionLink(applyButtonName, actionName, controlName, paramsList, new { @class = "btn btn-default" });

            var result = "<div id='modalWindow' class='modal fade' role='dialog'>" +
                            "<div class='modal-dialog'>" +
                            "<div class='modal-content'>" +
                            "<div class='modal-header'>" +
                            "<button type='button' class='close' data-dismiss='modal'>&times;</button>" +
                            "<h4 class='modal-title'>" + headText + "</h4>" +
                            "</div>" +
                            "<div class='modal-body'>" +
                            "<p>" + bodyText + "</p>" +
                            "</div>" +
                            "<div class='modal-footer'>" +
                            "<button type='button' class='btn btn-default' data-dismiss='modal'>Отмена</button>" +
                            a.ToString() +
                            "</div>" +
                            "</div>" +
                            "</div>" +
                            "</div>";

            return new HtmlString(result);
        }

        public static HtmlString AddInfoModalWindow(HtmlHelper helper, string headText, string bodyText)
        {
            var result = "<div id='modalWindow' class='modal fade in' role='dialog' style='display: block !important'>" +
                            "<div class='modal-dialog'>" +
                            "<div class='modal-content'>" +
                            "<div class='modal-header'>" +
                            "<button type='button' class='close' data-dismiss='modal'>&times;</button>" +
                            "<h4 class='modal-title'>" + headText + "</h4>" +
                            "</div>" +
                            "<div class='modal-body'>" +
                            "<p>" + bodyText + "</p>" +
                            "</div>" +
                            "<div class='modal-footer'>" +
                            "<button type='button' class='btn btn-default' data-dismiss='modal'>Ok</button>" +
                            "</div>" +
                            "</div>" +
                            "</div>" +
                            "</div>";

            return new HtmlString(result);
        }
    }
}