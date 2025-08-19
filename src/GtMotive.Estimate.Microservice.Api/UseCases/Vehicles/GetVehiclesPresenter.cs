using System.Linq;
using GtMotive.Estimate.Microservice.ApplicationCore.Vehicles;
using GtMotive.Estimate.Microservice.Domain.Vehicles;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    public sealed class GetVehiclesPresenter : IWebApiPresenter, IGetVehiclesOutputPort
    {
        public IActionResult ActionResult { get; private set; }

        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(new { message });
        }

        public void StandardHandle(GetVehiclesOutput output)
        {
            var items = output?.Vehicles.AsReadOnlyList().Select(Map).ToList();
            var response = new GetVehiclesResponse(items, output.Total, output.Page, output.PageSize);
            ActionResult = new OkObjectResult(response);
        }

        private static VehicleModel Map(Vehicle v) => new()
        {
            Id = v.Id,
            Vin = v.Vin,
            Plate = v.Plate,
            Make = v.Make,
            Model = v.Model,
            Year = v.Year
        };
    }
}
