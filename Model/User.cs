using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceVT.Model
{
    [Table("User")]
    public class User : Authenticate
    {
        [Key]
        public int User_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Location_Id { get; set; }

        [NotMapped]
        public Location Location { get; set; }
    }
}
