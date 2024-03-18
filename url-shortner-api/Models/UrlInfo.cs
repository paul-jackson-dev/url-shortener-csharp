namespace url_shortner_api.Models
{
    public class UrlInfo
    {
        public int Id { get; set; }

        public string ShortUrl { get; set; }

        public string LongUrl { get; set; }

        //User not required
        public User? User { get; set; }

        public UrlInfo()
        {
        }

        public static string GenerateShortUrl()
        {
            string shortUrlString = "aaa";
            return shortUrlString;
        }
    }
}
