using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceVT.Model
{
    [Table("Location")]
    public class Location
    {
        [Key]
        public int Location_Id { get; set; }
        public int Zip_Code { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [Column(TypeName = "decimal(8,6)")]
        public decimal Lat { get; set; }

        [Column(TypeName = "decimal(8,6)")]
        public decimal Lng { get; set; }

    }
}
