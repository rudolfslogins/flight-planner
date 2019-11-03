using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using flight_planner.core.Models;
using flight_planner.core.Services;
using flight_planner.data;
using MoreLinq;

namespace flight_planner.services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context) { }

        public async Task<IEnumerable<Flight>> GetFlights()
        {
            return await Task.FromResult(Get());
        }
        public async Task<ServiceResult> AddFlight(Flight flight)
        {
            if (await FlightExists(flight))
            {
                return new ServiceResult(false);
            }

            return Create(flight);
        }
        public async Task<Flight> GetFlightById(int id)
        {
            return await GetById(id);
        }
        public async Task<ServiceResult> DeleteFlightById(int id)
        {
            var flight = await GetById(id);
            return flight == null ? new ServiceResult(true) : Delete(flight);
        }
        public async Task<bool> FlightExists(Flight flight)
        {
            return await Query().AnyAsync(f =>
                f.Carrier == flight.Carrier && 
                f.ArrivalTime == flight.ArrivalTime &&
                f.DepartureTime == flight.DepartureTime &&
                f.From.City == flight.From.City &&
                f.From.Country == flight.From.Country &&
                f.From.AirportCode == flight.From.AirportCode &&
                f.To.City == flight.To.City &&
                f.To.Country == flight.To.Country &&
                f.To.AirportCode == flight.To.AirportCode);
        }

        public async Task DeleteFlights()
        {
            _ctx.Flights.RemoveRange(_ctx.Flights);
            _ctx.Airports.RemoveRange(_ctx.Airports);
            await _ctx.SaveChangesAsync();

            using (var context = new FlightPlannerDbContext())
            {
                await context.Database.ExecuteSqlCommandAsync("DBCC CHECKIDENT ('Flights.dbo.Airports', RESEED, 0)");
                await context.Database.ExecuteSqlCommandAsync("DBCC CHECKIDENT ('Flights.dbo.Flights', RESEED, 0)");
            }
        }



        //public async Task<ICollection<Flight>> GetAllFlights()
        //{
        //    using (var context = new FlightPlannerDbContext())
        //    {
        //        return await context.Flights.Include(f => f.To).Include(f => f.From).ToListAsync();
        //    }
        //}

        //public async Task ClearFlights()
        //{
        //    using (var context = new FlightPlannerDbContext())
        //    {
        //        context.Flights.RemoveRange(context.Flights);
        //        context.Airports.RemoveRange(context.Airports);
        //        await context.SaveChangesAsync();
        //        await context.Database.ExecuteSqlCommandAsync("DBCC CHECKIDENT ('Flights.dbo.Airports', RESEED, 0)");
        //        await context.Database.ExecuteSqlCommandAsync("DBCC CHECKIDENT ('Flights.dbo.Flights', RESEED, 0)");
        //    }
        //}
        //public async Task<ICollection<Airport>> GetAirport(string search)
        //{
        //    using (var context = new FlightPlannerDbContext())
        //    {
        //        return await context.Airports.Where(a =>
        //            a.Country.ToLower().StartsWith(search) ||
        //            a.City.ToLower().StartsWith(search) ||
        //            a.AirportCode.ToLower().StartsWith(search)).Distinct().ToListAsync();
        //    }
        //}


        //public async Task<bool> AirportExists(Airport airport)
        //{
        //    using (var context = new FlightPlannerDbContext())
        //    {
        //        var exists = await context.Airports.AnyAsync(a =>
        //            a.Country == airport.Country &&
        //            a.City == airport.City &&
        //            a.AirportCode == airport.AirportCode);
        //        return exists;
        //    }
        //}
    }
}