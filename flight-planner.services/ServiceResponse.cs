namespace flight_planner.services
{
    public class ServiceResponse
    {
        public bool Succeeded { get; }
        public int Id { get; }

        public ServiceResponse(bool succeeded)
        {
            Succeeded = succeeded;
        }

        public ServiceResponse(int id, bool succeeded)
        {
            Id = id;
            Succeeded = succeeded;
        }
    }
}