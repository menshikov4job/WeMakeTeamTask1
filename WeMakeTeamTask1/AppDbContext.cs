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
        static readonly DbContextOptions<AppDbContext> _contextOptions;

        public DbSet<Transaction> Transactions { get; set; } = null!;

        static AppDbContext()
        {
            // Открываем соединение зарание (один раз), что бы EF не закрывал его автоматом после
            // каждого освобождения контекста (после закрытия данные удаляются если Sqlite in memory).
            DbConnection connection = new SqliteConnection("Filename=:memory:");
            //DbConnection connection = new SqliteConnection("Filename=transaction.db");
            connection.Open();

            _contextOptions = new DbContextOptionsBuilder<AppDbContext>()
             .UseSqlite(connection)
            .Options;
        }

        public AppDbContext()
            : base(_contextOptions)
        {
            Database.EnsureCreated();
        }
    }
}
