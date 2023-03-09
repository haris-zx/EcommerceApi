using DummyProject.Data;
using DummyProject.Interface;
using DummyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DummyProject.Repository
{
    public class CategoryRepository : CategoryInterface
    { 

          public DataContext _context;
    public CategoryRepository(DataContext context)
    {
        _context = context;
    }

    
        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
           return _context.Category.OrderBy(c=>c.CategoryID).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Category.Where(p => p.CategoryID == id).FirstOrDefault();
        }

        public bool IsCategoryExist(int Id)
        {
            return _context.Category.Any(c => c.CategoryID == Id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
