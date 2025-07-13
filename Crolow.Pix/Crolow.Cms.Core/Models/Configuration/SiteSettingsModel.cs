using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Crolow.Core.Models.Configuration
{
    public class SiteSettingsModel
    {
        public string Theme { get; set; }
        public IPublishedContent Footer { get; set; }
    }
}
