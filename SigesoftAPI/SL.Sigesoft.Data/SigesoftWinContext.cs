using Microsoft.EntityFrameworkCore;
using SL.Sigesoft.Data.Configuration;
using SL.Sigesoft.Data.Configuration.Win;
using SL.Sigesoft.Models.Win;

namespace SL.Sigesoft.Data
{
    public partial class SigesoftWinContext : DbContext
    {
        public SigesoftWinContext()
        {
        }
        public SigesoftWinContext(DbContextOptions<SigesoftWinContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Component> Component { get; set; }
        public virtual DbSet<SystemParameter> SystemParameter { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
            modelBuilder.ApplyConfiguration(new ComponentConfiguration());
        }

    }
}
