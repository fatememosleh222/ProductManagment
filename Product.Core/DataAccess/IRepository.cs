using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Product.Core.Contracts.Entities;

namespace Product.Core.DataAccess
{
	public interface IRepository<T> where T : StrongEntity
	{
		T GetByPK(params object[] keyValues);
		void Insert(T newEntity);
		void Update(T entity);
		void Delete(T entity);
		void DeleteByPK(params object[] keyValues);
        List<T> GetAll();
		IQueryable<T> Get(Expression<Func<T, bool>> filter = null, bool includeDeleted = false);
        PaginatedResult<T> QueryPaginated(PaginatedRequest request, IQueryable<T> query = null);

     //   IQueryable<T> Table { get; }

	    ///// <summary>
	    ///// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
	    ///// </summary>
	    //IQueryable<T> TableNoTracking { get; }
    }
}
