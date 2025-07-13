using Crolow.Cms.Core.Models.Schemas;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Crolow.Cms.Core.Models.ViewModel.Basket
{
    public class CrolowOrderLineModel : CrolowOrderLineSchema
    {
        public int Id { get; set; } 
        public string ProductName { get; set; } 
        public string ProductDescription { get; set; }  
        public string ProductUrl { get; set; }  
        public string ProductImageUrl { get; set; } 
    }
}
