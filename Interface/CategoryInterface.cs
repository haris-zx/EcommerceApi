using DummyProject.Models;

namespace DummyProject.Interface
{
    public interface CategoryInterface
    {
        ICollection<Category> GetCategories();

        Category GetCategory(int id);

        bool IsCategoryExist(int Id);

        bool CreateCategory(Category category);

        bool UpdateCategory(Category category);
        bool Save();
    }

}

