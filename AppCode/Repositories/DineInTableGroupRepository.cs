using Microsoft.EntityFrameworkCore;
using QonaqWebApp.AppCode.Infrastructure;
using QonaqWebApp.Models.Context;
using QonaqWebApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QonaqWebApp.AppCode.Repositories
{
    public class DineInTableGroupRepository : AbstractRepository, IRepository<DineInTableGroup>
    {
        public DineInTableGroupRepository(IdealContext db)
            : base(db) { }

        public DineInTableGroup Add(DineInTableGroup entity)
        {
            db.DineInTableGroups.Add(entity);
            return entity;
        }

        public async Task<DineInTableGroup> AddAsync(DineInTableGroup entity)
        {
            await db.DineInTableGroups.AddAsync(entity);
            return entity;
        }

        public IEnumerable<DineInTableGroup> AddRange(IEnumerable<DineInTableGroup> entities)
        {
            db.DineInTableGroups.AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<DineInTableGroup>> AddRangeAsync(IEnumerable<DineInTableGroup> entities)
        {
            await db.DineInTableGroups.AddRangeAsync(entities);
            return entities;
        }

        public void Delete(DineInTableGroup entity)
        {
            db.DineInTableGroups.Remove(entity);
        }

        public DineInTableGroup Edit(DineInTableGroup entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<DineInTableGroup> GetAll(Expression<Func<DineInTableGroup, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return db.DineInTableGroups.Where(predicate);
            }
            return db.DineInTableGroups.AsQueryable();
        }

        public async Task<ICollection<DineInTableGroup>> GetAllAsync(Expression<Func<DineInTableGroup, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await db.DineInTableGroups.Where(predicate).ToListAsync();
            }
            return await db.DineInTableGroups.ToListAsync();
        }

        public DineInTableGroup GetByCode(string id)
        {
            throw new NotImplementedException();
        }

        public DineInTableGroup GetById(int id)
        {
            return db.DineInTableGroups.Find(id);
        }

        public async Task<DineInTableGroup> GetByIdAsync(int id)
        {
            return await db.DineInTableGroups.FindAsync(id);
        }

        public DineInTableGroup Update(DineInTableGroup entity)
        {
            db.Update(entity);
            return entity;
        }

        public IEnumerable<DineInTableGroup> UpdateRange(IEnumerable<DineInTableGroup> entities)
        {
            db.UpdateRange(entities);
            return entities;
        }
    }
}
