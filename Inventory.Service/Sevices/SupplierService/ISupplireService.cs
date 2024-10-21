using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Service.Sevices.SupplierService
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> GetAll();
        Supplier GetById(int id);
        void Insert(Supplier User);
        void Update(Supplier User);
        void Delete(int id);
    }
}
