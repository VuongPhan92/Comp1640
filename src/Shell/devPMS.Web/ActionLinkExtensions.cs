using University;
using devPMS.Web.Models;
using SSTVN.PMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace devPMS.Web
{
    public static class ActionLinkExtensions
    {
        public static MvcHtmlString ThreadLink(this HtmlHelper helper, Thread thread)
        {
            return helper.ActionLink(thread.Title, "Thread", "Forum", new { year = thread.CreatedDT.Year, month = thread.CreatedDT.Month, day = thread.CreatedDT.Day, title = thread.UrlSeo }, new { title = thread.Title });
        }

        public static MvcHtmlString ThreadLink(this HtmlHelper helper, ThreadViewModel thread)
        {
            return helper.ActionLink(thread.Title, "Thread", "Forum", new { year = thread.CreatedDT.Year, month = thread.CreatedDT.Month, day = thread.CreatedDT.Day, title = thread.UrlSeo }, new { title = thread.Title });
        }

        public static MvcHtmlString CategoryLink(this HtmlHelper helper, Category category)
        {
            return helper.ActionLink(category.CategoryName, "Category", "Forum", new { category = category.UrlSeo }, new { title = String.Format("See all posts in {0}", category.CategoryName) });
        }

        public static MvcHtmlString TagLink(this HtmlHelper helper, Tag tag)
        {
            return helper.ActionLink(tag.TagName, "Tag", "Forum", new { tag = tag.UrlSeo }, new { title = String.Format("See all posts in {0}", tag.TagName)});
            }

        public static MvcHtmlString ImageUrl(this HtmlHelper helper, string photo)
        {
            return MvcHtmlString.Create(string.Format(@"{0}{1}", Constant.PARENT_PATH_API_IMAGE, photo));
        }
    }
}