using EcommerceVT.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceVT.Model
{
    [Table("Deal")]
    public class Deal
    {
        [Key]
        public int Deal_Id { get; set; }
        public int Location_Id { get; set; }
        public int User_Id { get; set; }

        public int Type { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string Trade_For { get; set; }
        public int Urgency { get; set; }
        public DateTime Limit_Date { get; set; }

        [NotMapped]
        public Location Location { get; set; }
    }

    public class DealPost : Deal
    {
        [NotMapped]
        public List<IFormFile> Photos { get; set; }
    }

    public class DealGet : Deal
    {
        [NotMapped]
        public IList<File> Photos { get; set; }
    }
}
