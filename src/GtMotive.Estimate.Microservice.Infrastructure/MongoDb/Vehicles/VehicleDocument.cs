namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Vehicles
{
    public sealed class VehicleDocument
    {
        public string Id { get; set; }

        public string Vin { get; set; }

        public string Plate { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }
    }
}
