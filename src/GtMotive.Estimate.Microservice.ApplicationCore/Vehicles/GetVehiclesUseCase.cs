using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Vehicles;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Vehicles
{
    /// <summary>
    /// Class that implements the use case for retrieving a paginated list of vehicles.
    /// </summary>
    /// <remarks>This use case encapsulates the logic for fetching vehicles based on a VIN
    /// or license plate, along with pagination support. It interacts with the vehicle repository
    /// to retrieve the data and uses an output port to return the results.</remarks>
    public sealed class GetVehiclesUseCase : IUseCase<GetVehiclesInput>
    {
        private readonly IVehicleRepository _repository;
        private readonly IGetVehiclesOutputPort _output;
        private readonly IUnitOfWork _uow; // si no hay transacción, puede omitirse

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehiclesUseCase"/> class.
        /// </summary>
        public GetVehiclesUseCase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehiclesUseCase"/> class.
        /// </summary>
        /// <param name="repository">Vehicle repository.</param>
        /// <param name="output">Interface which defines the case use's output.</param>
        /// <param name="uow">Unit Of Work.</param>
        public GetVehiclesUseCase(
            IVehicleRepository repository,
            IGetVehiclesOutputPort output,
            IUnitOfWork uow)
        {
            _repository = repository;
            _output = output;
            _uow = uow;
        }

        /// <summary>
        /// Executes the use case to retrieve a paginated list of vehicles based on the provided input.
        /// </summary>
        /// <param name="input">Object with the needed data to get the vehicle list.</param>
        /// <returns>List of vehicles.</returns>
        public async Task Execute(GetVehiclesInput input)
        {
            var vehicles = await _repository.GetAsync(input?.VinOrPlate, input.Page, input.PageSize, CancellationToken.None);
            var total = await _repository.CountAsync(input.VinOrPlate, CancellationToken.None);

            if (total == 0 || vehicles.Count == 0)
            {
                _output.NotFoundHandle("No se han encontrado vehículos con el filtro indicado.");
                return;
            }

            // No persiste nada, pero mantenemos UoW por consistencia del patrón.
            await _uow.Save();

            _output.StandardHandle(new GetVehiclesOutput(vehicles, total, input.Page, input.PageSize));
        }
    }
}
