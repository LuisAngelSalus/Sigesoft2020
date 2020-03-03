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
        public virtual DbSet<ProtocolWin> ProtocolWin { get; set; }
        public virtual DbSet<ProtocolComponentWin> ProtocolComponentWin { get; set; }
        public virtual DbSet<SecuentialWin> SecuentialWin { get; set; }
        public virtual DbSet<OrganizationWin> OrganizationWin { get; set; }
        public virtual DbSet<LocationWin> LocationWin { get; set; }
        public virtual DbSet<GroupOccupationWin> GroupOccupationWin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-servicing-10079");
            modelBuilder.ApplyConfiguration(new ComponentConfiguration());
            //modelBuilder.ApplyConfiguration(new SystemParameterConfiguration());
            modelBuilder.ApplyConfiguration(new ProtocolWinConfiguration());
            modelBuilder.ApplyConfiguration(new ProtocolComponentWinConfiguration());
            modelBuilder.ApplyConfiguration(new SecuentialWinConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationWinConfiguration());
            modelBuilder.ApplyConfiguration(new LocationWinConfiguration());
            modelBuilder.ApplyConfiguration(new GroupOccupationWinConfiguration());
        }

    }
}
