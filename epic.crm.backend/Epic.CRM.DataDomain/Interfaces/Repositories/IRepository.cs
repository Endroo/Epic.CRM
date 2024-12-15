﻿using Epic.CRM.DataDomain.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(FindOptions? findOptions = null);
        TEntity FindOne(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null);
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddMany(IEnumerable<TEntity> entities);
        Task AddManyAsync(IEnumerable<TEntity> entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteMany(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        int Count(Expression<Func<TEntity, bool>> predicate);
    }
}