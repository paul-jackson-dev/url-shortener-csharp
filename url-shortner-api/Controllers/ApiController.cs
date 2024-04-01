using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Diagnostics;
using System.Security.Claims;
using url_shortner_api.Data;
using url_shortner_api.Dtos;
using url_shortner_api.Models;
using url_shortner_api.Services;

namespace url_shortner_api.Controllers
{
    [ApiController]
    [Route("-/api")]
    public class ApiController : Controller
    {
        ControllerServices _services;
        UrlShortnerDbContext _context;
        UserManager<AppUser> _userManager;

        public ApiController(UrlShortnerDbContext dbContext, UserManager<AppUser> aUserManager)
        {
            _services = new ControllerServices(dbContext);
            _userManager = aUserManager;

        }

        [HttpPost]
        [Route("auth/signup")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto registrationDto)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = registrationDto.Email,
                    Email = registrationDto.Email,
                    First = registrationDto.First,
                };

                var result = await _userManager.CreateAsync(user, registrationDto.Password);
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

        [HttpGet]
        [Route("auth-test")]
        [Authorize(Policy = "api")] //auth with api policy in Program.cs
        public ActionResult Auth()
        {
            AppUser user = _services.GetAppUserByUsername(HttpContext);
            Debug.WriteLine(user.First, user.UserName, user.Email);  

            return Ok("you are auth'd. " + user.First);
        }

        // /-/api/url/
        [HttpPost]
        [Route("url/create")]
        public ActionResult CreateUrl([FromBody] CreateUrlDto createUrlDto)
        {
            UrlInfo url = _services.CreateUrlInfo(createUrlDto);
         
            if (url == null)
            {
                return BadRequest();
            }

            return Ok(url);
        }

        [HttpGet]
        [Route("url/redirect/{shortUrl}")]
        public ActionResult RedirectUrl(string shortUrl)
        {
            //Debug.WriteLine(shortUrl);
            UrlInfo url = _services.GetShortUrlRedirect(shortUrl);

            if (url == null)
            {
                return NotFound();
            }

            return Redirect(url.LongUrl);
        }
    }
}
