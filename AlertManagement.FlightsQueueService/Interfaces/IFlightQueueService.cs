namespace AlertManagement.FlightsQueueService.Interfaces
{
    public interface IFlightQueueService
    {
        Task AddFlightAsync(string flightNumber);
        Task RemoveFlightAsync(string flightNumber);
    }
}
