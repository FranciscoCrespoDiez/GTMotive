using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Vehicles;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    public sealed class GetVehiclesRequestHandler : IRequestHandler<GetVehiclesRequest, IWebApiPresenter>
    {
        private readonly GetVehiclesUseCase _useCase;
        private readonly GetVehiclesPresenter _presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehiclesRequestHandler"/> class.
        /// </summary>
        public GetVehiclesRequestHandler()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehiclesRequestHandler"/> class with the specified use case and presenter.
        /// </summary>
        /// <param name="useCase">The use case.</param>
        /// <param name="presenter">The presenter.</param>
        public GetVehiclesRequestHandler(GetVehiclesUseCase useCase, GetVehiclesPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(GetVehiclesRequest request, CancellationToken cancellationToken)
        {
            var input = new GetVehiclesInput(request?.PlateOrVIN ?? throw new ArgumentNullException(nameof(request)), request.Page, request.PageSize);
            await _useCase.Execute(input);
            return _presenter;
        }
    }
}
