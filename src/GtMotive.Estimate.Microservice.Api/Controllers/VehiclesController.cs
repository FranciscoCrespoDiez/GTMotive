using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases.Vehicles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    [ApiController]
    [Route("vehicles")]
    public sealed class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesController"/> class.
        /// </summary>
        /// <remarks>This constructor is used for dependency injection.</remarks>
        public VehiclesController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        /// <remarks>This constructor is used for dependency injection.</remarks>
        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Vehicle enquiry (filter by VIN or registration number).</summary>
        /// <param name="plateOrVIN">Plate or VIN.</param>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>List of vehicles.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetVehiclesResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(
            [FromQuery] string plateOrVIN,
            [FromQuery, Range(1, int.MaxValue)] int page = 1,
            [FromQuery, Range(1, 200)] int pageSize = 20)
        {
            var presenter = await _mediator.Send(new GetVehiclesRequest { PlateOrVIN = plateOrVIN, Page = page, PageSize = pageSize });
            return presenter.ActionResult;
        }
    }
}
