using Crolow.Core.Models.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Extensions;

namespace Crolow.Cms.Core.Context
{
    public class SiteContext
    {
        public SiteSettingsModel SettingsModel { get; set;}

        public static object _lock = new object();

        public SiteContext()
        {

        }
        
        public static SiteContext GetContext(HttpContext context)
        {
            if (!context.Items.ContainsKey("SiteContext"))
            {
                lock (_lock)
                {
                    if (!context.Items.ContainsKey("SiteContext"))
                    {
                        context.Items.Add("SiteContext", new SiteContext());
                    }
                }
            }
            return (SiteContext) context.Items["SiteContext"];
        }
        public static SiteSettingsModel CurrentSiteSettingsModel(HttpContext context, SiteSettingsModel model = null)
        {
            SiteContext siteContext = GetContext(context);

            if (model != null)
            {
                siteContext.SettingsModel = model;
            }

            return siteContext.SettingsModel;

        }
    }
}
