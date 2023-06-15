using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Core.Contracts.Entities;
using Product.Core.Module;



namespace Product.Core.DataAccess
{
    /// <summary>
    /// The Entity Framework implementation of IUnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The DbContext
        /// </summary>
        private DbContext _dbContext;


        private readonly CurrentUser _currentUser;


        protected Dictionary<string, object> _repositories = new Dictionary<string, object>();


        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="context">The object context</param>
        public UnitOfWork(DbContext context, CurrentUser currentUser)
        {
            _dbContext = context;
            _currentUser = currentUser;

        }



        /// <summary>
        /// Gets an instance of repository of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IRepository<T> Repository<T>() where T : StrongEntity
        {
            try
            {
                var key = typeof(T).FullName;
                if (_repositories.ContainsKey(key))
                    return _repositories[key] as IRepository<T>;
                else
                {
                    var repository = new EFRepository<T>(_dbContext, _currentUser);
                    _repositories[key] = repository;
                    return repository;
                }
            }
            catch (Exception ex)
            {

                // add message
                throw new Exception("Make sure DbSet<entity> added in dbContext and EntityMap exists " + ex.Message);
            }

            throw new Exception("The corresponding repository was not found in the unit of work." +
                " It might be because the repository is not instantiated in the unit of work.");
        }



        public CurrentUser GetCurrentUser()
        {
            return _currentUser;
        }


        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing && _dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
        }

        public int Commit()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (Exception dbEx)
            {
                throw dbEx;
            }

        }
        public string GetConnStr()
        {
            return _dbContext.Database.GetDbConnection().ConnectionString;
        }
        public DbContext GetContext()
        {
            return _dbContext;
        }
    }
}
