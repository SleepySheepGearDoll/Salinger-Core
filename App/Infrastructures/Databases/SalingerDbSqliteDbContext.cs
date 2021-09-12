using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Salinger.Core.Infrastructures.Databases
{
    internal class SalingerDbSqliteDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}