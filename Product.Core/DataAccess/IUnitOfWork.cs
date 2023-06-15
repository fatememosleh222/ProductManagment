using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Core.Contracts.Entities;
using Product.Core.Module;

namespace Product.Core.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {

        IRepository<T> Repository<T>() where T : StrongEntity;

        string GetConnStr();
        CurrentUser GetCurrentUser();
        DbContext GetContext();
        int Commit();
    }
}
