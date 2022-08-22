namespace CookiesSettings.DTO
{
    public class CookiesDTO
    {
        public int SessionId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; } = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
        public int WebSiteCookieTypeDefinitionId { get; set; }
    }
}
