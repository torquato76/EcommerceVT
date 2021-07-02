using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceVT.Model
{
    [Table("Deliverie")]
    public class Deliverie
    {
        [Key]
        public int Deliverie_Id { get; set; }
        public int Deal_Id { get; set; }
        public int User_Id { get; set; }
    }
}
