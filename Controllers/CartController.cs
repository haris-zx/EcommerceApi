using AutoMapper;
using DummyProject.Dto;
using DummyProject.Interface;
using DummyProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace DummyProject.Controllers
{
  
        [ApiController]
        [Route("api/cart")]
        public class CartController : Controller
        {
            private CartInterface _cartRepository;
            private ProductInterface _itemRepository;
            private IUserInteface _userRepository;
            private IMapper _mapper;
            public CartController(CartInterface cartRepository, ProductInterface itemRepository, IUserInteface userRepository, IMapper mapper)
            {
                _cartRepository = cartRepository;
                _itemRepository = itemRepository;
                _userRepository = userRepository;
                _mapper = mapper;
            }
            [HttpGet("getAllCartItems/{userEmail}")]
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]
            public IActionResult GetCartItems(string userEmail)
            {
                var cartItems = _mapper.Map<List<Cart>>(_cartRepository.GetCarts(userEmail));
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(cartItems);
            }
            [HttpPost("addCartItem")]
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]
            public IActionResult AddCartItem(CartDto cartItem)
            {
                if (cartItem == null)
                    return BadRequest(ModelState);
                if (!_userRepository.UserExist(cartItem.UserEmail))
                    return BadRequest("Invalide User Email");
                if (_cartRepository.CartItemExist(cartItem.UserEmail, cartItem.CartId))
                    return BadRequest("Cart Item Already Added");
                if (!_itemRepository.IsProductExist(cartItem.ProductId))
                    return BadRequest("Item Doesn't Exist");
                Cart cart = new Cart();
            /*   cart.CartId= _cartRepository.LastItem() + 1;*/
/*            cart.CartId = 99;
*/            cart.UserEmail = cartItem.UserEmail.Trim();
                cart.ProductId = cartItem.ProductId;
                cart.Quantity = cartItem.Quantity;
                bool cartItemAdded = _cartRepository.CreateCart(cart);
                if (!cartItemAdded)
                    return BadRequest("Something went wrong");
                return Ok("Succesfully Added");
            }
            [HttpPost("updateCartItem")]
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]
            public IActionResult UpdateCartItem(Cart cartItem)
            {
                if (cartItem == null)
                    return BadRequest(ModelState);
                if (!_cartRepository.CartItemExist(cartItem.ProductId))
                    return BadRequest("Cart Doesn't Exsit");
                bool cartItemUpdated = _cartRepository.UpdateCartItem(cartItem);
                if (!cartItemUpdated)
                    return BadRequest("Something went wrong");
                return Ok("Succesfully Updated");
            }
            [HttpDelete("deleteCartItem/{cartId}")]
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]
            public IActionResult DeleteCartItem(int cartId)
            {
                if (!_cartRepository.CartItemExist(cartId))
                    return BadRequest("Cart Item Doesn't Exist");
                Cart cartToDelete = _cartRepository.GetCartItem(cartId);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (!_cartRepository.DeleteCartItem(cartToDelete))
                    return BadRequest("Something went wrong");
                return NoContent();
            }
            [HttpDelete("deleteAllCartItem/{userEmail}")]
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]
            public IActionResult DeleteAllCartItem(string userEmail)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (!_cartRepository.DeleteAllCartItems(userEmail))
                    return BadRequest("Something went wrong");
                return Ok("All Cart Items Deleted Succesfully");
            }
        }
    }

