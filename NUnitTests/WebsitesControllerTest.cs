using System.Linq;
using CookiesSettings.Models;
using KariyerNet.CookieManager.Business;
using KariyerNet.CookieManager.Business.BusinessEngine;
using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using KariyerNet.CookieManager.Data.Contract.Repository;
using KariyerNet.CookieManagerApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace NUnitTests
{
    public class WebsitesControllerTest
    {
        private readonly Mock<IWebSiteRepository> _repository;
        private readonly IWebSiteEngine _engine;
        private readonly WebSitesController _controller;

        public WebsitesControllerTest() 
        {
            _repository = new Mock<IWebSiteRepository>();
            _engine = new WebSiteEngine(_repository.Object);
            _controller = new WebSitesController(_engine);
        }

        [SetUp]
        public void Setup()
        {
            List<WebSite> data = getSampleData();
            //_repository.Setup(x => x.GetList()).Returns(data);
        }

        [Test]
        public void GetAllWebsites_ReturnsResult()
        {
            //arrange
            var data = getSampleData();

            //act
            var result = _controller.GetWebSites();

            //assert
            
        }

        [Test]
        public void GetAllWebsites_ReturnsAllItems()
        {
            //arrange
            var data = getSampleData();
            int count = data.Count;

            //act
            var result = _controller.GetWebSites();
            
            //assert
            Assert.AreEqual(count, result.Count);
        }

        private List<WebSite> getSampleData()
        {
            List<WebSite> websites = new List<WebSite>
            {
                new WebSite()
                {
                    Name = "kariyer",
                    WebSiteCookieTypeDefinitions = new List<WebSiteCookieTypeDefinition>()
                    {
                        new WebSiteCookieTypeDefinition()
                        {
                            CookieType = "performance",
                            Title = "performans",
                            Description = "kullanıcı geçmişinden faydalanarak kullanıcının site performansını artırır",
                            IsRequired = true,
                            IsActive = true,
                            Cookies = new List<Cookie>()
                            {
                                new Cookie()
                                {
                                    SessionId = "13ad",
                                    Status = true,
                                },
                                new Cookie()
                                {
                                    SessionId = "adf31",
                                    Status = true,
                                },
                                new Cookie()
                                {
                                    SessionId = "113s",
                                    Status = true,
                                }
                            },
                        },
                        new WebSiteCookieTypeDefinition()
                        {
                            CookieType = "targeting",
                            Title = "...",
                            Description = "........",
                            IsRequired = false,
                            IsActive = true,
                            Cookies = new List<Cookie>()
                            {
                                new Cookie()
                                {
                                    SessionId = "dsg223d",
                                    Status = false,
                                },
                                new Cookie()
                                {
                                    SessionId = "jgjg222",
                                    Status = false,
                                },
                                new Cookie()
                                {
                                    SessionId = "fejg4212",
                                    Status = true,
                                }
                            },
                        }
                    }
                },
                new WebSite()
                {
                    Name = "coensio",
                    WebSiteCookieTypeDefinitions = new List<WebSiteCookieTypeDefinition>()
                    {
                        new WebSiteCookieTypeDefinition()
                        {
                            CookieType = "performance",
                            Title = "performans",
                            Description = "kullanıcı geçmişinden faydalanarak kullanıcının site performansını artırır",
                            IsRequired = false,
                            IsActive = false,
                            Cookies = new List<Cookie>()
                            {
                                new Cookie()
                                {
                                    SessionId = "fghjj6642224",
                                    Status = true,
                                },
                                new Cookie()
                                {
                                    SessionId = "fzzzaf334",
                                    Status = false,
                                },
                                new Cookie()
                                {
                                    SessionId = "fixdwqyox325",
                                    Status = false,
                                }
                            },
                        },
                        new WebSiteCookieTypeDefinition()
                        {
                            CookieType = "Functional",
                            Title = "...",
                            Description = "........",
                            IsRequired = true,
                            IsActive = true,
                            Cookies = new List<Cookie>()
                            {
                                new Cookie()
                                {
                                    SessionId = "skdjgg43281b",
                                    Status = true,
                                },
                                new Cookie()
                                {
                                    SessionId = "dvhjrr88sbbcx",
                                    Status = true,
                                },
                                new Cookie()
                                {
                                    SessionId = "iowqhvba2221",
                                    Status = true,
                                }
                            },
                        }
                    }
                },
                new WebSite()
                {
                    Name = "iskolig",
                    WebSiteCookieTypeDefinitions = new List<WebSiteCookieTypeDefinition>()
                    {
                        new WebSiteCookieTypeDefinition()
                        {
                            CookieType = "Functional",
                            Title = "...",
                            Description = "........",
                            IsRequired = true,
                            IsActive = false,
                        }
                    }
                },
                new WebSite()
                {
                    Name = "unisbul"
                }
            };

            return websites;
        }
    }
}