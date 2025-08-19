using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Vehicles;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Vehicles
{
    public sealed class VehicleRepository : IVehicleRepository
    {
        private readonly IMongoCollection<VehicleDocument> _collection;

        public VehicleRepository(MongoService mongo, IOptions<MongoDbSettings> options)
        {
            var db = mongo?.MongoClient.GetDatabase(options?.Value.MongoDbDatabaseName);
            _collection = db.GetCollection<VehicleDocument>("vehicles");
        }

        public async Task<VehicleCollection> GetAsync(string vinOrPlate, int page, int pageSize, CancellationToken ct)
        {
            var filter = BuildFilter(vinOrPlate);

            var docs = await _collection.Find(filter)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync(ct);

            var domain = docs.Select(Map);
            return new VehicleCollection(domain);
        }

        public async Task<long> CountAsync(string vinOrPlate, CancellationToken ct)
        {
            var filter = BuildFilter(vinOrPlate);
            return await _collection.CountDocumentsAsync(filter, cancellationToken: ct);
        }

        private static FilterDefinition<VehicleDocument> BuildFilter(string vinOrPlate)
        {
            var builder = Builders<VehicleDocument>.Filter;
            if (string.IsNullOrWhiteSpace(vinOrPlate))
            {
                return builder.Empty;
            }

            var like = vinOrPlate.Trim();
            return builder.Or(
                builder.Regex(d => d.Vin, new MongoDB.Bson.BsonRegularExpression(like, "i")),
                builder.Regex(d => d.Plate, new MongoDB.Bson.BsonRegularExpression(like, "i")));
        }

        private static Vehicle Map(VehicleDocument d) => new()
        {
            Id = d.Id,
            Vin = d.Vin,
            Plate = d.Plate,
            Make = d.Make,
            Model = d.Model,
            Year = d.Year
        };
    }
}
