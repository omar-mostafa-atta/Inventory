using Inventory.Service.Sevices.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Repositories;

namespace Inventory.Service.Sevices.SupplierService
{
    public class SupplierService:ISupplierService
    {

         
            private readonly IGenericRepository<Supplier> _SupplierRepository;

            public SupplierService(IGenericRepository<Supplier> SupplierRepository)
            {
            _SupplierRepository = SupplierRepository;
            }

            public IEnumerable<Supplier> GetAll()
            {
                return _SupplierRepository.GetAll();
            }

            public Supplier GetById(int id)
            {
                return _SupplierRepository.GetById(id);
            }

            public void Insert(Supplier Supplier)
            {
            _SupplierRepository.Add(Supplier);
            }

            public void Update(Supplier Supplier)
            {
            _SupplierRepository.Update(Supplier);
            }

            public void Delete(int id)
            {
            _SupplierRepository.Delete(id);
            }
        }

    
}
