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
            var userName = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Debug.WriteLine(userName);
            string appUser = _userManager.GetUserName(HttpContext.User);
            Debug.WriteLine(appUser);
            Debug.WriteLine("hwerawerhere");
            return Ok("api is up and running.");
        }

        [HttpGet]
        [Route("auth-test")]
        [Authorize] //auth with api policy in Program.cs
        public ActionResult Auth()
        {
            //var appUser = _userManager.GetUserName(HttpContext.User);
            //Debug.WriteLine(appUser);
            //Debug.WriteLine("Asdfasdfasdfasdfasdfasdfsad");
            var claims = HttpContext.User.Claims.ToList();
            foreach (Claim claim in claims)
            {
                Debug.WriteLine(claim.Value.ToString());
            }
            AppUser user = _services.GetAppUserByUsername(HttpContext);
            //Debug.WriteLine(user.First, user.UserName, user.Email);  

            return Ok("you are auth'd. " + user.First);
        }

        // /-/api/url/
        [HttpPost]
        [Route("url/create")]
        public ActionResult CreateUrl([FromBody] CreateUrlDto createUrlDto)
        {
            var appUser = _userManager.GetUserName(HttpContext.User);
            var claims = HttpContext.User.Claims.ToList();
            foreach (Claim claim in claims)
            {
                Debug.WriteLine(claim.Value.ToString());
            }
            Debug.WriteLine(appUser);
            Debug.WriteLine("Asdfasdfasdfasdfasdfasdfsad");
            Debug.WriteLine(Request.Headers.Authorization.ToString());
            AppUser user = _services.GetAppUserByUsername(HttpContext);
            UrlInfo url = _services.CreateUrlInfo(createUrlDto, user);

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
