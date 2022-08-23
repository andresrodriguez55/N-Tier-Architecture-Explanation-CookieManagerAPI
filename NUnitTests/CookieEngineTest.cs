using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KariyerNet.CookieManager.Business.BusinessEngine;
using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using KariyerNet.CookieManager.Data.Contract.Repository;
using Moq;

namespace NUnitTests
{
    public class CookieEngineTest
    {
        private readonly Mock<ICookieRepository> _repository;
        private readonly ICookieEngine _engine;

        public CookieEngineTest()
        {
            _repository = new Mock<ICookieRepository>();
            //_engine = new CookieEngine(_repository.Object);
        }
    }
}
