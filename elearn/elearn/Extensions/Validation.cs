using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace elearn.Extensions
{
    public static class Validation
    {

        private static string GetInvalidPropertyValueResource(HttpContextBase httpContext)
        {
            string str = (string)null;
            if (!string.IsNullOrEmpty(ValidationExtensions.ResourceClassKey) && httpContext != null)
                str = httpContext.GetGlobalResourceObject(ValidationExtensions.ResourceClassKey, "InvalidPropertyValue", CultureInfo.CurrentUICulture) as string;
            return str;
        }

        private static string GetUserErrorMessageOrDefault(HttpContextBase httpContext, ModelError error, ModelState modelState)
        {
            if (!string.IsNullOrEmpty(error.ErrorMessage))
                return error.ErrorMessage;
            else if (modelState == null)
            {
                return (string)null;
            }
            else
            {
                string str = modelState.Value != null ? modelState.Value.AttemptedValue : (string)null;
                return string.Format((IFormatProvider)CultureInfo.CurrentCulture, GetInvalidPropertyValueResource(httpContext), new object[1]
        {
          (object) str
        });
            }
        }

        public static MvcHtmlString KeyedValidationSummary(this HtmlHelper htmlHelper,string boxClass,string iconImgSrc,IDictionary<string, object> htmlAttributes)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }
            else
            {
                string str = String.Empty;
                StringBuilder stringBuilder = new StringBuilder();
                TagBuilder tagBuilder1 = new TagBuilder("div");

                IEnumerable<ModelState> enumerable = (IEnumerable<ModelState>)htmlHelper.ViewData.ModelState.Values;
                //alert image
                if (enumerable != null && enumerable.Count() >0)
                {
                    TagBuilder imgspan = new TagBuilder("span");
                    TagBuilder img = new TagBuilder("img");
                    img.Attributes.Add("height", "24");
                    img.Attributes.Add("width", "24");
                    img.Attributes.Add("src", iconImgSrc);
                    imgspan.InnerHtml = img.ToString();

                    stringBuilder.AppendLine(imgspan.ToString(TagRenderMode.Normal));
                    tagBuilder1.AddCssClass(boxClass);

                    foreach (ModelState modelState in enumerable)
                    {
                        foreach (ModelError error in (Collection<ModelError>)modelState.Errors)
                        {
                            string messageOrDefault = GetUserErrorMessageOrDefault(htmlHelper.ViewContext.HttpContext, error, (ModelState)null);
                            if (!string.IsNullOrEmpty(messageOrDefault))
                            {
                                TagBuilder tagBuilder2 = new TagBuilder("span");
                                tagBuilder2.InnerHtml=messageOrDefault;

                                stringBuilder.AppendLine(tagBuilder2.ToString(TagRenderMode.Normal));
                            }
                        }
                    }
                    tagBuilder1.MergeAttributes<string, object>(htmlAttributes);
                    tagBuilder1.AddCssClass(htmlHelper.ViewData.ModelState.IsValid ? HtmlHelper.ValidationSummaryValidCssClassName : HtmlHelper.ValidationSummaryCssClassName);
                    tagBuilder1.InnerHtml = stringBuilder.ToString();
                    return new MvcHtmlString(tagBuilder1.ToString(TagRenderMode.Normal));
                }
                return new MvcHtmlString(String.Empty);
            }
        }
    }
}