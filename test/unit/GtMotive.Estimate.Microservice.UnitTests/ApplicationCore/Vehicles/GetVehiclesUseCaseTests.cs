using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Vehicles;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Vehicles;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore.Vehicles
{
    public sealed class GetVehiclesUseCaseTests
    {
        /// <summary>
        /// Test to ensure that the use case returns NotFound when no vehicles match the criteria.
        /// </summary>
        /// <returns>Result of test.</returns>
        [Fact]
        public async Task ExecuteShouldNotFoundWhenEmpty()
        {
            // Arrange
            var repo = new Mock<IVehicleRepository>();
            repo.Setup(r => r.GetAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new VehicleCollection());
            repo.Setup(r => r.CountAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            var outPort = new Mock<IGetVehiclesOutputPort>(MockBehavior.Strict);
            outPort.Setup(o => o.NotFoundHandle(It.IsAny<string>()));

            var uow = new Mock<IUnitOfWork>(MockBehavior.Strict);

            var uc = new GetVehiclesUseCase(repo.Object, outPort.Object, uow.Object);

            // Act
            await Task.Run(() => uc.Execute(new GetVehiclesInput("ABC", 1, 10)));

            // Assert
            outPort.Verify(o => o.NotFoundHandle(It.IsAny<string>()), Times.Once);
            outPort.Verify(o => o.StandardHandle(It.IsAny<GetVehiclesOutput>()), Times.Never);
            uow.Verify(u => u.Save(), Times.Never);
        }

        /// <summary>
        /// Test to ensure that the use case handles results correctly when there are vehicles returned.
        /// </summary>
        /// <returns>Test result.</returns>
        [Fact]
        public async Task ExecuteShouldStandardHandleWhenHasResults()
        {
            // Arrange
            var vehicles = new VehicleCollection([
                V("1", "VIN1", "1234ABC", "Toyota", "Corolla", 2020),
                V("2", "VIN2", "5678DEF", "Ford", "Focus", 2021),
            ]);

            var repo = new Mock<IVehicleRepository>();
            repo.Setup(r => r.GetAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicles);
            repo.Setup(r => r.CountAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(2);
#nullable enable
            GetVehiclesOutput? captured = null;
#nullable disable
            var outPort = new Mock<IGetVehiclesOutputPort>(MockBehavior.Strict);
            outPort.Setup(o => o.StandardHandle(It.IsAny<GetVehiclesOutput>()))
                   .Callback<GetVehiclesOutput>(o => captured = o);
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(u => u.Save()).Returns((Task<int>)Task.CompletedTask);

            var uc = new GetVehiclesUseCase(repo.Object, outPort.Object, uow.Object);

            // Act
            await Task.Run(() => uc.Execute(new GetVehiclesInput("VIN", page: 1, pageSize: 10)));

            // Assert
            Assert.NotNull(captured);
            Assert.Equal(2, captured!.Vehicles.Count);
            Assert.Equal(2, captured.Total);
            Assert.Equal(1, captured.Page);
            Assert.Equal(10, captured.PageSize);

            outPort.Verify(o => o.StandardHandle(It.IsAny<GetVehiclesOutput>()), Times.Once);
            outPort.Verify(o => o.NotFoundHandle(It.IsAny<string>()), Times.Never);
            uow.Verify(u => u.Save(), Times.Once);
        }

        private static Vehicle V(string id, string vin, string plate = "", string make = "", string model = "", int year = 0)
            => new()
            { Id = id, Vin = vin, Plate = plate, Make = make, Model = model, Year = year };
    }
}
