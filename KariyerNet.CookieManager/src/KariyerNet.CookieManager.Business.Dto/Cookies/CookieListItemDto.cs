namespace KariyerNet.CookieManager.Business.Dto.Cookies
{
    public class CookieListItemDto
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public bool Status { get; set; }
        public int WebSiteCookieTypeDefinitionId { get; set; }
        public string WebSiteCookieTypeName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
