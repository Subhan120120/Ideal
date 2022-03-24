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
    public class CustomerRepository : AbstractRepository, IRepository<Customer>
    {
        public CustomerRepository(IdealContext db)
            : base(db) { }

        public Customer Add(Customer entity)
        {
            db.Customers.Add(entity);
            return entity;
        }

        public async Task<Customer> AddAsync(Customer entity)
        {
            await db.Customers.AddAsync(entity);
            return entity;
        }

        public IEnumerable<Customer> AddRange(IEnumerable<Customer> entities)
        {
            db.Customers.AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<Customer>> AddRangeAsync(IEnumerable<Customer> entities)
        {
            await db.Customers.AddRangeAsync(entities);
            return entities;
        }

        public void Delete(Customer entity)
        {
            db.Customers.Remove(entity);
        }

        public Customer Edit(Customer entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<Customer> GetAll(Expression<Func<Customer, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return db.Customers.Where(predicate);
            }
            return db.Customers.AsQueryable();
        }

        public async Task<ICollection<Customer>> GetAllAsync(Expression<Func<Customer, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await db.Customers.Where(predicate).ToListAsync();
            }
            return await db.Customers.ToListAsync();
        }

        public Customer GetByCode(string id)
        {
            return db.Customers.Find(id);
        }

        public Customer GetById(int id)
        {
            return db.Customers.Find(id);
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await db.Customers.FindAsync(id);
        }

        public Customer Update(Customer entity)
        {
            db.Update(entity);
            return entity;
        }

        public IEnumerable<Customer> UpdateRange(IEnumerable<Customer> entities)
        {
            db.UpdateRange(entities);
            return entities;
        }
    }
}
