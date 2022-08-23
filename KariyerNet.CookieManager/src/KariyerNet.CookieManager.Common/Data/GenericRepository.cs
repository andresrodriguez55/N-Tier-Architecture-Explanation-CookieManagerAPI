using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace KariyerNet.CookieManager.Common.Data
{
    public class GenericRepository<T, PK> : IGenericRepository<T, PK> where T : BaseEntity<PK>, new() where PK : struct
    {
        private readonly DbContext _context;
        protected DbSet<T> DbSet { get; }

        public GenericRepository(DbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public T GetById(PK id)
        {
            return DbSet.FirstOrDefault(e => e.Id.Equals(id));
        }


        public void Create(T entity) 
        {
            DbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
            _context.SaveChanges();
        }

        public bool Exists(Expression<Func<T, bool>> filter = null)
        {
            return (filter == null) ? DbSet.Any() : DbSet.Any(filter);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            return DbSet.FirstOrDefault(filter);
        }

        public TResult GetFirstOrDefault<TResult>(Expression<Func<T, bool>> filter) where TResult : class, new()
        {
            IQueryable<T> query = DbSet;

            return query.Where(filter).ProjectToType<TResult>().FirstOrDefault();
        }

        public List<T> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? topRecords = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet;

            if(filter != null)
                query = query.Where(filter);
            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include<T, object>(include);
            if (orderBy != null)
                query = orderBy(query); 
            if (topRecords != null)
                query = query.Take((int) topRecords);

            var result = query.ToList<T>();
            return result;
        }

        public List<TResult> GetList<TResult>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? topRecords = null, params Expression<Func<T, object>>[] includes) where TResult : class, new()
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
                query = query.Where(filter);
            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include<T, object>(include);
            if (orderBy != null)
                query = orderBy(query);
            if (topRecords != null)
                query = query.Take((int)topRecords);

            return query.ProjectToType<TResult>().ToList();
        }

        public int RecordCount(Expression<Func<T, bool>> filter = null)
        {
            return (filter == null) ? DbSet.Count() : DbSet.Count(filter);
        }

        public TResult GetFirstOrDefault<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> filter) where TResult : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
