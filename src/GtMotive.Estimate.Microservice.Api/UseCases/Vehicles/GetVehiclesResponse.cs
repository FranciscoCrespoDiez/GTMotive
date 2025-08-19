using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    public sealed class GetVehiclesResponse
    {
        /// <summary>Initializes a new instance of the <see cref="GetVehiclesResponse"/> class.</summary>
        /// <remarks>This class represents the response for a use case that retrieves a paginated list of vehicles.</remarks>
        public GetVehiclesResponse()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="GetVehiclesResponse"/> class.</summary>
        /// <param name="items">List of vehicles.</param>
        /// <param name="page">Actual page.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="total">Total of vehicles.</param>
        /// <remarks>This class represents the response for a use case that retrieves a paginated list of vehicles.</remarks>
        public GetVehiclesResponse(IReadOnlyList<VehicleModel> items, long total, int page, int pageSize)
        {
            Items = items;
            Total = total;
            Page = page;
            PageSize = pageSize;
        }

        /// <summary>Gets vehicles.</summary>
        [Required]
        public IReadOnlyList<VehicleModel> Items { get; }

        /// <summary>Gets total of vehicles.</summary>
        [Required]
        public long Total { get; }

        /// <summary>Gets actual page.</summary>
        [Required]
        public int Page { get; }

        /// <summary>Gets page size.</summary>
        [Required]
        public int PageSize { get; }
    }
}
