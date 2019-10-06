using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using flight_planner.core.Models;
using flight_planner.data;
using MoreLinq;

namespace flight_planner.services
{
    public class FlightService
    {
        public async Task<ICollection<Flight>> GetAllFlights()
        {
            using (var context = new FlightPlannerDbContext())
            {
                return await context.Flights.Include(f => f.To).Include(f => f.From).ToListAsync();
            }
        }

        public async Task<ICollection<Flight>> GetFlights(string from, string to, string departureDate)
        {
            using (var context = new FlightPlannerDbContext())
            {
                var flights = await context.Flights.Where(f =>
                        f.From.AirportCode == from &&
                        f.To.AirportCode == to &&
                        f.DepartureTime.Substring(0, 10) == departureDate).
                    Include(f => f.To).
                    Include(f => f.From).ToListAsync();
                return flights.DistinctBy(a => a.Carrier).ToList();
            }
        }

        public async Task<Flight> GetFlightById(int id)
        {
            using (var context = new FlightPlannerDbContext())
            {
                return await context.Flights.Where(f => f.id == id).Include(f => f.To).Include(f => f.From).FirstOrDefaultAsync();
            }
        }
        public async Task<ServiceResponse> AddFlight(Flight flight)
        {
            using (var context = new FlightPlannerDbContext())
            {
                if (await Exists(flight))
                {
                    return new ServiceResponse(false);
                }

                //if (await AirportExists(flight.From) && !await AirportExists(flight.To))
                //{
                //    context.Flights.Add(new Flight
                //    {
                //        From = await Get,

                //    })
                //    context.SaveChanges();
                //}
                context.Flights.Add(flight);
                await context.SaveChangesAsync();
                return new ServiceResponse(flight.id, true);
            }
        }

        public async Task DeleteFlightById(int id)
        {
            using (var context = new FlightPlannerDbContext())
            {
                var flight = await GetFlightById(id);
                if (flight != null)
                {
                    context.Flights.Remove(context.Flights.Single(f => f.id == id));
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task ClearFlights()
        {
            using (var context = new FlightPlannerDbContext())
            {
                context.Flights.RemoveRange(context.Flights);
                context.Airports.RemoveRange(context.Airports);
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlCommandAsync("DBCC CHECKIDENT ('Flights.dbo.Airports', RESEED, 0)");
                await context.Database.ExecuteSqlCommandAsync("DBCC CHECKIDENT ('Flights.dbo.Flights', RESEED, 0)");
            }
        }
        public async Task<ICollection<Airport>> GetAirport(string search)
        {
            using (var context = new FlightPlannerDbContext())
            {
                return await context.Airports.Where(a =>
                    a.Country.ToLower().StartsWith(search) ||
                    a.City.ToLower().StartsWith(search) ||
                    a.AirportCode.ToLower().StartsWith(search)).Distinct().ToListAsync();
            }
        }

        public async Task<bool> Exists(Flight flight)
        {
            using (var context = new FlightPlannerDbContext())
            {
                var exists = await context.Flights.AnyAsync(f => 
                    f.Carrier == flight.Carrier && f.ArrivalTime == flight.ArrivalTime &&
                    f.DepartureTime == flight.DepartureTime &&
                    f.From.City == flight.From.City &&
                    f.From.Country == flight.From.Country &&
                    f.From.AirportCode == flight.From.AirportCode &&
                    f.To.City == flight.To.City &&
                    f.To.Country == flight.To.Country &&
                    f.To.AirportCode == flight.To.AirportCode);
                return exists;
            }
        }

        public async Task<bool> AirportExists(Airport airport)
        {
            using (var context = new FlightPlannerDbContext())
            {
                var exists = await context.Airports.AnyAsync(a =>
                    a.Country == airport.Country &&
                    a.City == airport.City &&
                    a.AirportCode == airport.AirportCode);
                return exists;
            }
        }
    }
}