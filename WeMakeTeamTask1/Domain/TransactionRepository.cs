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
            Transaction? transaction = _context.Transactions.Where (t => t.Id == id).SingleOrDefault();                      
            return transaction;
        }

        public int Insert(Transaction transaction)
        {            
            _context.Transactions.Add(transaction);
            return _context.SaveChanges();
        }

    }
}
