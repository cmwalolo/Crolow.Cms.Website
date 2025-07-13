using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Crolow.Cms.Core.Models.ViewModel.Basket
{
    public  class CrolowProductModel
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName {get; set; }
        public string Description {get; set; }
        public long Price {get; set; }
        public string ExternalReference { get; set; }
    }
}
