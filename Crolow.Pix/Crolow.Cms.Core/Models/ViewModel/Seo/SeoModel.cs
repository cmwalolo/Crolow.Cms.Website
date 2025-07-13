using Crolow.Cms.Core.Models.ViewModel.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common;

namespace Crolow.Cms.Core.Models.ViewModel.Navigation
{
    public class SeoModel
    {
        public string PageTitle;
        public string Canonical;
        public string MetaAuthor;
        public string MetaDescription;
        public List<string> MetaKeywords;
        public string MetaTagBlock;
        public string OGUrl;
        public string OGTitle;
        public string OGDescription;
        public string OGImage;
        public string FBAppId;
        public string OGType;
        public string OGLocale;
        public string TwitterCardType;
        public string TwitterSite;
        public string TwitterTitle;
        public string TwitterDescription;
        public string TwitterImage;
        public string TwitterImageAlt;
    }
}
