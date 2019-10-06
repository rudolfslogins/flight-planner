namespace flight_planner.core.Models
{
    public class Flight
    {
        public int id { get; set; }
        public Airport From { get; set; }
        public Airport To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }

        public Flight(int Id, Airport from, Airport to, string carrier, string departureTime, string arrivalTime)
        {
            id = Id;
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