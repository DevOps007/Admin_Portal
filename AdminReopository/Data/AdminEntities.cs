using AdminReopository.Model;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminReopository.EDMX
{
    public partial class AdminEntities : DbContext
    {
        public AdminEntities(DbContextOptions<AdminEntities> options) : base(options)
        {
        }

        public DbSet<Login> Logins { get; set; }

        public DbSet<TxnHistory> txnhist { get; set; }

        public DbSet<AccountMaster> AccountMasters { get; set; }

        public DbSet<accmast> accmast { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Login>().HasNoKey();
            modelBuilder.Entity<TxnHistory>().HasKey(a => a.Id);
            modelBuilder.Entity<AccountMaster>().HasKey(a => a.accno);

        }
    }
}
