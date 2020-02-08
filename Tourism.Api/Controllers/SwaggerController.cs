using Microsoft.AspNetCore.Mvc;

namespace Tourism.Api.Controllers
{
    public class SwaggerController : Controller
    {
        [Route(""), HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return Redirect("/swagger/index.html");
        }
    }
}