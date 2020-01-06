using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Tourism.Idp.Controllers
{
    [Route("idp/Home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IConfiguration _config;
        public HomeController(IConfiguration config)
        {
            _config = config;
        }
        public IActionResult Index()
        {
            return Ok(_config.GetSection("AllowedHosts"));
        }
    }
}