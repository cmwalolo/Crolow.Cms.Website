using Crolow.Cms.Core.Models.Umbraco;
using Crolow.Core.Models.Configuration;
using Microsoft.AspNetCore.Http;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;


namespace Crolow.Cms.Core.Context
{
    public class SiteContext
    {
        public SiteSettingsModel SettingsModel { get; set; }

        public static object _lock = new object();
        protected static Dictionary<string, SiteSettingsModel> settingsDico = new Dictionary<string, SiteSettingsModel>();

        public SiteContext()
        {

        }


        public static SiteContext Current(HttpContext context, IPublishedContent content = null)
        {
            SiteContext siteContext = GetContext(context);

            if (siteContext == null && content != null)
            {
                siteContext = new SiteContext();
                siteContext.SettingsModel = new SiteSettingsModel();
                SetContent(siteContext.SettingsModel, content);
                AddContext(context, siteContext);

            }
            context.Items["SiteContext"] = siteContext;
            return siteContext;

        }

        private static SiteContext GetContext(HttpContext context)
        {

            return context.Items.ContainsKey("SiteContext") ? (SiteContext)context.Items["SiteContext"] : null;
        }

        private static SiteContext AddContext(HttpContext context, SiteContext model)
        {

            if (!context.Items.ContainsKey("SiteContext"))
            {
                context.Items.Add("SiteContext", model);
            }
            return (SiteContext)context.Items["SiteContext"];
        }

        private static void SetContent(SiteSettingsModel model, IPublishedContent content)
        {
            model.Root = content.AncestorOrSelf(1);
            model.Home = model.Root.Children<HomePage>().FirstOrDefault();
            model.DataFolder = model.Root.Children<DataFolder>().FirstOrDefault();
            model.ProductsFolder = model.DataFolder.Children<ProductsFolder>().FirstOrDefault();
            var item = model.DataFolder.Children<SettingsFolder>()?.FirstOrDefault()?.Children<LayoutSettings>()?.FirstOrDefault();
            model.Theme = item?.Theme ?? "crolow";


        }
    }
}
