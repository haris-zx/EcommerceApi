using DummyProject.Models;

namespace DummyProject.Interface
{
    public interface CartInterface
    {

        ICollection<Cart> GetCarts(string email);

       

        Cart GetCartItem(int id);

        bool CreateCart(Cart cart);

        bool UpdateCartItem(Cart cart);

       
       bool  CartItemExist(string email, int id);
        int LastItem();

       bool  CartItemExist(int id);

        bool DeleteCartItem(Cart cart);

        bool DeleteAllCartItems(string email);
            
        bool Save();


    }
}
