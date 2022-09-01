using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookiesSettings.Models;
using KariyerNet.CookieManager.Business.BusinessEngine;
using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using KariyerNet.CookieManager.Business.Dto.Cookies;
using KariyerNet.CookieManager.Business.Dto.WebSiteCookieTypeDefinitions;
using KariyerNet.CookieManager.Data.Contract.Repository;
using Mapster;
using Moq;

namespace NUnitTests.Engines
{
    public class CookieEngineTest
    {
        private readonly Mock<ICookieRepository> _cookieRepository;
        private readonly Mock<IWebSiteCookieTypeDefinitionEngine> _webSiteCookieTypeDefinitionEngine;
        private readonly ICookieEngine _cookieEngine;

        public CookieEngineTest()
        {
            _cookieRepository = new Mock<ICookieRepository>();
            _webSiteCookieTypeDefinitionEngine = new Mock<IWebSiteCookieTypeDefinitionEngine>();
            _cookieEngine = new CookieEngine(_cookieRepository.Object, _webSiteCookieTypeDefinitionEngine.Object);
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetCookies_ReturnsAllItems()
        {
            //arrange
            List<Cookie> data = getSampleData();
            _cookieRepository.Setup(x => x.GetList(null, null, null, c => c.WebSiteCookieTypeDefinition)).Returns(data);
            int count = data.Count;

            //act
            var result = _cookieEngine.GetCookies();

            //assert
            Assert.AreEqual(count, result.Count);
        }

        [Test]
        public void CreateCookie_ValidCookie_ReturnsTrue()
        {
            //arrange
            List<Cookie> data = getSampleData();
            Cookie cookie = data[0];

            CookieCreateRequestDto request = cookie.Adapt<CookieCreateRequestDto>();
            _cookieRepository.Setup(x => x.Create(It.IsAny<Cookie>()));

            WebSiteCookieTypeDefinitionListItemDto foreignKey =
               cookie.WebSiteCookieTypeDefinition.Adapt<WebSiteCookieTypeDefinitionListItemDto>();
            _webSiteCookieTypeDefinitionEngine.Setup(
                x => x.GetWebSiteCookieDefinitionById(It.IsAny<int>())).Returns(foreignKey);

            //act
            var result = _cookieEngine.CreateCookie(request);

            //assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void CreateCookie_InvalidCookie_CookieMustBeAccepted_GetsException()
        {
            //arrange
            List<Cookie> data = getSampleData();
            Cookie cookie = data[0];
            cookie.Status = false;

            CookieCreateRequestDto request = cookie.Adapt<CookieCreateRequestDto>();
            _cookieRepository.Setup(x => x.Create(It.IsAny<Cookie>()));

            WebSiteCookieTypeDefinitionListItemDto foreignKey =
               cookie.WebSiteCookieTypeDefinition.Adapt<WebSiteCookieTypeDefinitionListItemDto>();
            foreignKey.IsRequired = true;
            _webSiteCookieTypeDefinitionEngine.Setup(
                x => x.GetWebSiteCookieDefinitionById(It.IsAny<int>())).Returns(foreignKey);

            //act

            //assert
            Assert.That(() => _cookieEngine.CreateCookie(request), Throws.Exception);
        }

        private List<Cookie> getSampleData()
        {
            List<Cookie> cookies = new List<Cookie>
            {
                new Cookie
                {
                    Id = 1,
                    CreatedDate = DateTime.Now,
                    SessionId = "adfggw2342",
                    Status = true,
                    WebSiteCookieTypeDefinitionId = 1,
                    WebSiteCookieTypeDefinition = new WebSiteCookieTypeDefinition()
                    {
                        Id = 1,
                        CookieType = "performance",
                        Title = "performans",
                        Description = "kullanıcı geçmişinden faydalanarak kullanıcının site performansını artırır",
                        IsRequired = true,
                        IsActive = true,
                        WebSiteId = 4,
                        WebSite = new WebSite()
                        {
                            Id = 4,
                            Name = "unisbul"
                        }
                    },
                },
                new Cookie
                {
                    Id = 2,
                    CreatedDate = DateTime.Now,
                    SessionId = "axadfggw2342",
                    Status = false,
                    WebSiteCookieTypeDefinitionId = 2,
                    WebSiteCookieTypeDefinition = new WebSiteCookieTypeDefinition()
                    {
                        Id = 2,
                        CookieType = "targeting",
                        Title = "...",
                        Description = "........",
                        IsRequired = false,
                        IsActive = true,
                        WebSiteId = 3,
                        WebSite = new WebSite()
                        {
                            Id = 3,
                            Name = "iskolig",
                        }
                    }
                }
            };

            return cookies;
        }
    }
}
