using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class TestWebsiteController
    {
        public TestWebsiteController()
        {
            var options = new DbContextOptionsBuilder<CookieSettingsContext>().UseInMemoryDatabase(databaseName: "").Options;
        }

        [TestMethod]
        public void GetAllWebsites()
        {
            // var controller = new WebsiteController(); 
        }
    }
}
