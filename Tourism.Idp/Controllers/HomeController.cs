﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Tourism.Idp.Controllers
{
    [Route("idp/Home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok("identityservice4身份验证服务");
        }
    }
}