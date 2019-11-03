using System.ComponentModel.DataAnnotations;

namespace flight_planner.core.Models
{
    public class Flight : Entity
    {
        [ConcurrencyCheck]
        public virtual Airport From { get; set; }
        [ConcurrencyCheck]
        public virtual Airport To { get; set; }
        [ConcurrencyCheck]
        public string Carrier { get; set; }
        [ConcurrencyCheck]
        public string DepartureTime { get; set; }
        [ConcurrencyCheck]
        public string ArrivalTime { get; set; }

        public Flight(Airport from, Airport to, string carrier, string departureTime, string arrivalTime)
        {
            //id = Id;
            From = from;
            To = to;
            Carrier = carrier;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }

        public Flight()
        {

        }
    }
}