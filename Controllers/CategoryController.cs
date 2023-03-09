using AutoMapper;
using DummyProject.Dto;
using DummyProject.Interface;
using DummyProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace DummyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        public CategoryInterface categoryInterface;
        public IMapper _mapper;
        public CategoryController(CategoryInterface category, IMapper mapper1)
        {
            categoryInterface = category;
            _mapper = mapper1;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]

        public IActionResult GetCategories()
        {

            var category = _mapper.Map<List<CategoryDto>>(categoryInterface.GetCategories());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(category);


        }
        [HttpGet("{catId}")]

        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]

        public IActionResult GetCategory(int catId)
        {
            if (!categoryInterface.IsCategoryExist(catId))
                return NotFound();

            var category = _mapper.Map<CategoryDto>(categoryInterface.GetCategory(catId));

            if (!ModelState.IsValid)

                return BadRequest(ModelState);
            return Ok(category);


        }
       

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]

        public IActionResult CreateCategoty([FromBody] CategoryDto category)
        {
            if (category == null)
                return BadRequest(ModelState);

            var cat = categoryInterface.GetCategories().Where(c => c.CategoryName.Trim().ToUpper() == category.CategoryName.TrimEnd().ToUpper()).FirstOrDefault();
            if (cat != null)
            {

                ModelState.AddModelError("", "category is already found");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(category);

            if (!categoryInterface.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "something Went wrong");

                return StatusCode(500, ModelState);
            }

            return Ok("successfully created");

        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)
        {
            if (updatedCategory == null)
                return BadRequest(ModelState);

            if (categoryId != updatedCategory.CategoryID)
                return BadRequest(ModelState);

            if (!categoryInterface.IsCategoryExist(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = _mapper.Map<Category>(updatedCategory);

            if (!categoryInterface.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }




    }
}

