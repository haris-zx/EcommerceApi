using AutoMapper;
using DummyProject.Dto;
using DummyProject.Interface;
using DummyProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace DummyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController: Controller

    {

        public ProductInterface productInterface;
        public IMapper _mapper;
        public ProductController(ProductInterface product, IMapper mapper1)
        {
            productInterface = product;
            _mapper = mapper1;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]

        public IActionResult GetCategories()
        {

            var category = _mapper.Map<List<ProductDto>>(productInterface.GetProducts());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(category);


        }
        [HttpGet("{prodId}")]

        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(400)]

        public IActionResult GetCategory(int prodId)
        {
            if (!productInterface.IsProductExist(prodId))
                return NotFound();

            var category = _mapper.Map<ProductDto>(productInterface.GetProduct(prodId));

            if (!ModelState.IsValid)

                return BadRequest(ModelState);
            return Ok(category);


        }
      

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]

        public IActionResult CreateProduct([FromBody] ProductDto product)
        {
            if (product == null)
                return BadRequest(ModelState);
/*
            var cat = productInterface.GetProducts().Where(c => c.ProductName.Trim().ToUpper() == product.ProductName.TrimEnd().ToUpper()).FirstOrDefault();
            if (cat != null)
            {

                ModelState.AddModelError("", "Product is already found");
                return StatusCode(422, ModelState);
            }*/
   /* if(productInterface.IsProductExist(product.ProductID))
            {
                ModelState.AddModelError("", "Product is already found");
                return StatusCode(422, ModelState);
            }
*/
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            var ProductMap = _mapper.Map<Product>(product);

            if (!productInterface.CreateProduct(ProductMap))
            {
                ModelState.AddModelError("", "something Went wrong");

                return StatusCode(500, ModelState);
            }

            return Ok("successfully created");

        }

        [HttpPut("{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int productId, [FromBody] ProductDto updatedProduct)
        {
            if (updatedProduct == null)
                return BadRequest(ModelState);

            if (productId != updatedProduct.ProductID)
                return BadRequest(ModelState);

            if (!productInterface.IsProductExist(productId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = _mapper.Map<Product>(updatedProduct);

            if (!productInterface.UpdateProduct(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }




    }
}
