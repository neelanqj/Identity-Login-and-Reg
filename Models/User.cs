using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Identity_Login_and_Reg.Models
{
    public class User : IdentityUser
    {
        public List<Message> Messages { get; set; }
        public User()
        {
            Messages = new List<Message>();
        }
    }
}