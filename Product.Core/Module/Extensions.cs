using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Product.Core.Contracts.DTOs;
using Product.Core.Contracts.Entities;

namespace Product.Core.Module
{
    public static class Extensions
    {
        #region Queryable Extensions

        public static IMapper Mapper { get; set; }
        public static string ConnStr { get; set; }
        public static PaginatedResult<T> ToPaginatedResult<T>(this IOrderedQueryable<T> query,
            PaginatedRequest request)
            where T : class
        {
            var dsResult = query.Skip(request.Skip).Take(request.Take).AsNoTracking().ToList();

            var toReturn = new PaginatedResult<T>
            {
                Items = dsResult,
                TotalCount = query.Count(),
                CurrentPage = request.Skip,
                PageSize = request.Take
            };
            return toReturn;
        }
        public static PaginatedResult<TDTO> ToDTOPaginatedResult<TEntity, TDTO>(this IOrderedQueryable<TEntity> query,
            PaginatedRequest request)
            where TEntity : StrongEntity
            where TDTO : class // not important to be BaseDTO
        {

            var projectQuery = query.ProjectTo<TDTO>(Mapper.ConfigurationProvider);

            var dsResult = projectQuery.Skip(request.Skip).Take(request.Take).AsNoTracking().ToList();
            var toReturn = new PaginatedResult<TDTO>
            {
                Items = dsResult.ToList(),
                TotalCount = projectQuery.Count(),
                CurrentPage = request.Skip,
                PageSize = request.Take
            };
            return toReturn;
        }

        public static PaginatedResult<TDTO> ToDTOPaginatedResult<TEntity, TDTO>(this IQueryable<TEntity> query,
            PaginatedRequest request)
            where TEntity : StrongEntity
            where TDTO : class
        {
            query = query.OrderByDescending(x => x.Id);
            var projectQuery = query.ProjectTo<TDTO>(Mapper.ConfigurationProvider);
            var dsResult = projectQuery.Skip(request.Skip).Take(request.Take).AsNoTracking().ToList();
            var toReturn = new PaginatedResult<TDTO>
            {
                Items = dsResult.ToList(),
                TotalCount = projectQuery.Count(),
                CurrentPage = request.Skip,
                PageSize = request.Take
            };
            return toReturn;
        }
        #endregion

        #region Mapping Extensions

        public static TDTO ToDTO<TDTO>(this StrongEntity entity)
            where TDTO : class
        {
            if (entity == null)
                return null;
            return Mapper.Map<TDTO>(entity);
        }

        public static TEntity ToEntity<TEntity>(this StrongEntityDTO dto)
            where TEntity : StrongEntity
        {
            if (dto == null)
                return default(TEntity);
            return Mapper.Map<TEntity>(dto);
        }

        public static List<TDTO> ToDTOList<TDTO>(this IQueryable<StrongEntity> entityList)
            where TDTO : class
        {
            if (entityList == null)
                return null;
            return entityList.ProjectTo<TDTO>(Mapper.ConfigurationProvider).ToList();
        }

        public static List<TEntity> ToEntityList<TEntity>(this IEnumerable<StrongEntityDTO> dtoList)
            where TEntity : StrongEntity
        {
            if (dtoList == null)
                return null;
            return dtoList.Select(e => e.ToEntity<TEntity>()).ToList();
        }



        private static TDest Map<TDest>(object obj)
        {
            return Mapper.Map<TDest>(obj);
        }


        public static List<TDTO> DataReaderToEntityList<TDTO>(this DbDataReader dr) where TDTO : class
        {

            List<TDTO> list = new List<TDTO>();
            TDTO obj = default(TDTO);

            //if (!dr.HasRows)
            //    return list;
            try
            {
                while (dr.Read())
                {
                    obj = Activator.CreateInstance<TDTO>();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        if (dr[i] is DBNull)
                            continue;
                        obj.GetType().GetProperty(dr.GetName(i)).SetValue(obj, dr[i], null);
                    }
                    list.Add(obj);
                }
            }
            catch (Exception ex) { }
            return list;
        }


        #endregion




    }

}
