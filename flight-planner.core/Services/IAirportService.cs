using System.Collections.Generic;
using System.Threading.Tasks;
using flight_planner.core.Models;

namespace flight_planner.core.Services
{
    public interface IAirportService : IEntityService<Airport>
    {
        Task<IEnumerable<Airport>> SearchAirports(string search);
    }
}