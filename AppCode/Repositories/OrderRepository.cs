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
    public class OrderRepository : AbstractRepository, IRepository<Order>
    {
        public OrderRepository(IdealContext db)
            : base(db) { }

        public Order Add(Order entity)
        {
            db.Orders.Add(entity);
            return entity;
        }

        public async Task<Order> AddAsync(Order entity)
        {
            await db.Orders.AddAsync(entity);
            return entity;
        }

        public IEnumerable<Order> AddRange(IEnumerable<Order> entities)
        {
            db.Orders.AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<Order>> AddRangeAsync(IEnumerable<Order> entities)
        {
            await db.Orders.AddRangeAsync(entities);
            return entities;
        }

        public void Delete(Order entity)
        {
            db.Orders.Remove(entity);
        }

        public Order Edit(Order entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<Order> GetAll(Expression<Func<Order, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return db.Orders.Where(predicate);
            }
            return db.Orders.AsQueryable();
        }

        public async Task<ICollection<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await db.Orders.Where(predicate).ToListAsync();
            }
            return await db.Orders.ToListAsync();
        }

        public Order GetByCode(string id)
        {
            return db.Orders.Find(id);
        }

        public Order GetById(int id)
        {
            return db.Orders.Find(id);
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await db.Orders.FindAsync(id);
        }

        public Order Update(Order entity)
        {
            db.Update(entity);
            return entity;
        }

        public IEnumerable<Order> UpdateRange(IEnumerable<Order> entities)
        {
            db.UpdateRange(entities);
            return entities;
        }
    }
}
