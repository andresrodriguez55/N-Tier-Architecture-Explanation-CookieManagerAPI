using System.Linq;
using CookiesSettings.Models;
using KariyerNet.CookieManager.Business;
using KariyerNet.CookieManager.Business.BusinessEngine;
using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using KariyerNet.CookieManager.Business.Dto.WebSites;
using KariyerNet.CookieManager.Data.Contract.Repository;
using KariyerNet.CookieManagerApi.Controllers;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace NUnitTests
{
    public class WebsiteEngineTest
    {
        private readonly Mock<IWebSiteRepository> _repository;
        private readonly IWebSiteEngine _engine;

        public WebsiteEngineTest() 
        {
            _repository = new Mock<IWebSiteRepository>();
            _engine = new WebSiteEngine(_repository.Object);
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetWebsites_ReturnsAllItems()
        {
            //arrange
            List<WebSite> data = getSampleData();
            _repository.Setup(x => x.GetList(null, null, null)).Returns(data);
            int count = data.Count;

            //act
            var result = _engine.GetWebSites();

            //assert
            Assert.AreEqual(count, result.Count);
        }

        [Test]
        public void CreateWebsite_ValidWebsite_ReturnsTrue()
        {
            //arrange
            List<WebSite> data = getSampleData();
            WebSite webSite = data[0];
            WebSiteCreateRequestDto request = webSite.Adapt<WebSiteCreateRequestDto>();
            _repository.Setup(x => x.Create(It.IsAny<WebSite>()));

            //act
            var result = _engine.CreateWebSite(request);

            //assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void CreateWebsite_InvalidWebsiteNameLength_GetsException()
        {
            //arrange
            List<WebSite> data = getSampleData();
            WebSite webSite = data[0];
            WebSiteCreateRequestDto request = webSite.Adapt<WebSiteCreateRequestDto>();
            request.Name = "aa";
            _repository.Setup(x => x.Create(It.IsAny<WebSite>()));

            //act


            //assert
            Assert.That(() => _engine.CreateWebSite(request), Throws.Exception);
        }

        [Test]
        public void UpdateWebsite_ValidWebsite_ReturnsTrue()
        {
            //arrange
            List<WebSite> data = getSampleData();
            WebSite webSite = data[0];
            WebSiteUpdateRequestDto request = webSite.Adapt<WebSiteUpdateRequestDto>();
            _repository.Setup(x => x.Update(It.IsAny<WebSite>()));

            //act
            var result = _engine.UpdateWebSite(request);

            //assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void UpdateWebsite_InvalidWebsiteNameLength_GetsException()
        {
            //arrange
            List<WebSite> data = getSampleData();
            WebSite webSite = data[0];
            WebSiteUpdateRequestDto request = webSite.Adapt<WebSiteUpdateRequestDto>();
            request.Name = "aa";
            _repository.Setup(x => x.Create(It.IsAny<WebSite>()));

            //act


            //assert
            Assert.That(() => _engine.UpdateWebSite(request), Throws.Exception);
        }

        [Test]
        public void DeleteWebsite_ValidId_ReturnsTrue()
        {
            //arrange
            List<WebSite> data = getSampleData();
            WebSite webSite = data[0];
            _repository.Setup(x => x.GetById(It.IsAny<int>())).Returns(webSite);
            _repository.Setup(x => x.Delete(It.IsAny<WebSite>()));

            //act
            var result = _engine.DeleteWebSite(webSite.Id);

            //assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void DeleteWebsite_InvalidId_GetsException()
        {
            //arrange
            List<WebSite> data = getSampleData();
            WebSite webSite = data[0];
            _repository.Setup(x => x.GetById(It.IsAny<int>())).Returns((WebSite) null);
            _repository.Setup(x => x.Delete(It.IsAny<WebSite>()));

            //act

            //assert
            Assert.That(() => _engine.DeleteWebSite(webSite.Id), Throws.Exception);
        }

        private List<WebSite> getSampleData()
        {
            List<WebSite> websites = new List<WebSite>
            {
                new WebSite()
                {
                    Id = 1,
                    Name = "kariyer",                
                },
                new WebSite()
                {
                    Id = 2,
                    Name = "coensio",
                },
                new WebSite()
                {
                    Id = 3,
                    Name = "iskolig",
                },
                new WebSite()
                {
                    Id = 4,
                    Name = "unisbul"
                }
            };

            return websites;
        }
    }
}