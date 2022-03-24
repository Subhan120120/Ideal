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
    public class ProductRepository : AbstractRepository, IRepository<Product>
    {
        public ProductRepository(IdealContext db)
            : base(db) { }


        public Product Add(Product entity)
        {
            db.Products.Add(entity);
            return entity;
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await db.Products.AddAsync(entity);
            return entity;
        }

        public IEnumerable<Product> AddRange(IEnumerable<Product> entities)
        {
            db.Products.AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await db.Products.AddRangeAsync(entities);
            return entities;
        }

        public void Delete(Product entity)
        {
            db.Products.Remove(entity);
        }

        public Product Edit(Product entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<Product> GetAll(Expression<Func<Product, bool>> predicate = null)
        {
            if (predicate!=null)
            {
                return db.Products.Where(predicate);
            }
            return db.Products.AsQueryable();
        }

        public async Task<ICollection<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await db.Products.Where(predicate).ToListAsync();
            }
            return await db.Products.ToListAsync();
        }

        public Product GetByCode(string id)
        {
            return db.Products.Find(id);
        }

        public Product GetById(int id)
        {
            return db.Products.Find(id);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await db.Products.FindAsync(id);
        }

        public Product Update(Product entity)
        {
            db.Update(entity);
            return entity;
        }

        public IEnumerable<Product> UpdateRange(IEnumerable<Product> entities)
        {
            db.UpdateRange(entities);
            return entities;
        }
    }
}
