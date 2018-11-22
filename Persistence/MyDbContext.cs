using Identity_Login_and_Reg.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity_Login_and_Reg.Persistence
{
    public class MyDbContext: IdentityDbContext<User>
    {
        public DbSet<User> users { get; set; }
        public DbSet<Message> messages { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
            
        }
    }
}