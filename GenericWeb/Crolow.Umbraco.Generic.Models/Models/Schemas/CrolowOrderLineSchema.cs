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
    [TableName("CrolowOrderLines")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [ExplicitColumns]
    public class CrolowOrderLineSchema
    {
        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("OrderId")]
        public required string OrderId { get; set; }

        [Column("ProductId")]
        public required int ProductId { get; set; }

        [Column("Quantity")]
        public required long Quantity { get; set; }

        [Column("UnitPrice")]
        public required long UnitPrice { get; set; }

        [Column("TotalPrice")]
        public required long TotalPrice { get; set; }
        [Column("Status")]
        public int Status { get; set; }
    }
}
