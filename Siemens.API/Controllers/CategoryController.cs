using Microsoft.AspNetCore.Mvc;

namespace Siemens.API.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {

        public IActionResult Get()
        {
            return Ok("Category list...");
        }
    }
}
