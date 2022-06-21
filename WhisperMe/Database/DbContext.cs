using Microsoft.EntityFrameworkCore;
using WhisperMe.Entities;

namespace WhisperMe.Database
{

    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {

        }



        public DbSet<Whisper> Whisper { get; set; }
        public DbSet<User> User { get; set; }
    }
}
