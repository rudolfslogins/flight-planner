﻿using System.Collections.Generic;

namespace flight_planner.Models
{
    public class FlightSearchResponse
    {
        public List<Flight> Items { get; set; }
        public int TotalItems { get; set; }
        public int Page { get; set; }

        public FlightSearchResponse(List<Flight> items)
        {
            Items = items;
            TotalItems = Items.Count;
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