using Crolow.Cms.Core.Models.Enumerations;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Crolow.Cms.Core.Models.Schemas
{
    [TableName("CrolowOrders")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [ExplicitColumns]
    public class CrolowOrderSchema
    {
        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
        [Column("Id")]
        int Id { get; set; }

        [Column("MemberId")]
        public string MemberId { get; set; }

        [Column("OrderStatus")]
        public int Status { get; set; }

        [Column("PriceTotal")]
        public long PriceTotal { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("ClientSecret")]
        public string ClientSecret { get; set; }

        [Ignore()]
        public List<CrolowOrderLineSchema> Lines { get; set; }
    }
}
