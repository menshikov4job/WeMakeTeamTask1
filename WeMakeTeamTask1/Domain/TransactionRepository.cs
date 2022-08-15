using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeMakeTeamTask1.Domain
{
    public class TransactionRepository
    {
        readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public Transaction? Get(int id)
        {
            // Id is primary key поэтому вернется либо одна транзакция, либо null т.к. default знач. не указано.
            Transaction? transaction = _context.Transactions.Where(t => t.Id == id).SingleOrDefault();
            if (transaction != null)
            {
                // работа с часовыми поясами это отдельная тема,
                // у меня на работе, что бы не зависить от настроек храним часовой пояс в договорах в базе и от этого уже пляшем,
                // но в этом приложении было решено так:
                // в базе хранить DateTime в UTC, а при выборке DateTime переводим в локальное время                
                transaction.TransactionDate = TimeZoneInfo.ConvertTimeFromUtc(transaction.TransactionDate, TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id));
                transaction.TransactionDate = DateTime.SpecifyKind(transaction.TransactionDate, DateTimeKind.Local);
            }           
            return transaction;
        }

        public void Insert(Transaction transaction)
        {
            // Решено было в базе хранить DateTime в UTC, а при выборке DateTime бутет из UTC переводится в локальное время.
            transaction.TransactionDate = DateTime.SpecifyKind(transaction.TransactionDate, DateTimeKind.Local);
            transaction.TransactionDate = TimeZoneInfo.ConvertTimeToUtc(transaction.TransactionDate);
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

    }
}
