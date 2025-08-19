using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    public sealed class GetVehiclesRequest : IRequest<IWebApiPresenter>
    {
        public string PlateOrVIN { get; init; }

        public int Page { get; init; } = 1;

        public int PageSize { get; init; } = 20;
    }
}
