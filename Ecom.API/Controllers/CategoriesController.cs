using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork work, IMapper mapper) : base(work,mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var category = await work.categoryRepositry.GetAllAsync();
                if (category is null) {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(category);
            }
            catch (Exception ex) 
            {
                    return BadRequest(ex.Message); 
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> getbyId( int id)
        {
            try
            {
                var category = await work.categoryRepositry.GetByIdAsync(id);
                if (category is null)
                {
                    return BadRequest(new ResponseAPI(400,$"not found categroy id= {id}"));
                }
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> add(CategoryDTO categoryDTO)
        {
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                await work.categoryRepositry.AddAsync(category);
                return Ok(new ResponseAPI(200, "Item has been Added"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-category")]
        public async Task<IActionResult> update(UpdateCategoryDTO categoryDTO)
        {
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                await work.categoryRepositry.UpdateAsync(category); 
                return Ok(new ResponseAPI(200, "Item has been Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete-category/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                await work.categoryRepositry.DeleteAsync(id);
                return Ok(new ResponseAPI(200, "Item has been Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
