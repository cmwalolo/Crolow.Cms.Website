using Crolow.Cms.Core.Models.Schemas;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crolow.Cms.Core.Models.ViewModel.Basket
{
    public class CrolowOrderModel: CrolowOrderSchema
    { 
        public List<CrolowOrderLineModel> Lines { get; set; }
    }
}
