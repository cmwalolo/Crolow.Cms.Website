using Crolow.Cms.Core.Models.ViewModel.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common;

namespace Crolow.Cms.Core.Models.ViewModel.Navigation
{
    public class MenuItemModel
    {
        public MenuItemModel()
        {
            Children = new List<MenuItemModel>();
        }

        public string Title { get; set; }
        public string Url { get; set; }
        public List<MenuItemModel> Children { get; set; }
        public string RootUrl { get; set; }
        public bool Active { get; set; }
    }
}
