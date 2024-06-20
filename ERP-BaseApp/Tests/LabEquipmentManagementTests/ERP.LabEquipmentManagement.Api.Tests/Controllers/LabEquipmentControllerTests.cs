using AutoFixture;
using AutoMapper;
using ERP.LabEquipmentManagement.Api.Controllers;
using ERP.LabEquipmentManagement.Core.DTOs.Requests;
using ERP.LabEquipmentManagement.Core.DTOs.Responses;
using ERP.LabEquipmentManagement.Core.Entities;
using ERP.LabEquipmentManagement.DataService.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ERP.LabEquipmentManagement.Api.Tests.Controllers
{
    public class LabEquipmentControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _mock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LabEquipmentController _controller;

        public LabEquipmentControllerTests()
        {
            _fixture = new Fixture();
            _mock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            //_controller = new LabEquipmentController(_mock.Object, _mockMapper.Object);
        }

        //Get All LabEquipments API test
        [Fact]
        public async Task GetAllLabEquipments_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var labEquipmentMock = _fixture.Create<IEnumerable<LabEquipment>>();
            object value = _mock.Setup(x => x.LabEquipments.All()).ReturnsAsync(labEquipmentMock);

            var labEquipmentListMock = _fixture.Create<IEnumerable<GetLabEquipmentResponse>>();
            object listValue = _mockMapper.Setup(x => x.Map<IEnumerable<GetLabEquipmentResponse>>(labEquipmentMock)).Returns(labEquipmentListMock);

            //Act
            var result = await _controller.GetAllLabEquipment().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(labEquipmentListMock.GetType());
            _mock.Verify(x => x.LabEquipments.All(), Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<GetLabEquipmentResponse>>(labEquipmentMock), Times.Once);

        }

        [Fact]
        public async Task GetAllLabEquipment_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            List<LabEquipment> response = null;
            object value = _mock.Setup(x => x.LabEquipments.All()).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAllLabEquipment().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.LabEquipments.All(), Times.Once);

        }


        //Get LabEquipment By Id API tests

        [Fact]
        public async Task GetLabEquipmentById_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var labEquipmentMock = _fixture.Create<LabEquipment>();
            var labEquipmentId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.LabEquipments.GetById(labEquipmentId)).ReturnsAsync(labEquipmentMock);

            var labEquipmentListMock = _fixture.Create<GetLabEquipmentResponse>();
            object listValue = _mockMapper.Setup(x => x.Map<GetLabEquipmentResponse>(labEquipmentMock)).Returns(labEquipmentListMock);

            //Act
            var result = await _controller.GetLabEquipment(labEquipmentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(labEquipmentListMock.GetType());
            _mock.Verify(x => x.LabEquipments.GetById(labEquipmentId), Times.Once);
            _mockMapper.Verify(x => x.Map<GetLabEquipmentResponse>(labEquipmentMock), Times.Once);

        }

        [Fact]
        public async Task GetLabEquipmentById_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            LabEquipment response = null;
            var labEquipmentId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.LabEquipments.GetById(labEquipmentId)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetLabEquipment(labEquipmentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.LabEquipments.GetById(labEquipmentId), Times.Once);

        }


        //Add lab equipment API tests
        [Fact]
        public async Task AddLabEquipment_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateLabEquipmentRequest>();
            var response = _fixture.Create<LabEquipment>();

            _mockMapper.Setup(x => x.Map<LabEquipment>(request)).Returns(response);
            _mock.Setup(x => x.LabEquipments.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLabEquipment(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
            _mockMapper.Verify(x => x.Map<LabEquipment>(request), Times.Once);
            _mock.Verify(x => x.LabEquipments.Add(response), Times.Once);
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
            _mock.Setup(x => x.LabEquipments.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLabEquipment(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _mockMapper.Verify(x => x.Map<LabEquipment>(request), Times.Never);
            _mock.Verify(x => x.LabEquipments.Add(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Delete LabEquipment API test

        [Fact]
        public async Task DeleteLabEquipment_ShouldReturnNoContent_WhenGraduateDeleted()
        {
            //Arange

            var labEquipmentId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabEquipment>();

            _mock.Setup(x => x.LabEquipments.GetById(labEquipmentId)).ReturnsAsync(response);
            _mock.Setup(x => x.LabEquipments.Delete(labEquipmentId)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteLabEquipment(labEquipmentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mock.Verify(x => x.LabEquipments.GetById(labEquipmentId), Times.Once);
            _mock.Verify(x => x.LabEquipments.Delete(labEquipmentId), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteLabEquipment_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange

            var labEquipmentId = _fixture.Create<Guid>();
            LabEquipment labEquipmentResponce = null;

            _mock.Setup(x => x.LabEquipments.GetById(labEquipmentId)).ReturnsAsync(labEquipmentResponce);

            //Act
            var result = await _controller.DeleteLabEquipment(labEquipmentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.LabEquipments.GetById(labEquipmentId), Times.Once);
            _mock.Verify(x => x.LabEquipments.Delete(labEquipmentId), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Update Graduate API tests

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
            result.Should().BeAssignableTo<BadRequestResult>();
            _mockMapper.Verify(x => x.Map<LabEquipment>(request), Times.Never);
            _mock.Verify(x => x.LabEquipments.Update(response), Times.Never);
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
            _mock.Setup(x => x.LabEquipments.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);


            //Act
            var result = await _controller.UpdateLabEquipment(labEquipmentId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mockMapper.Verify(x => x.Map<LabEquipment>(request), Times.Once);
            _mock.Verify(x => x.LabEquipments.Update(response), Times.Once);
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
            _mock.Setup(x => x.LabEquipments.Update(response)).ReturnsAsync(true);
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
            _mock.Verify(x => x.LabEquipments.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }
    }
}