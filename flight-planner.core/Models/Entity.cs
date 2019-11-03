using flight_planner.core.Interfaces;

namespace flight_planner.core.Models
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}