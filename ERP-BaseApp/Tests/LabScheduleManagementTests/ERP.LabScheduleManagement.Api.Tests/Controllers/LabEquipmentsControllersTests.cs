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
using ERP.LabScheduleManagement.Core.Entities;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetById;
using ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests;
using ERP.LabScheduleManagement.Core.DTOs.Requests.UpdateRequests;

namespace ERP.LabScheduleManagement.Api.Tests.Controllers
{
    public class LabEquipmentsControllersTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _mock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LabEquipmentControllers _controller;

        public LabEquipmentsControllersTests()
        {
            _fixture = new Fixture();
            _mock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _controller = new LabEquipmentControllers(_mock.Object, _mockMapper.Object);

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }


        //Get All LabEquipments API test
        [Fact]
        public async Task GetAllLabEquipments_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var labEquipmentMock = _fixture.Create<IEnumerable<LabEquipment>>();
            object value = _mock.Setup(x => x.Equipments.All()).ReturnsAsync(labEquipmentMock);

            var labEquipmentListMock = _fixture.Create<IEnumerable<GetLabEquipmentResponse>>();
            object listValue = _mockMapper.Setup(x => x.Map<IEnumerable<GetLabEquipmentResponse>>(labEquipmentMock)).Returns(labEquipmentListMock);

            //Act
            var result = await _controller.GetAllLabEquipements().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(labEquipmentListMock.GetType());
            _mock.Verify(x => x.Equipments.All(), Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<GetLabEquipmentResponse>>(labEquipmentMock), Times.Once);

        }

        [Fact]
        public async Task GetAllLabEquipments_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            List<LabEquipment> response = null;
            object value = _mock.Setup(x => x.Equipments.All()).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAllLabEquipements().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Equipments.All(), Times.Once);

        }


        //Get LabEquipment By Id API tests

        [Fact]
        public async Task GetLabEquipmentById_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var labEquipmentMock = _fixture.Create<LabEquipment>();
            var labEquipmentId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Equipments.GetById(labEquipmentId)).ReturnsAsync(labEquipmentMock);

            var labEquipmentListMock = _fixture.Create<GetLabEquipmentByIdResponse>();
            object listValue = _mockMapper.Setup(x => x.Map<GetLabEquipmentByIdResponse>(labEquipmentMock)).Returns(labEquipmentListMock);

            //Act
            var result = await _controller.GetLabEquipment(labEquipmentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(labEquipmentListMock.GetType());
            _mock.Verify(x => x.Equipments.GetById(labEquipmentId), Times.Once);
            _mockMapper.Verify(x => x.Map<GetLabEquipmentByIdResponse>(labEquipmentMock), Times.Once);

        }

        [Fact]
        public async Task GetLabEquipmentById_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            LabEquipment response = null;
            var labEquipmentId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Equipments.GetById(labEquipmentId)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetLabEquipment(labEquipmentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Equipments.GetById(labEquipmentId), Times.Once);

        }

        //Add lab equipment API tests
        [Fact]
        public async Task AddLabEquipment_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateLabEquipmentRequest>();
            var response = _fixture.Create<LabEquipment>();

            _mockMapper.Setup(x => x.Map<LabEquipment>(request)).Returns(response);
            _mock.Setup(x => x.Equipments.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLabEquipment(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
            _mockMapper.Verify(x => x.Map<LabEquipment>(request), Times.Once);
            _mock.Verify(x => x.Equipments.Add(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task AddLabRquipment_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateLabEquipmentRequest>();
            _controller.ModelState.AddModelError("EquipmentName", "The EquipmentName field is required.");
            var response = _fixture.Create<LabEquipment>();

            _mockMapper.Setup(x => x.Map<LabEquipment>(request)).Returns(response);
            _mock.Setup(x => x.Equipments.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLabEquipment(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _mockMapper.Verify(x => x.Map<LabEquipment>(request), Times.Never);
            _mock.Verify(x => x.Equipments.Add(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Delete LabEquipment API test

        [Fact]
        public async Task DeleteLabEquipment_ShouldReturnNoContent_WhenGraduateDeleted()
        {
            //Arange

            var labEquipmentId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabEquipment>();

            _mock.Setup(x => x.Equipments.GetById(labEquipmentId)).ReturnsAsync(response);
            _mock.Setup(x => x.Equipments.Delete(labEquipmentId)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteLabEquipment(labEquipmentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mock.Verify(x => x.Equipments.GetById(labEquipmentId), Times.Once);
            _mock.Verify(x => x.Equipments.Delete(labEquipmentId), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteLabEquipment_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange

            var labEquipmentId = _fixture.Create<Guid>();
            LabEquipment labEquipmentResponce = null;

            _mock.Setup(x => x.Equipments.GetById(labEquipmentId)).ReturnsAsync(labEquipmentResponce);

            //Act
            var result = await _controller.DeleteLabEquipment(labEquipmentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Equipments.GetById(labEquipmentId), Times.Once);
            _mock.Verify(x => x.Equipments.Delete(labEquipmentId), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }

        //Update LabEquipment API tests

        [Fact]
        public async Task UpdateLabEquipment_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateLabEquipmentRequest>();
            var labEquipmentId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabEquipment>();
            _controller.ModelState.AddModelError("EquipmentName", "The EquipmentName field is required.");


            //Act
            var result = await _controller.UpdateLabEquipment(labEquipmentId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _mockMapper.Verify(x => x.Map<LabEquipment>(request), Times.Never);
            _mock.Verify(x => x.Equipments.Update(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        [Fact]
        public async Task UpdateLabEquipment_ShouldReturnNoContentResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateLabEquipmentRequest>();
            var labEquipmentId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabEquipment>();

            _mockMapper.Setup(x => x.Map<LabEquipment>(request)).Returns(response);
            _mock.Setup(x => x.Equipments.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);


            //Act
            var result = await _controller.UpdateLabEquipment(labEquipmentId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mockMapper.Verify(x => x.Map<LabEquipment>(request), Times.Once);
            _mock.Verify(x => x.Equipments.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task UpdateLabEquipment_ShouldReturnStatusCode500_WhenErrorOccursSavingToDatabase()
        {
            //Arange
            var request = _fixture.Create<UpdateLabEquipmentRequest>();
            var labEquipmentId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabEquipment>();

            _mockMapper.Setup(x => x.Map<LabEquipment>(request)).Returns(response);
            _mock.Setup(x => x.Equipments.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ThrowsAsync(new Exception("Database error occurred"));


            //Act
            var result = await _controller.UpdateLabEquipment(labEquipmentId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var statusCodeResult = (ObjectResult)result;
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("An error occurred while updating the lab equipment.");
            _mockMapper.Verify(x => x.Map<LabEquipment>(request), Times.Once);
            _mock.Verify(x => x.Equipments.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

    }
}
