using System.Data.Entity;

namespace ClientCRUD.EF
{
    public class Context : DbContext
    {
        public Context()
            :base ("DefaultConnection")
        {
        }

        public DbSet<Client> Clients { get; set; }
    }
}
