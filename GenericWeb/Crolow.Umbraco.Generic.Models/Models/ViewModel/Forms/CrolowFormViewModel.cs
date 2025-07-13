using AutoMapper.Configuration.Annotations;
using Crolow.Cms.Core.Models.Entities;
using NPoco;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Crolow.Cms.Core.Models.ViewModel.Forms
{
    public class CrolowFormViewModel : CrolowFormEntry
    {
        public string PageName { get; set; }
        public string PageUrl { get; set; }
        public string StatusText { get; set; }
    }

}
