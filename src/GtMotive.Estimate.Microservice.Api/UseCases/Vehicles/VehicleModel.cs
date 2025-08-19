using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    public sealed class VehicleModel
    {
        /// <summary>Gets identifier.</summary>
        [Required]
        public string Id { get; init; }

        /// <summary>Gets VIN.</summary>
        [Required]
        public string Vin { get; init; }

        /// <summary>Gets plate.</summary>
        public string Plate { get; init; }

        /// <summary>Gets make.</summary>
        public string Make { get; init; }

        /// <summary>Gets model.</summary>
        public string Model { get; init; }

        /// <summary>Gets year.</summary>
        public int Year { get; init; }
    }
}
