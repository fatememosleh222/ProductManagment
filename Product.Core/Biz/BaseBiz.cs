using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Product.Core.Contracts.DTOs;
using Product.Core.Contracts.Entities;
using Product.Core.DataAccess;
using Product.Core.Module;

namespace Product.Core.Biz
{
    public class BaseBiz<TEntity, TDTO>
        where TEntity : BaseEntity
        where TDTO : BaseEntityDTO
    {
        const string DtoPostfix = "DTO";

        public string ConnStr;
        private IRepository<TEntity> _repository;
        protected IUnitOfWork UnitOfWork;

        protected CurrentUser CurrentUser => UnitOfWork.GetCurrentUser();


        //protected byte[] GetbyteArray(HttpPostedFile file)
        //{
        //    byte[] fileData = null;
        //    using (var binaryReader = new BinaryReader(file.InputStream))
        //    {
        //        fileData = binaryReader.ReadBytes(file.ContentLength);
        //    }
        //    return fileData;
        //}
        public BaseBiz(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
            //this._repository = repository;
            this._repository = unitOfWork.Repository<TEntity>();
        }

        public void Commit()
        {
            UnitOfWork.Commit();
        }


        public virtual List<TDTO> GetAll()
        {
            return _repository.Get().ToDTOList<TDTO>();
        }


        //public List<TDTO> GetAll<TProperty>(Expression<Func<TDTO, TProperty>>[] includeProperties = null)
        //{
        //    return Repository.GetAll<TEntity>(GetInclustionString(includeProperties)).ToDTOList<TDTO>();
        //}


        public virtual void DeleteByPk(params object[] keyValues)
        {
            _repository.DeleteByPK(keyValues);
            UnitOfWork.Commit();
        }

        public virtual void Delete(TDTO dto, bool commit = true)
        {
            _repository.Delete(dto.ToEntity<TEntity>());
            if (commit)
                UnitOfWork.Commit();
        }


        public virtual TDTO GetByPK(params object[] keyValues)
        {

            return _repository.GetByPK(keyValues).ToDTO<TDTO>();
        }


        public virtual void Insert(TDTO newDto, bool commit = true)
        {
            var entity = newDto.ToEntity<TEntity>();
            _repository.Insert(entity);
            if (commit)
            {
                UnitOfWork.Commit();

            }
        }

        public virtual TDTO InsertAndReturn(TDTO newDto)
        {
            var entity = newDto.ToEntity<TEntity>();
            _repository.Insert(entity);

            UnitOfWork.Commit();
            return entity.ToDTO<TDTO>();


        }

        public virtual void Update(TDTO dto, bool commit = true)
        {
            var entity = dto.ToEntity<TEntity>();
            _repository.Update(entity);
            if (commit)
                UnitOfWork.Commit();

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (UnitOfWork != null)
                {
                    UnitOfWork.Dispose();
                    UnitOfWork = null;
                }
            }
        }
        public PaginatedResult<TDTO> GetAllPaginated(PaginatedRequest param)
        {
            var data = _repository.Get();
            return data.ToDTOPaginatedResult<TEntity, TDTO>(param);
        }

        protected PaginatedResult<TDTO> GetOrderedPaginated(PaginatedRequest param, IOrderedQueryable<TEntity> filter)
        {
            return filter.ToDTOPaginatedResult<TEntity, TDTO>(param);
        }
        protected PaginatedResult<TDTO> GetPaginated(PaginatedRequest param, IQueryable<TEntity> filter)
        {
            return filter.ToDTOPaginatedResult<TEntity, TDTO>(param);
        }




        protected IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            return _repository.Get(filter);
        }

        protected IQueryable<R> Get<R>(Expression<Func<R, bool>> filter = null)
        where R : BaseEntity
        {
            return UnitOfWork.Repository<R>().Get(filter);
        }

        protected List<TDTO> ToDTOList(IQueryable<TEntity> data)
        {
            return data.ToDTOList<TDTO>();
        }


    }
}
