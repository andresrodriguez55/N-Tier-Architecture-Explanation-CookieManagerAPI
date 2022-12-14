using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerNet.CookieManager.Common.Data
{
    public abstract class BaseEntity<T> : IEntity where T : struct
    {
        public T Id { get; set; }
    }
}
