using System;
using System.Collections.Generic;
using flight_planner.Models;
using System.Collections.Concurrent;
using System.Linq;

namespace flight_planner.Models
{
    public class FlightStorage
    {
        private static SynchronizedCollection<Flight> _flights { get; set; }
        private static int _id;
        private static readonly object ListLock = new object();
        static FlightStorage()
        {
            _flights = new SynchronizedCollection<Flight>();
            _id = 1;
        }

        public static bool AddFlight(Flight flight)
        {
            lock (ListLock)
            {
                if (_flights.Any(f => f.Equals(flight))) return false;
                _flights.Add(flight);
                return true;
            }
        }

        public static Flight GetFlightById(int id)
        {
            lock (ListLock)
            {
                var pair = _flights.FirstOrDefault(f => f.id == id);
                return pair;
            }
        }

        public static List<Flight> GetFlights(string from, string to, DateTime departureDate)
        {
            var result = new List<Flight>();
            foreach (var flight in _flights)
            {
                DateTime flightDate = DateTime.Parse(flight.DepartureTime);
                if (flight.From.Airport == from && flight.To.Airport == to && flightDate.Date == departureDate.Date)
                {
                    result.Add(flight);
                }
            }
            return result;
        }

        public static void ClearFlights()
        {
            _flights.Clear();
            _id = 1;
        }

        public static bool RemoveFlightById(int id)
        {
            {
                var flight = GetFlightById(id);

                if (flight == null) return false;
                _flights.Remove(flight);
                return true;
            }
        }

        public static int GetNextId()
        {
            return _id++;
        }

        public static List<AirportRequest> GetAirport(string search)
        {
            StringComparison comp = StringComparison.OrdinalIgnoreCase;
            var airports = new List<AirportRequest>();
            foreach (var keyValuePair in _flights)
            {

                if (keyValuePair.From.Contains(search, comp))
                {
                    if (!airports.Contains(keyValuePair.From))
                        airports.Add(keyValuePair.From);
                }

                if (keyValuePair.To.Contains(search, comp))
                {
                    if (!airports.Contains(keyValuePair.To))
                        airports.Add(keyValuePair.To);
                }
            }
            return airports;
        }
    }
}