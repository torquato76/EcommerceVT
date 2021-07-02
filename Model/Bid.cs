using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceVT.Model
{
    [Table("Bid")]
    public class Bid
    {
        [Key]
        public int Bid_Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        public bool Accepted { get; set; }
        public string Description { get; set; }
        public int Deal_Id { get; set; }
        public int User_Id { get; set; }
    }
}
