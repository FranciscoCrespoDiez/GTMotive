using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Vehicles
{
    /// <summary>
    /// Represents the input parameters required to retrieve a paginated list of vehicles.
    /// </summary>
    /// <remarks>This class encapsulates the search criteria and pagination details for querying vehicles. The
    /// <see cref="VinOrPlate"/> property is used to filter vehicles by their VIN or license plate. The <see
    /// cref="Page"/> and <see cref="PageSize"/> properties control the pagination of the results.</remarks>
    public sealed class GetVehiclesInput : IUseCaseInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehiclesInput"/> class.
        /// </summary>
        public GetVehiclesInput()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehiclesInput"/> class with the specified VIN or license
        /// plate, page number, and page size.
        /// </summary>
        /// <param name="vinOrPlate">The vehicle identification number (VIN) or license plate to filter the vehicles. Cannot be null or empty.</param>
        /// <param name="page">The page number to retrieve. If the value is less than or equal to 0, it defaults to 1.</param>
        /// <param name="pageSize">The number of items per page. If the value is less than or equal to 0, it defaults to 20.</param>
        public GetVehiclesInput(string vinOrPlate, int page, int pageSize)
        {
            VinOrPlate = vinOrPlate;
            Page = page <= 0 ? 1 : page;
            PageSize = pageSize <= 0 ? 20 : pageSize;
        }

        /// <summary>
        /// Gets the vehicle's unique identifier, which can be either the VIN (Vehicle Identification Number) or the
        /// license plate number.
        /// </summary>
        public string VinOrPlate { get; }

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
