using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KariyerNet.CookieManager.Common.Data
{
    public interface IGenericRepository<T, PK> where T : BaseEntity<PK>, new() where PK : struct
    {
        T GetById(PK id); 
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        TResult GetFirstOrDefault<TResult>(Expression<Func<T, bool>> filter) where TResult : class, new();
        TResult GetFirstOrDefault<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> filter) where TResult : class, new();
        int RecordCount(Expression<Func<T, bool>> filter = null);
        bool Exists(Expression<Func<T, bool>> filter = null);
        List<T> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? topRecords = null, params Expression<Func<T, object>>[] includes);
        List<TResult> GetList<TResult>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? topRecords = null, params Expression<Func<T, object>>[] includes) where TResult : class, new();
    }
}
