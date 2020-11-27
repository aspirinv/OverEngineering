using Microsoft.AspNetCore.Mvc;

namespace OverEngineering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new 
            { 
                App = "Surf elephant",
                Version = "0.1",
                Description = "Do not use it"
            });
        }
    }
}
