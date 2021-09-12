using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Salinger.Core.Applications.Data;

namespace Salinger.Core.Infrastructures.Databases
{
    /// <summary>
    /// how to migration :
    ///   1. dotnet tool install --global dotnet-ef
    ///   2. dotnet tool update --global dotnet-ef 
    ///   3. dotnet add package Microsoft.EntityFrameworkCore.Design
    ///   4. dotnet ef migrations add InitialCreate
    /// how to database update :
    ///   5. dotnet ef database update
    /// </summary>
    public class SalingerDbSqliteDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public DbSet<SearchActionLog> SearchActionLogs {get; set;}

        public SalingerDbSqliteDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var splitStringConverter = new ValueConverter<IEnumerable<string>, string>(
                v => string.Join(";", v), v => v.Split(new[] {';'} ));
            builder
                .Entity<SearchActionLog>()
                .Property(e => e.MailingList)
                .HasConversion(splitStringConverter);
        }
    }
}