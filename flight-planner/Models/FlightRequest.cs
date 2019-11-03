using flight_planner.core.Models;

namespace flight_planner.Models
{
    public class FlightRequest
    {
        public int id { get; set; }
        public AirportRequest From { get; set; }
        public AirportRequest To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }

        public FlightRequest(AirportRequest from, AirportRequest to, string carrier, string departureTime, string arrivalTime)
        {
            From = from;
            To = to;
            Carrier = carrier;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            //id = Id;
        }

        public FlightRequest()
        {

        }
        public override bool Equals(object obj)
        {
            var flight = obj as FlightRequest;
            if (flight == null)
            {
                return false;
            }
            return flight.Carrier == Carrier && flight.ArrivalTime == ArrivalTime &&
                   flight.DepartureTime == DepartureTime && flight.From.Equals(From) &&
                   flight.To.Equals(To);
        }
    }
}