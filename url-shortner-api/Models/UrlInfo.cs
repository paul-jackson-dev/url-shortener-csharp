using System.Diagnostics;

namespace url_shortner_api.Models
{
    public class UrlInfo
    {
        public int Id { get; set; }

        public string ShortUrl { get; set; }

        public string LongUrl { get; set; }

        public bool SoftDelete { get; set; }

        //User not required
        public User? User { get; set; }

        public UrlInfo()
        {
        }


        // this method is basically a counter. there is probably a more simple way to write it.
        public static string GenerateShortUrl(string lastShortUrl)
        {
            string characters = "abcdefghijklmnopqrstuvxyz" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "0123456789";
            //        string  characters = "abc" + "9"; //TODO remove after testing
            string shortUrlString;
            if (lastShortUrl == null)
            {
                shortUrlString = characters.Substring(0, 1); // "a"
                return shortUrlString;
            }

            int length = lastShortUrl.Length;

            if (lastShortUrl.StartsWith("9") && lastShortUrl.EndsWith("9"))
            {
                lastShortUrl = "";
                for (int i = 0; i < length + 1; i++)
                {
                    lastShortUrl += characters.Substring(0, 1); // "99" becomes "aaa"
                }
                return lastShortUrl;
            }
            else if (lastShortUrl.EndsWith("9"))
            {
                int indexOfNine = lastShortUrl.IndexOf("9");
                lastShortUrl = lastShortUrl.Replace("9", characters.Substring(0, 1));
                string lastChar = lastShortUrl.Substring(indexOfNine - 1, 1);
                int lastCharIndex = characters.IndexOf(lastChar);
                string nextChar = characters.Substring(lastCharIndex + 1, 1);
                return lastShortUrl.Substring(0, indexOfNine - 1) + nextChar + lastShortUrl.Substring(indexOfNine); // "aa9" becomes "aba"
            }
            else
            {
                string lastChar = lastShortUrl.Substring(length - 1);
                int lastCharIndex = characters.IndexOf(lastChar);
                string nextChar = characters.Substring(lastCharIndex + 1, 1);
                return lastShortUrl.Substring(0, length - 1) + nextChar; // "aaa" becomes "aab"
            }
        }
    }
}
