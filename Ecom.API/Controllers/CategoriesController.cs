using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork work) : base(work)
        {
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var category = await work.categoryRepositry.GetAllAsync();
                if (category is null) {
                    return BadRequest();
                }
                return Ok(category);
            }
            catch (Exception ex) 
            {
                    return BadRequest(ex.Message); 
            }
        }
    }
}
