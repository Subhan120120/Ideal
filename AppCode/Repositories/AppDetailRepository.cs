using Microsoft.EntityFrameworkCore;
using QonaqWebApp.AppCode.Infrastructure;
using QonaqWebApp.Models.Context;
using QonaqWebApp.Models.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QonaqWebApp.AppCode.Repositories
{
    public class AppDetailRepository : AbstractRepository, IRepository<AppDetail>
    {
        public AppDetailRepository(IdealContext db)
            : base(db) { }

        public AppDetail Add(AppDetail entity)
        {
            db.AppDetails.Add(entity);
            return entity;
        }

        public async Task<AppDetail> AddAsync(AppDetail entity)
        {
            await db.AppDetails.AddAsync(entity);
            return entity;
        }

        public IEnumerable<AppDetail> AddRange(IEnumerable<AppDetail> entities)
        {
            db.AppDetails.AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<AppDetail>> AddRangeAsync(IEnumerable<AppDetail> entities)
        {
            await db.AppDetails.AddRangeAsync(entities);
            return entities;
        }

        public void Delete(AppDetail entity)
        {
            db.AppDetails.Remove(entity);
        }

        public AppDetail Edit(AppDetail entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<AppDetail> GetAll(Expression<Func<AppDetail, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return db.AppDetails.Where(predicate);
            }
            return db.AppDetails.AsQueryable();
        }

        public async Task<ICollection<AppDetail>> GetAllAsync(Expression<Func<AppDetail, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await db.AppDetails.Where(predicate).ToListAsync();
            }
            return await db.AppDetails.ToListAsync();
        }

        public AppDetail GetByCode(string id)
        {
            return db.AppDetails.Find(id);
        }

        public AppDetail GetById(int id)
        {
            return db.AppDetails.Find(id);
        }

        public async Task<AppDetail> GetByIdAsync(int id)
        {
            return await db.AppDetails.FindAsync(id);
        }

        public AppDetail Update(AppDetail entity)
        {
            db.Update(entity);
            return entity;
        }

        public IEnumerable<AppDetail> UpdateRange(IEnumerable<AppDetail> entities)
        {
            db.UpdateRange(entities);
            return entities;
        }
    }
}
