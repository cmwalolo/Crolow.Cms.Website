using AutoMapper.Configuration.Annotations;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Crolow.Cms.Core.Models.Entities
{
    [TableName("CrolowForm")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [ExplicitColumns]
    public class CrolowFormEntry
    {
        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("BlogPostUmbracoId")]
        public int BlogPostUmbracoId { get; set; }

        [Column("Name")]
        public required string Name { get; set; }

        [Column("Email")]
        public required string Email { get; set; }

        [Column("Website")]
        public required string Website { get; set; }

        [Column("Message")]
        [SpecialDbType(SpecialDbTypes.NVARCHARMAX)]
        public string Message { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("Status")]
        public int Status { get; set; }


        [Column("Subscribe")]
        public int Subscribe { get; set; }

        [Column("Application")]
        public string Application { get; set; }
        [Column("Additional")]
        public string Additional { get; set; }
    }

}
