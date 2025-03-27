using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Profile_Management.Common
{
	public static class HighlightHelper
	{
        public static MvcHtmlString Highlight(this HtmlHelper html, string text, string keyword)
        {
            if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(text))
            {
                return MvcHtmlString.Create(text);
            }

            var regex = new Regex("(" + Regex.Escape(keyword) + ")", RegexOptions.IgnoreCase);
            var result = regex.Replace(text, "<span class='highlight'>$1</span>");

            return MvcHtmlString.Create(result);
        }
    }
}