using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using url_shortner_api.Data;
using url_shortner_api.Models;

namespace url_shortner_api.Controllers
{
    [ApiController]
    [Route("-/api")]
    public class ApiController : Controller
    {
        UrlShortnerDbContext context;

        public ApiController(UrlShortnerDbContext dbContext)
        {
            context = dbContext;
        }

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            return Ok("api is up and running.");
        }

        [HttpPost]
        [Route("")]
        public ActionResult Index([FromBody] Test test)
        {
            if (ModelState.IsValid)
            {
                context.Tests.Add(test);
                context.SaveChanges();
                return Ok(test.Name);
            }
            return BadRequest(); // returns errors in response
        }
        [HttpGet]
        [Route("auth-test")]
        [Authorize(Policy = "api")] //auth with api policy in Program.cs
        public ActionResult Auth()
        {
            return Ok("you are auth'd.");
        }
    }
}
