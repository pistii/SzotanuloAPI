using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using SzotanuloAPI.Entities;

namespace SzotanuloAPI
{
    public partial class DBContext : DbContext
    {
        public virtual DbSet<Words> Words { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options) 
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:firstsqldatabase.database.windows.net,1433;Initial Catalog=firstDB;Persist Security Info=False;User ID=P_isti;Password=kopaszBarack123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
