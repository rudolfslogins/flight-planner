using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using flight_planner.core.Models;
using flight_planner.core.Services;
using flight_planner.data;

namespace flight_planner.services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Airport>> SearchAirports(string search)
        {
            search = search.ToLower().Trim();
            var airports = Query().Where(a => a.AirportCode.ToLower().Contains(search) ||
                                              a.City.ToLower().Contains(search) ||
                                              a.Country.ToLower().Contains(search));
            return await airports.ToListAsync();
        }
    }
}