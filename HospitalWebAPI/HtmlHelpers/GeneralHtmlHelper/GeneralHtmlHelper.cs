using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace HtmlHelpers.GeneralHtmlHelper
{
    public static class GeneralHtmlHelper
    {
        public static MvcHtmlString WriteTitlePage(
            this HtmlHelper htmlHelper,
            string title,
            string tagName)
        {
            var newTag = new TagBuilder(tagName);
            newTag.SetInnerText(title);
            return new MvcHtmlString(newTag.ToString());
        }

        public static MvcHtmlString WriteText(
            this HtmlHelper htmlHelper, 
            string text, 
            string tagName)
        {
            var newTag = new TagBuilder(tagName);
            newTag.SetInnerText(text);
            return new MvcHtmlString(newTag.ToString());
        }
        public static MvcHtmlString WriteTextAndVariable(
            this HtmlHelper htmlHelper,
            string textBefore, 
            string variable, 
            string textAfter, 
            string tagName)
        {
            var newTag = new TagBuilder(tagName);
            var finishString = String.Format("{0} {1} {2}", textBefore, variable, textAfter);
            newTag.SetInnerText(finishString);
            return new MvcHtmlString(newTag.ToString());
        }

        public static MvcHtmlString WriteTextAndUrl(
            this HtmlHelper htmlHelper,
            string textBefore,
            string action,
            string controller,
            string linkText)
        {
            var url = htmlHelper.ActionLink(linkText, action, controller, new {Token = htmlHelper.ViewBag.Token},new object()).ToHtmlString();
            var resultString = String.Format("{0} {1}", textBefore, url);
            return new MvcHtmlString(resultString);
        }
    }
}