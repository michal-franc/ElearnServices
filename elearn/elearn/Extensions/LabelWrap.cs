using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System;

namespace elearn.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString WrapedInLabel(this HtmlHelper helper, MvcHtmlString @object, object labelAttribtues = null, object wrappedObjectAttribtues = null)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder wrappedObjectSb = new StringBuilder();
            Match match = Regex.Match(@object.ToHtmlString(), @"id=""(?<ID>\w+)""");
            if (match.Success)
            {
                sb.AppendFormat("<label ");
                if (labelAttribtues != null)
                {
                    PropertyInfo[] propertyInfos = labelAttribtues.GetType().GetProperties();
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        sb.AppendFormat(@"{0}=""{1}"">", propertyInfo.Name, propertyInfo.GetValue(labelAttribtues, null));
                    }
                }
                sb.AppendFormat(@"<strong>{0}</strong>", match.Groups["ID"].Value);

                wrappedObjectSb.Append(@object.ToHtmlString());
                int firstWhiteSpace = @object.ToHtmlString().IndexOf(" ");
                

                if (wrappedObjectAttribtues != null)
                {
                    PropertyInfo[] propertyInfos = wrappedObjectAttribtues.GetType().GetProperties();
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        wrappedObjectSb.Insert(firstWhiteSpace,String.Format(@" {0}=""{1}"" ", propertyInfo.Name, propertyInfo.GetValue(wrappedObjectAttribtues, null)));
                    }
                }

                sb.AppendFormat("{0}", wrappedObjectSb);
                sb.Append("</label>");
            }
            return new HtmlString(sb.ToString());
        }
    }
}