using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Product.Domain.Common;
using Product.Domain.Security;

namespace Product.Domain
{
    public class DB : DbContext
    {

        public DB(DbContextOptions<DB> options) : base(options)
        {
        }

        #region security
        public DbSet<User> Users { get; set; }

        #endregion

        #region Common
        public DbSet<Products> Products { get; set; }

        #endregion


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Products>().HasKey(x => new { x.ProduceDate, x.ManufactureEmail });

        }
    }
}
