using AutoFixture;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.LabScheduleManagement.DataServices.Repositories.Interfaces;
using ERP.LabScheduleManagement.Api.Controllers;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll;
using ERP.LabScheduleManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetById;
using ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests;
using ERP.LabScheduleManagement.Core.DTOs.Requests.UpdateRequests;

namespace ERP.LabScheduleManagement.Api.Tests.Controllers
{
    public class LabCoordinatorControllersTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _mock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LabCoordinatorControllers _controller;

        public LabCoordinatorControllersTests()
        {
            _fixture = new Fixture();
            _mock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _controller = new LabCoordinatorControllers(_mock.Object, _mockMapper.Object);

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        //Get All Lab Coordinators API test
        [Fact]
        public async Task GetAllLabCoordinators_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var labCoordinatorsMock = _fixture.Create<IEnumerable<LabCoordinator>>();
            object value = _mock.Setup(x => x.Coordinators.All()).ReturnsAsync(labCoordinatorsMock);

            var labCoordinatorsListMock = _fixture.Create<IEnumerable<GetLabCoordinatorResponse>>();
            object listValue = _mockMapper.Setup(x => x.Map<IEnumerable<GetLabCoordinatorResponse>>(labCoordinatorsMock)).Returns(labCoordinatorsListMock);

            //Act
            var result = await _controller.GetAllLabCoordinators().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(labCoordinatorsListMock.GetType());
            _mock.Verify(x => x.Coordinators.All(), Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<GetLabCoordinatorResponse>>(labCoordinatorsMock), Times.Once);

        }

        [Fact]
        public async Task GetAllLabCoordinators_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            List<LabCoordinator> response = null;
            object value = _mock.Setup(x => x.Coordinators.All()).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAllLabCoordinators().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Coordinators.All(), Times.Once);

        }



        //Get Lab Coordinator By Id API tests

        [Fact]
        public async Task GetLabCoordinator_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var labCoordinatorMock = _fixture.Create<LabCoordinator>();
            var labCoordinatorId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Coordinators.GetById(labCoordinatorId)).ReturnsAsync(labCoordinatorMock);

            var labCoordinatorDTOMock = _fixture.Create<GetLabCoordinatorByIdResponse>();
            object mappedValue = _mockMapper.Setup(x => x.Map<GetLabCoordinatorByIdResponse>(labCoordinatorMock)).Returns(labCoordinatorDTOMock);

            //Act
            var result = await _controller.GetLabCoordinator(labCoordinatorId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(labCoordinatorDTOMock.GetType());
            _mock.Verify(x => x.Coordinators.GetById(labCoordinatorId), Times.Once);
            _mockMapper.Verify(x => x.Map<GetLabCoordinatorByIdResponse>(labCoordinatorMock), Times.Once);

        }

        [Fact]
        public async Task GetLabCoordinator_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            LabCoordinator response = null;
            var labCoordinatorId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Coordinators.GetById(labCoordinatorId)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetLabCoordinator(labCoordinatorId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Coordinators.GetById(labCoordinatorId), Times.Once);

        }

        //Add Lab Coordinator API tests
        [Fact]
        public async Task AddLabCoordinator_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateLabCoordinatorRequest>();
            var response = _fixture.Create<LabCoordinator>();

            _mockMapper.Setup(x => x.Map<LabCoordinator>(request)).Returns(response);
            _mock.Setup(x => x.Coordinators.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLabCoordinator(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
            _mockMapper.Verify(x => x.Map<LabCoordinator>(request), Times.Once);
            _mock.Verify(x => x.Coordinators.Add(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task AddLabCoordinator_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateLabCoordinatorRequest>();
            _controller.ModelState.AddModelError("CoordinatorName", "The CoordinatorName field is required.");
            var response = _fixture.Create<LabCoordinator>();

            _mockMapper.Setup(x => x.Map<LabCoordinator>(request)).Returns(response);
            _mock.Setup(x => x.Coordinators.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLabCoordinator(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _mockMapper.Verify(x => x.Map<LabCoordinator>(request), Times.Never);
            _mock.Verify(x => x.Coordinators.Add(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }



        //Delete Lab Coordinator API test

        [Fact]
        public async Task DeleteLabCoordinator_ShouldReturnNoContent_WhenLabCoordinatorDeleted()
        {
            //Arange

            var labCoordinatorId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabCoordinator>();

            _mock.Setup(x => x.Coordinators.GetById(labCoordinatorId)).ReturnsAsync(response);
            _mock.Setup(x => x.Coordinators.Delete(labCoordinatorId)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteLabCoordinator(labCoordinatorId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mock.Verify(x => x.Coordinators.GetById(labCoordinatorId), Times.Once);
            _mock.Verify(x => x.Coordinators.Delete(labCoordinatorId), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteLabCoordinator_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange

            var labCoordinatorId = _fixture.Create<Guid>();
            LabCoordinator labResponce = null;

            _mock.Setup(x => x.Coordinators.GetById(labCoordinatorId)).ReturnsAsync(labResponce);

            //Act
            var result = await _controller.DeleteLabCoordinator(labCoordinatorId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Coordinators.GetById(labCoordinatorId), Times.Once);
            _mock.Verify(x => x.Coordinators.Delete(labCoordinatorId), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }

        //Update Lab Coordinator API tests

        [Fact]
        public async Task UpdateLabCoordinator_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateLabCoordinatorRequest>();
            var labCoordinatorId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabCoordinator>();
            _controller.ModelState.AddModelError("CoordinatorName", "The CoordinatorName field is required.");


            //Act
            var result = await _controller.UpdateLabCoordinator(labCoordinatorId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _mockMapper.Verify(x => x.Map<LabCoordinator>(request), Times.Never);
            _mock.Verify(x => x.Coordinators.Update(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        [Fact]
        public async Task UpdateLabCoordinator_ShouldReturnNoContentResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateLabCoordinatorRequest>();
            var labCoordinatorId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabCoordinator>();

            _mockMapper.Setup(x => x.Map<LabCoordinator>(request)).Returns(response);
            _mock.Setup(x => x.Coordinators.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);


            //Act
            var result = await _controller.UpdateLabCoordinator(labCoordinatorId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mockMapper.Verify(x => x.Map<LabCoordinator>(request), Times.Once);
            _mock.Verify(x => x.Coordinators.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task UpdateLabCoordinator_ShouldReturnStatusCode500_WhenErrorOccursSavingToDatabase()
        {
            //Arange
            var request = _fixture.Create<UpdateLabCoordinatorRequest>();
            var labCoordinatorId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabCoordinator>();

            _mockMapper.Setup(x => x.Map<LabCoordinator>(request)).Returns(response);
            _mock.Setup(x => x.Coordinators.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ThrowsAsync(new Exception("Database error occurred"));


            //Act
            var result = await _controller.UpdateLabCoordinator(labCoordinatorId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var statusCodeResult = (ObjectResult)result;
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("An error occurred while updating the lab coordinators.");
            _mockMapper.Verify(x => x.Map<LabCoordinator>(request), Times.Once);
            _mock.Verify(x => x.Coordinators.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

    }

}
