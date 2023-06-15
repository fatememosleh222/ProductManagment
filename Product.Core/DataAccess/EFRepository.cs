using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Product.Core.Contracts.Entities;
using Product.Core.Module;

namespace Product.Core.DataAccess
{
    public class EFRepository<T> : IRepository<T> where T : StrongEntity
    {
        private readonly DbContext _context;
        protected DbSet<T> Entities { get; private set; }
        private CurrentUser _currentUser { get; set; }


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EFRepository(DbContext context, CurrentUser currentUser)
        {
            this._context = context;
            this.Entities = _context.Set<T>();
            this._currentUser = currentUser;

        }

        //public virtual IQueryable<T> Table => _context.Set<T>();
        //public virtual IQueryable<T> TableNoTracking => this.Entities.AsNoTracking();

        public T GetByPK(params object[] keyValues)
        {
            var entity = Entities.Find(keyValues);
            return entity;
        }


        public virtual void Insert(T entity)
        {
            if (entity != null)
            {
                entity.CreatorId = _currentUser.ID;
            }

            this.Entities.Add(entity);
        }


        public virtual void Update(T entity)
        {
            _context.Entry(entity).Property(x => x.CreatorId).IsModified = false;
            this.Entities.Update(entity);
        }


        public virtual void Delete(T entity)
        {
            entity.IsDelete = true;
            Update(entity);
        }


        public void DeleteByPK(params object[] keyValues)
        {
            var entityToDelete = Entities.Find(keyValues);
            Delete(entityToDelete);
        }

        public List<T> GetAll()
        {
            var query = Entities.Where(x => !x.IsDelete);
            return query.ToList();
        }


        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null, bool includeDeleted = false)
        {
            IQueryable<T> query = Entities;

            if (!includeDeleted)
            {
                query = query.Where(x => !x.IsDelete);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query;
        }



        public PaginatedResult<T> QueryPaginated(PaginatedRequest request, IQueryable<T> query = null)
        {
            if (query == null)
                query = this.Entities.AsQueryable();
            //if (_checkTenant)
            //{
            //    query = query.Where(x => x.TenantId == _currentUser.TenantId);
            //}
            var dsResult = query.OrderByDescending(x => x.Id).ToPaginatedResult(request);
            return dsResult;
        }




    }
}
