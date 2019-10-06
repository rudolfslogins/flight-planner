using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using flight_planner.Models;
using flight_planner.services;

namespace flight_planner.Controllers
{
    public class TestApiController : ApiController
    {
        private readonly FlightService _flightService;
        public TestApiController()
        {
            _flightService = new FlightService();
        }
        [HttpPost]
        [Route("testing-api/clear")]
        public async Task<IHttpActionResult> Clear()
        {
            await _flightService.ClearFlights();
            return Ok();
        }

        [HttpGet]
        [Route("testing-api/")]
        public string Get()
        {
            return "Testing api";
        }
    }
}
