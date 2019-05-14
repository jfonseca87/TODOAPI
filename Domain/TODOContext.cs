using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class TODOContext : DbContext
    {
        public TODOContext(DbContextOptions<TODOContext> options)
            :base(options)
        {}

        public DbSet<TODO> TODO { get; set; }
    }
}
