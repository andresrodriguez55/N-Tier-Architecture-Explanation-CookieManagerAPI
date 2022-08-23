using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerNet.CookieManager.Common.Data
{
    public interface IHasCreatedDateEntity
    {
        DateTime CreatedDate { get; set; }
    }
}
