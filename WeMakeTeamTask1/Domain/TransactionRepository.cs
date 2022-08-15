using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeMakeTeamTask1.Domain
{
    internal class TransactionRepository
    {
        readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            this.context = context;
        }

        internal Transaction? GetTransactionById(int id)
        {            
            // Id is primary key поэтому вернется либо одна транзакция, либо null т.к. default знач. не указано
            return context.Transactions.Where(t => t.Id == id).SingleOrDefault();
        }

        internal void InsertTransaction(Transaction transaction)
        {
            context.Transactions.Add(transaction);
            context.SaveChanges();
        }

    }
}
