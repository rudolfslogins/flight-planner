using System;
using System.ComponentModel.DataAnnotations;

namespace flight_planner.core.Models
{
    public class Airport : Entity
    {
        [ConcurrencyCheck]
        public string Country { get; set; }
        [ConcurrencyCheck]
        public string City { get; set; }
        [ConcurrencyCheck]
        public string AirportCode { get; set; }

        public Airport(string country, string city, string airportCode)
        {
            Country = country;
            City = city;
            AirportCode = airportCode;
        }

        public Airport()
        {

        }
    }
}