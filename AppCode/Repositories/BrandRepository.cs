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
    public class BrandRepository : AbstractRepository, IRepository<Brand>
    {
        public BrandRepository(IdealContext db)
            : base(db) { }

        public Brand Add(Brand entity)
        {
            db.Brands.Add(entity);
            return entity;
        }

        public async Task<Brand> AddAsync(Brand entity)
        {
            await db.Brands.AddAsync(entity);
            return entity;
        }

        public IEnumerable<Brand> AddRange(IEnumerable<Brand> entities)
        {
            db.Brands.AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<Brand>> AddRangeAsync(IEnumerable<Brand> entities)
        {
            await db.Brands.AddRangeAsync(entities);
            return entities;
        }

        public void Delete(Brand entity)
        {
            db.Brands.Remove(entity);
        }

        public Brand Edit(Brand entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<Brand> GetAll(Expression<Func<Brand, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return db.Brands.Where(predicate);
            }
            return db.Brands.AsQueryable();
        }

        public async Task<ICollection<Brand>> GetAllAsync(Expression<Func<Brand, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await db.Brands.Where(predicate).ToListAsync();
            }
            return await db.Brands.ToListAsync();
        }

        public Brand GetById(int id)
        {
            return db.Brands.Find(id);
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await db.Brands.FindAsync(id);
        }

        public Brand Update(Brand entity)
        {
            db.Update(entity);
            return entity;
        }

        public IEnumerable<Brand> UpdateRange(IEnumerable<Brand> entities)
        {
            db.UpdateRange(entities);
            return entities;
        }
    }
}
