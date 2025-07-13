using Crolow.Cms.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crolow.Cms.Core.Models.ViewModel
{
    public class CrolowFormModel
    {
        public int Id;
        public List<CrolowFormEntry> Comments { get; set; } 
    }
}
