namespace CookiesSettings.DTO
{
    public class WebsiteCookieTypeDefinitionsDTO
    {
        public string CookieType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; } = false;
        public DateTime CreatedDate { get; set; } = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
        public bool IsActive { get; set; } = true;
        public int WebsiteId { get; set; }
    }
}
