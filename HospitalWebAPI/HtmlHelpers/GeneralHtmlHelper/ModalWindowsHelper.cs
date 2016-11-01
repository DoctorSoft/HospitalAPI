using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HtmlHelpers.GeneralHtmlHelper
{
    public class ModalWindowsHelper
    {
        public static HtmlString AddActionModalWindow(HtmlHelper helper, string headText, string bodyText,
            string actionName,
            string controlName, object paramsList, string applyButtonName, string id)
        {
            var a = helper.ActionLink(applyButtonName, actionName, controlName, paramsList,
                new { @class = "btn btn-default", @id = "CauseLink" });

            var result = "<div id='modalWindow" + id + "' class='modal fade' role='dialog'>" +
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

        public static HtmlString AddActionModalWindowWithTextBox(HtmlHelper helper, string headText, string actionName, string controlName, object paramsList, string applyButtonName, string id,
            string textBoxName)
        {
            var link = helper.ActionLink(applyButtonName, actionName, controlName, paramsList,
                new {@class = "btn btn-default", @id = "link" + id});

            var modalWindow = 
                                "<div id='modalWindow" + id + "' class='modal fade' role='dialog'>" +
                                    "<div class='modal-dialog'>" +
                                        "<div class='modal-content'>" +
                                            "<div class='modal-header'>" +
                                                "<button type='button' class='close' data-dismiss='modal'>&times;</button>" +
                                                "<h4 class='modal-title'>" + headText + "</h4>" +
                                            "</div>" +
                                            "<div class='modal-body'>" +
                                                "<textarea class='form-control' rows='3' id='" + textBoxName + id +"' style='resize:none'></textarea>" +
                                            "</div>" +
                                            "<div class='modal-footer'>" +
                                                "<button type='button' class='btn btn-default' data-dismiss='modal'>Отмена</button>" + link +
                                            "</div>" +
                                        "</div>" +
                                    "</div>" +
                                "</div>";

            var script = "<script type='text/javascript'> $(function() {" +
                         "$('#link" + id + "').click(function () {" +
                         "var name = $('#" + textBoxName + id + "').val();" +
                         "this.href = this.href + '&" + textBoxName + "=' + encodeURIComponent(name);" +
                         "});" +
                         "});" +
                         "</script>";

            var result = modalWindow + script;

            return new HtmlString(result);
        }

        public static HtmlString AddInfoModalWindow(HtmlHelper helper, string headText, string bodyText)
        {
            var script = "<script type='text/javascript'>" +
                            "$(function () { $('#modalWindow').modal('toggle');});" +
                            "</script>";

                            
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

            return new HtmlString(script + result);
        }
    }
}