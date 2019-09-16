using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using flight_planner.Attributes;
using flight_planner.Models;

namespace flight_planner.Controllers
{
    [BasicAuthentication]
    public class AdminApiController : ApiController

    {

        [HttpGet]
        [Route("admin-api/flights/{id}")]
        public async Task<HttpResponseMessage> Get(HttpRequestMessage request, int id)
        {
            var flight = FlightStorage.GetFlightById(id);
            if (flight == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, flight);
        }

        [HttpPut]
        [Route("admin-api/flights")]
        public async Task<HttpResponseMessage> AddFlight(HttpRequestMessage request, Flight flight)
        {
            if (!IsValid(flight))
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, flight);
            }
            flight.id = FlightStorage.GetNextId();

            if (!FlightStorage.AddFlight(flight))
            {
                return request.CreateResponse(HttpStatusCode.Conflict, flight);
            }
            return request.CreateResponse(HttpStatusCode.Created, flight);
        }

        [HttpDelete]
        [Route("admin-api/flights/{id}")]
        public async Task<HttpResponseMessage> Delete(HttpRequestMessage request, int id)
        {

            FlightStorage.RemoveFlightById(id);

            return request.CreateResponse(HttpStatusCode.OK);
        }
        private bool IsValid(Flight flight)
        {
            if (!string.IsNullOrEmpty(flight.ArrivalTime) &&
                !string.IsNullOrEmpty(flight.DepartureTime) &&
                !string.IsNullOrEmpty(flight.Carrier) &&
                !string.IsNullOrEmpty(flight.DepartureTime) &&
                IsValidAirport(flight.From) && IsValidAirport(flight.To) &&
                !flight.From.Equals(flight.To) &&
                IsValidDateTime(flight.DepartureTime, flight.ArrivalTime))
            {
                return true;
            }
            return false;
        }

        private static bool IsValidAirport(AirportRequest airport)
        {
            return airport != null && !string.IsNullOrEmpty(airport.Airport) &&
                   !string.IsNullOrEmpty(airport.City) &&
                   !string.IsNullOrEmpty(airport.Country);
        }

        private static bool IsValidDateTime(string departTime, string arrTime)
        {
            DateTime dTime = DateTime.Parse(departTime);
            DateTime aTime = DateTime.Parse(arrTime);

            if (aTime == dTime || aTime < dTime)
            {
                return false;
            }
            return true;
        }
    }
}
