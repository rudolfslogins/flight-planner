using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using flight_planner.core.Services;
using flight_planner.Models;
using flight_planner.services;

namespace flight_planner.Controllers
{
    public class TestApiController : BasicApiController
    {
        //private readonly FlightService _flightService;
        public TestApiController(IFlightService flightService, IMapper mapper) : base(flightService, mapper)
        {
            //_flightService = new FlightService();
        }
        //public TestApiController()
        //{
        //    _flightService = new FlightService();
        //}
        [HttpPost]
        [Route("testing-api/clear")]
        public async Task<IHttpActionResult> Clear()
        {
            await _flightService.DeleteFlights();
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
