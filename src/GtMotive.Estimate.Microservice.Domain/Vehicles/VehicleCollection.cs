using System;
using System.Collections.Generic;
using System.Linq;

namespace GtMotive.Estimate.Microservice.Domain.Vehicles
{
    /// <summary>
    /// First-class collection of vehicles.
    /// </summary>
    public sealed class VehicleCollection : IReadOnlyCollection<Vehicle>
    {
        private readonly List<Vehicle> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleCollection"/> class.
        /// </summary>
        public VehicleCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleCollection"/> class with the specified items.
        /// </summary>
        /// <param name="items">Items of the collection.</param>
        /// <exception cref="InvalidOperationException">Invalid operation exception in the collection.</exception>
        public VehicleCollection(IEnumerable<Vehicle> items)
        {
#pragma warning disable IDE0028 // Simplify collection initialization: TO ASK: I didn't know how to fix this warning.
            _items = items?.ToList() ?? new List<Vehicle>();
#pragma warning restore IDE0028 // Simplify collection initialization

            // ejemplo de invariante: no permitir duplicados por Id
            if (_items.GroupBy(v => v.Id).Any(g => g.Count() > 1))
            {
                throw new InvalidOperationException("No se permiten vehículos duplicados.");
            }
        }

        /// <summary>
        /// Gets the number of items in the collection.
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <returns>Enumerator for a specified index.</returns>
        public IEnumerator<Vehicle> GetEnumerator() => _items.GetEnumerator();

        /// <summary>
        /// Gets the enumerator for the collection.
        /// </summary>
        /// <returns>Enumerator for the collection.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

#nullable enable
        /// <summary>
        /// Gets a vehicle by its VIN.
        /// </summary>
        /// <param name="vin">VIN.</param>
        /// <returns>The vehicle.</returns>
        public Vehicle? FindByVin(string vin) =>
            _items.FirstOrDefault(v => v.Vin.Equals(vin, StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Gets if the collection contains a vehicle with the specified VIN.
        /// </summary>
        /// <returns>True if there is any vehicle.</returns>
        public bool Any() => _items.Count > 0;

        /// <summary>
        /// Gets a read-only list of vehicles in the collection.
        /// </summary>
        /// <returns>Read-only list.</returns>
        public IReadOnlyList<Vehicle> AsReadOnlyList() => _items;
    }
}
