using System;

namespace flight_planner.core.Models
{
    public class Airport
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
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