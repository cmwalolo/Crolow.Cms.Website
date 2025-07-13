using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common;

namespace Crolow.Cms.Core.Models.ViewModel.Media
{
    public class MediaCollectionPageModel
    {
        public MediaCollectionPageModel()
        {
        }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string ImageUrl { get; set; }    
        public string Url { get; set; }
        public List<MediaCollectionPageModel> Collections { get; set; }
        public List<MediaPageModel> Images{ get; set; }
    }
}
