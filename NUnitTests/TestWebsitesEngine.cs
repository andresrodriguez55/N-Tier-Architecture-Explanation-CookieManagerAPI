using CookiesSettings.Models;
using KariyerNet.CookieManager.Business;
using KariyerNet.CookieManager.Business.BusinessEngine;
using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using KariyerNet.CookieManager.Data.Contract.Repository;
using Moq;

namespace NUnitTests
{
    public class TestWebsitesEngine
    {
        private readonly Mock<IWebSiteRepository> _repository;
        private readonly IWebSiteEngine _engine;

        public TestWebsitesEngine() 
        {
            _repository = new Mock<IWebSiteRepository>();
            _engine = new WebSiteEngine(_repository.Object);
        }

        [SetUp]
        public void Setup()
        {
            List<WebSite> data = getSampleData();
            _repository.Setup(x => x.ListAll()).Returns(data);
        }

        [Test]
        public void GetAllWebsites()
        {
            //arrange

            //act
            var dataEngine = _engine.GetWebSites();
            var data = getSampleData();

            //assert
            Assert.AreEqual(dataEngine.Count, data.Count);
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