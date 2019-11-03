using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Threading.Tasks;
using flight_planner.core.Models;
using flight_planner.core.Services;
using flight_planner.data;
using Microsoft.SqlServer.Server;

namespace flight_planner.services
{
    public class DbService : IDbService
    {
        protected readonly IFlightPlannerDbContext _ctx;
        public DbService(IFlightPlannerDbContext context)
        {
            _ctx = context;
        }
        public ServiceResult Create<T>(T entity) where T : Entity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _ctx.Set<T>().Add(entity);
            _ctx.SaveChanges();

            return new ServiceResult(succeeded:true).Set(entity);
        }
        public ServiceResult Delete<T>(T entity) where T : Entity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _ctx.Set<T>().Remove(entity);
            _ctx.SaveChanges();

            return new ServiceResult(succeeded: true);
        }
        public bool Exists<T>(int id) where T : Entity
        {
            return QueryById<T>(id).Any();
        }
        public IEnumerable<T> Get<T>() where T : Entity
        {
            return Query<T>().ToList();
        }
        public virtual async Task<T> GetById<T>(int id) where T : Entity
        {
            return await _ctx.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }
        public IQueryable<T> Query<T>() where T : Entity
        {
            return _ctx.Set<T>().AsQueryable();
        }

        public IQueryable<T> QueryById<T>(int id) where T : Entity
        {
            return Query<T>().Where(t => t.Id == id);
        }
        public ServiceResult Update<T>(T entity) where T : Entity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.SaveChanges();

            return new ServiceResult(succeeded: true).Set(entity);
        }
    }
}