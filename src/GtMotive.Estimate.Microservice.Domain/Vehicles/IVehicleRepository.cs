using System.Threading;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Defines a repository for retrieving and managing vehicle data.
    /// </summary>
    /// <remarks>This interface provides methods to query vehicles based on their VIN or license plate, as
    /// well as to retrieve the total count of matching vehicles. It supports asynchronous operations and cancellation
    /// through <see cref="CancellationToken"/>.</remarks>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Retrieves a paginated list of vehicles that match the specified VIN or license plate.
        /// </summary>
        /// <remarks>This method supports pagination to efficiently retrieve large sets of data. Use the
        /// <paramref name="page"/>  and <paramref name="pageSize"/> parameters to control the subset of results
        /// returned.</remarks>
        /// <param name="vinOrPlate">The VIN (Vehicle Identification Number) or license plate to search for.  This value cannot be null or empty.</param>
        /// <param name="page">The zero-based page index to retrieve. Must be greater than or equal to 0.</param>
        /// <param name="pageSize">The number of vehicles to include in each page. Must be greater than 0.</param>
        /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list  of <see
        /// cref="Vehicle"/> objects matching the search criteria. The list will be empty if no matches are found.</returns>
        Task<VehicleCollection> GetAsync(string vinOrPlate, int page, int pageSize, CancellationToken ct);

        /// <summary>
        /// Asynchronously retrieves the count of records associated with the specified VIN or license plate.
        /// </summary>
        /// <param name="vinOrPlate">The vehicle identification number (VIN) or license plate to search for.  This value cannot be null or empty.</param>
        /// <param name="ct">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the count of matching records.</returns>
        Task<long> CountAsync(string vinOrPlate, CancellationToken ct);
    }
}
