using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceVT.Model
{
    [Table("Message")]
    public class Messages
    {
        [Key]
        public int Message_Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int Deal_Id { get; set; }
        public int User_Id { get; set; }
    }
}