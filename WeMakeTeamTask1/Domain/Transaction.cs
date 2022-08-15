using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeMakeTeamTask1.Domain
{
    public class Transaction
    {
        public int Id { get; set; }
        
        // вместо DateTime лучше использовать DateTimeOffset
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
    }
}
