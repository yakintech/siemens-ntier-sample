using Microsoft.AspNetCore.Mvc;
using Siemens.API.Models.Filters;

namespace Siemens.API.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        [SampleActionFilter]
        public IActionResult Get()
        {
            return Ok("Category list...");
        }


        [SampleActionFilter]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok("");
        }
    }
}
