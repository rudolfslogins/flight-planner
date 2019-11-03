using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flight_planner.core.Models;
using flight_planner.core.Services;
using flight_planner.data;

namespace flight_planner.services
{
    public class EntityService<T> : DbService, IEntityService<T> where T : Entity
    {
        public EntityService(IFlightPlannerDbContext context) : base(context)
        {
        }
        public ServiceResult Create(T entity)
        {
            return Create<T>(entity);
        }
        public ServiceResult Delete(T entity)
        {
            return Delete<T>(entity);
        }
        public ServiceResult Update(T entity)
        {
            return Update<T>(entity);
        }
        public bool Exists(int id)
        {
            return QueryById(id).Any();
        }
        public IEnumerable<T> Get()
        {
            return Get<T>();
        }
        public virtual async Task<T> GetById(int id)
        {
            return await GetById<T>(id);
        }
        public virtual IQueryable<T> Query()
        {
            return Query<T>();
        }
        public virtual IQueryable<T> QueryById(int id)
        {
            return Query<T>().Where(t => t.Id == id);
        }
    }
}