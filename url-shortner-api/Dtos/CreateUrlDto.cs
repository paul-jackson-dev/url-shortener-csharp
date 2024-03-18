namespace url_shortner_api.Dtos
{
    public class CreateUrlDto
    {

        public string LongUrl { get; set; }

        // possibly get User info here or get it via HttpContext
    }
}
