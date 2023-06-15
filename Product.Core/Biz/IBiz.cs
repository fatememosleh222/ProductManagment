using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.Core.Contracts.Entities;

namespace Product.Core.Biz
{
	public interface IBiz<TDTO> : IDisposable
	{
		void Delete(TDTO dto, bool commit = true);
		void DeleteByPk(params object[] keyValues);
		List<TDTO> GetAll();
		TDTO GetByPK(params object[] keyValues);
		//TDTO GetByPKNoTracking(params object[] keyValues);
		//TDTO GetByUId(Guid id);
		//void DeleteByUId(Guid id);
        void Insert(TDTO newDto, bool commit = true);
		void Update(TDTO dto, bool commit = true);
	    //TDTO UpdateAndReturn(TDTO dto);
	    TDTO InsertAndReturn(TDTO dto);
	    //bool BulkInsert(List<TDTO> dtos, out string errorMessage);
	    //R BulkAction<R>(Func<R> func, bool transactional, out string errorMessage);
        PaginatedResult<TDTO> GetAllPaginated(PaginatedRequest param);

	}
}
