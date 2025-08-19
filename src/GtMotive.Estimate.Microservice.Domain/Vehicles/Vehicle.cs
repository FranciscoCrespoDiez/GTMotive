namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// Represents a vehicle with identifying and descriptive information, such as its VIN, license plate, make, model,
    /// and year.
    /// </summary>
    /// <remarks>This class is immutable, with all properties being initialized at the time of object
    /// creation.  It is designed to encapsulate key details about a vehicle for use in applications such as fleet
    /// management, vehicle tracking, or registration systems.</remarks>
    public sealed class Vehicle
    {
        /// <summary>
        /// Gets vehicle identifier.
        /// </summary>
        public string Id { get; init; }

        /// <summary>
        /// Gets the Vehicle Identification Number (VIN) of the vehicle.
        /// </summary>
        public string Vin { get; init; }

        /// <summary>
        /// Gets the license plate number associated with the vehicle.
        /// </summary>
        public string Plate { get; init; }

        /// <summary>
        /// Gets the make of the vehicle.
        /// </summary>
        public string Make { get; init; }

        /// <summary>
        /// Gets the model name associated with the vehicle.
        /// </summary>
        public string Model { get; init; }

        /// <summary>
        /// Gets the year of the vehicle.
        /// </summary>
        public int Year { get; init; }
    }
}
