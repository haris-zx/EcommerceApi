using DummyProject.Data;
using DummyProject.Interface;
using DummyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DummyProject.Repository
{
    public class CartRepository : CartInterface
    {
        public DataContext _context;
       

        public CartRepository(DataContext context)
        {
            _context = context;
        }

        public bool CartItemExist(string email, int id)
        {
            return _context.Cart.Any(c => c.UserEmail == email && c.CartId == id);
        }

        public bool CreateCart(Cart cart)
        {
            _context.Cart.Add(cart);
            return Save();

        }

        
       

        public ICollection<Cart> GetCarts(string email)
        {

            return _context.Cart.Where(c => c.UserEmail == email).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCartItem(Cart cart)
        {

            _context.Update(cart);
            return Save();

        }
      public bool  CartItemExist(int id)
        {
            return _context.Cart.Any(c => c.CartId == id);
        }

     

        public int LastItem()
        {

             var item = _context.Cart.OrderBy(a => a.CartId).LastOrDefault();
            if (item == null)

                return 0;


            return item.CartId;
        }

        public Cart GetCartItem(int id)
        {
            return _context.Cart.Where(c=>c.CartId==id).FirstOrDefault();

        }

        public bool DeleteCartItem(Cart cart)

        {
            /*var del=_context.Cart.Where(c=>c.CartId== id).FirstOrDefault();*/
            _context.Cart.Remove(cart);
            return Save();
           
        }

        public bool DeleteAllCartItems(string email)
        {
            var mail = _context.Cart.Where(c => c.UserEmail == email).ToList();
             
            _context.RemoveRange(mail);

            return Save();

                
        }
    }
}