using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceVT.Model
{
    [Table("Invite")]
    public class Invite
    {
        [Key]
        public int Invite_Id { get; set; }
        public int User_Id { get; set; }
        public int User_Invited { get; set; }
    }

    public class InvitePost : Invite
    {
        [NotMapped]
        public string Email { get; set; }

        [NotMapped]
        public string Name { get; set; }
    }
}