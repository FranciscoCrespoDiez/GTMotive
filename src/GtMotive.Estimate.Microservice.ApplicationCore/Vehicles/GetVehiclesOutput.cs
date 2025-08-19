using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Vehicles
{
    /// <summary>
    /// Represents the output of a use case that retrieves a paginated list of vehicles.
    /// </summary>
    public sealed class GetVehiclesOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehiclesOutput"/> class.
        /// </summary>
        public GetVehiclesOutput()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehiclesOutput"/> class.
        /// </summary>
        /// <param name="vehicles">Vehicles listed.</param>
        /// <param name="total">Total of elements in DB.</param>
        /// <param name="page">Page shown.</param>
        /// <param name="pageSize">Elements per page.</param>
        public GetVehiclesOutput(VehicleCollection vehicles, long total, int page, int pageSize)
        {
            Vehicles = vehicles;
            Total = total;
            Page = page;
            PageSize = pageSize;
        }

        /// <summary>
        /// Gets the list of vehicles.
        /// </summary>
        public VehicleCollection Vehicles { get; }

        /// <summary>
        /// Gets the total number of vehicles in the database that match the search criteria.
        /// </summary>
        public long Total { get; }

        /// <summary>
        /// Gets the current page number in a paginated result set.
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// Gets the number of items to include in each page of the paginated result set.
        /// </summary>
        public int PageSize { get; }
    }
}
