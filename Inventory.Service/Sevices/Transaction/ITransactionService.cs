using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Service.Sevices.TransactionService
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAll();
        Transaction GetById(int id);
        void Insert(Transaction product);
        void Add(Transaction transaction);
    }
}
