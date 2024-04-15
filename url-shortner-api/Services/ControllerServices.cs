using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using url_shortner_api.Data;
using url_shortner_api.Dtos;
using url_shortner_api.Models;

namespace url_shortner_api.Services
{
    public class ControllerServices
    {

        UrlShortnerDbContext _context;

        public ControllerServices(UrlShortnerDbContext dbContext)
        {
            _context = dbContext;
        }

        // get AppUser or return null
        public AppUser GetAppUserByUsername(HttpContext httpContext)
        {
            try
            {
                string username = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
                AppUser? appUser = _context.Users.FirstOrDefault(u => u.UserName == username);
                return appUser;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UrlInfo CreateUrlInfo(CreateUrlDto createUrlDto, AppUser user)
        {
            // get the last short url
            string? lastShortUrl = _context.UrlInfo
                .OrderByDescending(i => i.Id)
                .FirstOrDefault()
                ?.ShortUrl; // get the string or null

            // create a new short url
            UrlInfo url = new UrlInfo()
            {
                LongUrl = createUrlDto.LongUrl,
                ShortUrl = UrlInfo.GenerateShortUrl(lastShortUrl),
                SoftDelete = false,
                AppUser = user // if signed in, else null
            };
            
            if (url == null)
            {
                return null;
            }

            // add to the db and save
            _context.UrlInfo.Add(url);
            _context.SaveChanges();


            return url;
        }

        public UrlInfo GetShortUrlRedirect(string shortUrl)
        {
            // lookup using ShortUrl as the search parameter
            UrlInfo url = _context.UrlInfo.Single(s => s.ShortUrl == shortUrl); 
            return url; // return url or null
        }

    }
}
