using DummyProject.Models;

namespace DummyProject.Interface
{
    public interface ProductInterface
    {

        ICollection<Product> GetProducts();

        Product GetProduct(int id);

       

        bool IsProductExist(int Id);

        bool CreateProduct(Product product);

        bool UpdateProduct(Product product);

        bool DeleteProduct(int Id);

        bool ItemExist(int id);


        bool Save();
    }

}

