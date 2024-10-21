using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Service.Sevices.CategoryService
{
    public interface ICategoryService
    {

        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Insert(Category Category);
        void Update(Category Category);
        void Delete(int id);

    }
}
