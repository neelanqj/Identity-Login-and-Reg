using System.ComponentModel.DataAnnotations;

namespace Identity_Login_and_Reg.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        
    }
}