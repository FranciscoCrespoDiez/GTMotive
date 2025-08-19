using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Vehicles
{
    /// <summary>
    /// Interface for the output port of the GetVehicles use case.
    /// </summary>
    public interface IGetVehiclesOutputPort : IOutputPortStandard<GetVehiclesOutput>, IOutputPortNotFound
    {
    }
}
