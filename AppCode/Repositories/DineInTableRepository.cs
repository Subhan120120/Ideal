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
    public class DineInTableRepository : AbstractRepository, IRepository<DineInTable>
    {
        public DineInTableRepository(IdealContext db)
    : base(db) { }

        public DineInTable Add(DineInTable entity)
        {
            db.DineInTables.Add(entity);
            return entity;
        }

        public async Task<DineInTable> AddAsync(DineInTable entity)
        {
            await db.DineInTables.AddAsync(entity);
            return entity;
        }

        public IEnumerable<DineInTable> AddRange(IEnumerable<DineInTable> entities)
        {
            db.DineInTables.AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<DineInTable>> AddRangeAsync(IEnumerable<DineInTable> entities)
        {
            await db.DineInTables.AddRangeAsync(entities);
            return entities;
        }

        public void Delete(DineInTable entity)
        {
            db.DineInTables.Remove(entity);
        }

        public DineInTable Edit(DineInTable entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<DineInTable> GetAll(Expression<Func<DineInTable, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return db.DineInTables.Where(predicate);
            }
            return db.DineInTables.AsQueryable();
        }

        public async Task<ICollection<DineInTable>> GetAllAsync(Expression<Func<DineInTable, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await db.DineInTables.Where(predicate).ToListAsync();
            }
            return await db.DineInTables.ToListAsync();
        }

        public DineInTable GetByCode(string id)
        {
            return db.DineInTables.Find(id);
        }

        public DineInTable GetById(int id)
        {
            return db.DineInTables.Find(id);
        }

        public async Task<DineInTable> GetByIdAsync(int id)
        {
            return await db.DineInTables.FindAsync(id);
        }

        public DineInTable Update(DineInTable entity)
        {
            db.Update(entity);
            return entity;
        }

        public IEnumerable<DineInTable> UpdateRange(IEnumerable<DineInTable> entities)
        {
            db.UpdateRange(entities);
            return entities;
        }
    }
}
