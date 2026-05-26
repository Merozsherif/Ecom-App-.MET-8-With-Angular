using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork work;

        public BaseController(IUnitOfWork work)
        {
            this.work = work;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
