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
    public class CategoryRepository : AbstractRepository, IRepository<Category>
    {
        public CategoryRepository(IdealContext db)
            : base(db) { }  

        public Category Add(Category entity)
        {
            db.Categories.Add(entity);
            return entity;
        }

        public async Task<Category> AddAsync(Category entity)
        {
            await db.Categories.AddAsync(entity);
            return entity;
        }

        public IEnumerable<Category> AddRange(IEnumerable<Category> entities)
        {
            db.Categories.AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<Category>> AddRangeAsync(IEnumerable<Category> entities)
        {
            await db.Categories.AddRangeAsync(entities);
            return entities;
        }

        public void Delete(Category entity)
        {
            db.Categories.Remove(entity);
        }

        public Category Edit(Category entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<Category> GetAll(Expression<Func<Category, bool>> predicate = null)
        {
            if (predicate!= null)
            {
                return db.Categories.Where(predicate);
            }
            return db.Categories.AsQueryable();
        }

        public async Task<ICollection<Category>> GetAllAsync(Expression<Func<Category, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await db.Categories.Where(predicate).ToListAsync();
            }
            return await db.Categories.ToListAsync();
        }

        public Category GetByCode(string id)
        {
            return db.Categories.Find(id);
        }

        public Category GetById(int id)
        {
            return db.Categories.Find(id);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await db.Categories.FindAsync(id);
        }

        public Category Update(Category entity)
        {
            db.Update(entity);
            return entity;
        }

        public IEnumerable<Category> UpdateRange(IEnumerable<Category> entities)
        {
            db.UpdateRange(entities);
            return entities;
        }
    }
}
