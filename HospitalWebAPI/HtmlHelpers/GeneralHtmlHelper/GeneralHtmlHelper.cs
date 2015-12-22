using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace HtmlHelpers.GeneralHtmlHelper
{
    public static class GeneralHtmlHelper
    {
        public static MvcHtmlString InsertText(string text, string tagName)
        {
            var newTag = new TagBuilder(tagName);
            newTag.SetInnerText(text);
            return new MvcHtmlString(newTag.ToString());
        }
        public static MvcHtmlString InsertText(string textBefore, string variable, string textAfter, string tagName)
        {
            var newTag = new TagBuilder(tagName);
            var finishString = String.Format("{0} {1} {2}", textBefore, variable, textAfter);
            newTag.SetInnerText(finishString);
            return new MvcHtmlString(newTag.ToString());
        }

        public static MvcHtmlString InsertTextAndUrl(
            this HtmlHelper htmlHelper,
            string textBefore,
            string action,
            string controller,
            string linkText,
            string textAfter,
            string tagName)
        {
            var newTag = new TagBuilder(tagName);
            var url = htmlHelper.ActionLink(linkText, action, controller);
            var finishString = String.Format("{0} {1} {2}", textBefore, url, textAfter);
            newTag.SetInnerText(finishString);
            return new MvcHtmlString(newTag.ToString());
        }

        /*public static MvcHtmlString InsertTextAndUrlByToken(string textBefore, string action, string controller, string token, string linkText, string textAfter, string tagName)
        {
            var domain = Environment.UserDomainName;
            var newTag = new TagBuilder(tagName);
            var url = String.Format("<a href='{0}'/'{1}'/'{2}'/Token/'{3}'>{4}</a>", domain, action, controller, token, linkText);
            var finishString = String.Format("{0} {1} {2}", textBefore, url, textAfter);
            newTag.SetInnerText(finishString);
            return new MvcHtmlString(newTag.ToString());
        }*/
    }
}