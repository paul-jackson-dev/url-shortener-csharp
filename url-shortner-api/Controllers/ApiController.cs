using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using url_shortner_api.Data;
using url_shortner_api.Dtos;
using url_shortner_api.Models;

namespace url_shortner_api.Controllers
{
    [ApiController]
    [Route("-/api")]
    public class ApiController : Controller
    {
        UrlShortnerDbContext context;
        UserManager<User> userManager;

        public ApiController(UrlShortnerDbContext dbContext, UserManager<User> aUserManager)
        {
            context = dbContext;
            userManager = aUserManager;
        }

        [HttpPost]
        [Route("auth/signup")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto registrationDto)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    UserName = registrationDto.Email,
                    Email = registrationDto.Email,
                    First = registrationDto.First,
                };

                var result = await userManager.CreateAsync(user, registrationDto.Password);
                return Ok(new { Message = "Registration successful." });
            }

            return BadRequest(); // send in error messages
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

        // /-/api/url/
        [HttpPost]
        [Route("url/create")]
        public ActionResult CreateUrl([FromBody] CreateUrlDto createUrlDto)
        {
            string? lastShortUrl = context.UrlInfo
                .OrderByDescending(i => i.Id)
                .FirstOrDefault()
                ?.ShortUrl; // get the string or null

            UrlInfo url = new UrlInfo()
            {
                LongUrl = createUrlDto.LongUrl,
                ShortUrl = UrlInfo.GenerateShortUrl(lastShortUrl),
                SoftDelete = false
            };
            context.UrlInfo.Add(url);
            context.SaveChanges();

            return Ok(url);
        }

        [HttpGet]
        [Route("url/redirect/{shortUrl}")]
        public ActionResult RedirectUrl(string shortUrl)
        {
            Debug.WriteLine(shortUrl);
            UrlInfo url = context.UrlInfo.Single(s => s.ShortUrl == shortUrl); // lookup using ShortUrl as the search parameter

            if (url == null)
            {
                return NotFound();
            }

            return Redirect(url.LongUrl);
        }
    }
}
