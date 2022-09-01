using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookiesSettings.Models;
using KariyerNet.CookieManager.Business.BusinessEngine;
using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using KariyerNet.CookieManager.Business.Dto.WebSiteCookieTypeDefinitions;
using KariyerNet.CookieManager.Data.Contract.Repository;
using Mapster;
using Moq;

namespace NUnitTests.Engines
{
    public class WebSiteCookieTypeDefinitionEngineTest
    {
        private readonly Mock<IWebSiteCookieTypeDefinitionRepository> _repository;
        private readonly IWebSiteCookieTypeDefinitionEngine _engine;

        public WebSiteCookieTypeDefinitionEngineTest()
        {
            _repository = new Mock<IWebSiteCookieTypeDefinitionRepository>();
            _engine = new WebSiteCookieTypeDefinitionEngine(_repository.Object);
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetWebsiteCookieTypeDefinitions_ReturnsAllItems()
        {
            //arrange
            List<WebSiteCookieTypeDefinition> data = getSampleData();
            _repository.Setup(x => x.GetList(null, null, null)).Returns(data);
            int count = data.Count;

            //act
            var result = _engine.GetWebSiteCookieTypeDefinitions();

            //assert
            Assert.AreEqual(count, result.Count);
        }

        [Test]
        public void CreateWebsiteCookieTypeDefinition_ValidWebsiteCookieTypeDefinition_ReturnsTrue()
        {
            //arrange
            List<WebSiteCookieTypeDefinition> data = getSampleData();
            WebSiteCookieTypeDefinition webSiteCookieTypeDefinition = data[0];
            WebSiteCookieTypeDefinitionCreateRequestDto request =
                webSiteCookieTypeDefinition.Adapt<WebSiteCookieTypeDefinitionCreateRequestDto>();
            _repository.Setup(x => x.Create(It.IsAny<WebSiteCookieTypeDefinition>()));

            //act
            var result = _engine.CreateWebSiteCookieTypeDefinition(request);

            //assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void CreateWebsiteCookieTypeDefinition_InvalidWebsiteCookieTypeDefinitionTitleLength_GetsException()
        {
            //arrange
            List<WebSiteCookieTypeDefinition> data = getSampleData();
            WebSiteCookieTypeDefinition webSiteCookieTypeDefinition = data[0];
            WebSiteCookieTypeDefinitionCreateRequestDto request =
                webSiteCookieTypeDefinition.Adapt<WebSiteCookieTypeDefinitionCreateRequestDto>();
            request.Title = "aa";

            _repository.Setup(x => x.Create(It.IsAny<WebSiteCookieTypeDefinition>()));

            //act

            //assert
            Assert.That(() => _engine.CreateWebSiteCookieTypeDefinition(request), Throws.Exception);
        }

        [Test]
        public void UpdateWebsiteCookieTypeDefinition_ValidWebsiteCookieTypeDefinition_ReturnsTrue()
        {
            //arrange
            List<WebSiteCookieTypeDefinition> data = getSampleData();
            WebSiteCookieTypeDefinition webSiteCookieTypeDefinition = data[0];
            WebSiteCookieTypeDefinitionUpdateRequestDto request =
                webSiteCookieTypeDefinition.Adapt<WebSiteCookieTypeDefinitionUpdateRequestDto>();
            _repository.Setup(x => x.Update(It.IsAny<WebSiteCookieTypeDefinition>()));

            //act
            var result = _engine.UpdateWebSiteCookieTypeDefinition(request);

            //assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void UpdateWebsiteCookieTypeDefinition_InvalidWebsiteCookieTypeDefinitionTitleLength_GetsException()
        {
            //arrange
            List<WebSiteCookieTypeDefinition> data = getSampleData();
            WebSiteCookieTypeDefinition webSiteCookieTypeDefinition = data[0];
            WebSiteCookieTypeDefinitionUpdateRequestDto request =
                webSiteCookieTypeDefinition.Adapt<WebSiteCookieTypeDefinitionUpdateRequestDto>();
            request.Title = "aa";
            _repository.Setup(x => x.Create(It.IsAny<WebSiteCookieTypeDefinition>()));

            //act


            //assert
            Assert.That(() => _engine.UpdateWebSiteCookieTypeDefinition(request), Throws.Exception);
        }

        [Test]
        public void DeleteWebsite_ValidId_ReturnsTrue()
        {
            //arrange
            List<WebSiteCookieTypeDefinition> data = getSampleData();
            WebSiteCookieTypeDefinition webSiteCookieTypeDefinition = data[0];
            _repository.Setup(x => x.GetById(It.IsAny<int>())).Returns(webSiteCookieTypeDefinition);
            _repository.Setup(x => x.Delete(It.IsAny<WebSiteCookieTypeDefinition>()));

            //act
            var result = _engine.DeleteWebSiteCookieTypeDefinition(webSiteCookieTypeDefinition.Id);

            //assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void DeleteWebsiteCookieTypeDefinition_InvalidId_GetsException()
        {
            //arrange
            List<WebSiteCookieTypeDefinition> data = getSampleData();
            WebSiteCookieTypeDefinition webSiteCookieTypeDefinition = data[0];
            _repository.Setup(x => x.GetById(It.IsAny<int>())).Returns((WebSiteCookieTypeDefinition)null);
            _repository.Setup(x => x.Delete(It.IsAny<WebSiteCookieTypeDefinition>()));

            //act

            //assert
            Assert.That(() => _engine.DeleteWebSiteCookieTypeDefinition(webSiteCookieTypeDefinition.Id), Throws.Exception);
        }

        private List<WebSiteCookieTypeDefinition> getSampleData()
        {
            List<WebSiteCookieTypeDefinition> webSiteCookieTypeDefinitions = new List<WebSiteCookieTypeDefinition>
            {
                new WebSiteCookieTypeDefinition()
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
                new WebSiteCookieTypeDefinition()
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
                },
            };

            return webSiteCookieTypeDefinitions;
        }
    }
}
