using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerNet.CookieManager.Common.Data
{
    public interface IEntity<T> where T : struct
    {
        public T Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
