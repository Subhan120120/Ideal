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
    public class ReservationRepository : AbstractRepository, IRepository<Reservation>
    {
        public ReservationRepository(IdealContext db)
    : base(db) { }

        public Reservation Add(Reservation entity)
        {
            db.Reservations.Add(entity);
            return entity;
        }

        public async Task<Reservation> AddAsync(Reservation entity)
        {
            await db.Reservations.AddAsync(entity);
            return entity;
        }

        public IEnumerable<Reservation> AddRange(IEnumerable<Reservation> entities)
        {
            db.Reservations.AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<Reservation>> AddRangeAsync(IEnumerable<Reservation> entities)
        {
            await db.Reservations.AddRangeAsync(entities);
            return entities;
        }

        public void Delete(Reservation entity)
        {
            db.Reservations.Remove(entity);
        }

        public Reservation Edit(Reservation entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<Reservation> GetAll(Expression<Func<Reservation, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return db.Reservations.Where(predicate);
            }
            return db.Reservations.AsQueryable();
        }

        public async Task<ICollection<Reservation>> GetAllAsync(Expression<Func<Reservation, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await db.Reservations.Where(predicate).ToListAsync();
            }
            return await db.Reservations.ToListAsync();
        }

        public Reservation GetByCode(string id)
        {
            return db.Reservations.Find(id);
        }

        public Reservation GetById(int id)
        {
            return db.Reservations.Find(id);
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await db.Reservations.FindAsync(id);
        }

        public Reservation Update(Reservation entity)
        {
            db.Update(entity);
            return entity;
        }

        public IEnumerable<Reservation> UpdateRange(IEnumerable<Reservation> entities)
        {
            db.UpdateRange(entities);
            return entities;
        }
    }
}
