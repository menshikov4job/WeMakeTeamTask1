using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeMakeTeamTask1.Domain;

namespace WeMakeTeamTask1
{
    public class AppDbContext : DbContext
    {
        static readonly DbContextOptions<AppDbContext> contextOptions;

        public DbSet<Transaction> Transactions { get; set; } = null!;

        static AppDbContext()
        {
            // открываем соединение зарание, что бы EF не закрываля его автоматом после каждого создания контекста
            DbConnection connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            contextOptions = new DbContextOptionsBuilder<AppDbContext>()
             .UseSqlite(connection)
            .Options;
        }

        public AppDbContext()
            : base(contextOptions)
        {
            Database.EnsureCreated();
        }
    }
}
