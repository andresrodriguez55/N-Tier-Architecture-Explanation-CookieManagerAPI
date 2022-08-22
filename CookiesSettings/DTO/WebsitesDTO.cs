namespace CookiesSettings.DTO
{
    public class WebsitesDTO
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
    }
}
