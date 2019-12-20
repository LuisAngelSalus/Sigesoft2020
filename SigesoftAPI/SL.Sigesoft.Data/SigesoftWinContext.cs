using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-3OV50TL;Initial Catalog=SigesoftCore;Integrated Security=True;");
//            }
        }

    }
}
