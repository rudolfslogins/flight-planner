using System.Collections.Generic;
using flight_planner.core.Models;
using Newtonsoft.Json;

namespace flight_planner.Models
{
    public class FlightSearchResponse
    {
        [JsonProperty(Order = 2)]
        public List<FlightRequest> Items { get; set; }

        [JsonProperty(Order = 1)]
        public int TotalItems { get; set; }

        [JsonProperty(Order = -2)]
        public int Page { get; set; }

        public FlightSearchResponse(List<FlightRequest> items)
        {
            Items = items;
            TotalItems = items.Count;
            Page = GetPage();
        }

        private int GetPage()
        {
            var pageCount = 0;
            if (TotalItems > 0)
            {
                pageCount = 1;
            }
            return pageCount;
        }
    }
}