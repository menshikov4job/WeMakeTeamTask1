using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
             //.LogTo(Console.WriteLine)
            .Options;
        }

        public AppDbContext()
            : base(_contextOptions)
        {
            Database.EnsureCreated();            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Работа с часовыми поясами это отдельная тема,
            // у меня на работе, что бы не зависеть от настроек храним часовой пояс в договорах в базе и от этого уже пляшем,
            // в этом приложении решил так:
            // в базе хранить TransactionDate в UTC, поэтому при вставке конвертируем в UTC,
            // а при выборке TransactionDate переводим в локальное время.

            modelBuilder.Entity<Transaction>().Property(e => e.TransactionDate).
                HasConversion(
                v => TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(v, DateTimeKind.Local)),
                v => DateTime.SpecifyKind(TimeZoneInfo.ConvertTimeFromUtc(v, TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id)), DateTimeKind.Local));
        }        
    }
}
