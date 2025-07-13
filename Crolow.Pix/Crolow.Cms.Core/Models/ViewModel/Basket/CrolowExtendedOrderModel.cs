using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crolow.Cms.Core.Models.ViewModel.Basket
{
    public class CrolowExtendedOrderModel
    {
        public CrolowOrderModel Order { get; set; } 
        public CrolowMemberModel Member { get; set; } 
        
        public string ClientSecret { get; set; }
    }
}
