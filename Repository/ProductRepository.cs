using DummyProject.Data;
using DummyProject.Interface;
using DummyProject.Models;

namespace DummyProject.Repository
{
    public class ProductRepository : ProductInterface
    {
        public DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateProduct(Product product)
        {
            _context.Add(product);
            return Save();
        }

        public bool DeleteProduct(int Id)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            return _context.Product.Where(p => p.ProductID == id).FirstOrDefault();
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Product.OrderBy(p => p.ProductID).ToList();
        }

        public bool IsProductExist(int Id)
        {
            return _context.Product.Any(c => c.ProductID == Id);
        }

        public bool ItemExist(int id)
        {
            return _context.main.Any(c=>c.Id== id);
        }

        public bool Save()
        {
            /*  var saved= _context.SaveChanges();
               return saved>0?true:false;*/
            return _context.SaveChanges() > 0;
        }

        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            return Save();
        }
    }
}
