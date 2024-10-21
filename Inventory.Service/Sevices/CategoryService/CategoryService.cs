using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Repositories;

namespace Inventory.Service.Sevices.CategoryService
{
    public class CategoryService : ICategoryService
    {

        private readonly IGenericRepository<Category> _CategoryRepository;

        public CategoryService(IGenericRepository<Category> CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }

        public IEnumerable<Category> GetAll()
        {
            return _CategoryRepository.GetAll();
        }

        public Category GetById(int id)
        {
            return _CategoryRepository.GetById(id);
        }

        public void Insert(Category Category)
        {
            _CategoryRepository.Add(Category);
        }

        public void Update(Category Category)
        {
            _CategoryRepository.Update(Category);
        }

        public void Delete(int id)
        {
            _CategoryRepository.Delete(id);
        }
    }

}
