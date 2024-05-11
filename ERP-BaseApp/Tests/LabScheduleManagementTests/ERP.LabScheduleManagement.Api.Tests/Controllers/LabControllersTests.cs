using AutoFixture;
using AutoMapper;
using ERP.LabScheduleManagement.Api.Controllers;
using Moq;
using ERP.LabScheduleManagement.DataServices.Repositories.Interfaces;
using ERP.LabScheduleManagement.Core.Entities;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetById;
using ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests;
using ERP.LabScheduleManagement.Core.DTOs.Requests.UpdateRequests;

namespace ERP.LabScheduleManagement.Api.Tests.Controllers
{
    public class LabControllersTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _mock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LabControllers _controller;

        public LabControllersTests()
        {
            _fixture = new Fixture();
            _mock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _controller = new LabControllers(_mock.Object, _mockMapper.Object);

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
       

        //Get All Labs API test
        [Fact]
        public async Task GetAllLabs_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var labsMock = _fixture.Create<IEnumerable<Lab>>();
            object value = _mock.Setup(x => x.Labs.All()).ReturnsAsync(labsMock);

            var labsListMock = _fixture.Create<IEnumerable<GetLabResponse>>();
            object listValue = _mockMapper.Setup(x => x.Map<IEnumerable<GetLabResponse>>(labsMock)).Returns(labsListMock);

            //Act
            var result = await _controller.GetAllLabs().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(labsListMock.GetType());
            _mock.Verify(x => x.Labs.All(), Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<GetLabResponse>>(labsMock), Times.Once);

        }

        [Fact]
        public async Task GetAllLabs_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            List<Lab> response = null;
            object value = _mock.Setup(x => x.Labs.All()).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAllLabs().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Labs.All(), Times.Once);

        }


        //Get Lab By Id API tests

        [Fact]
        public async Task GetLab_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var labMock = _fixture.Create<Lab>();
            var labId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Labs.GetById(labId)).ReturnsAsync(labMock);

            var labDTOMock = _fixture.Create<GetLabByIdResponse>();
            object mappedValue = _mockMapper.Setup(x => x.Map<GetLabByIdResponse>(labMock)).Returns(labDTOMock);

            //Act
            var result = await _controller.GetLab(labId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(labDTOMock.GetType());
            _mock.Verify(x => x.Labs.GetById(labId), Times.Once);
            _mockMapper.Verify(x => x.Map<GetLabByIdResponse>(labMock), Times.Once);

        }

        [Fact]
        public async Task GetLab_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            Lab response = null;
            var labId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Labs.GetById(labId)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetLab(labId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Labs.GetById(labId), Times.Once);

        }



        //Add Lab API tests
        [Fact]
        public async Task AddLab_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateLabRequest>();
            var response = _fixture.Create<Lab>();

            _mockMapper.Setup(x => x.Map<Lab>(request)).Returns(response);
            _mock.Setup(x => x.Labs.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLab(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
            _mockMapper.Verify(x => x.Map<Lab>(request), Times.Once);
            _mock.Verify(x => x.Labs.Add(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task AddLab_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateLabRequest>();
            _controller.ModelState.AddModelError("LabName", "The LabName field is required.");
            var response = _fixture.Create<Lab>();

            _mockMapper.Setup(x => x.Map<Lab>(request)).Returns(response);
            _mock.Setup(x => x.Labs.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLab(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _mockMapper.Verify(x => x.Map<Lab>(request), Times.Never);
            _mock.Verify(x => x.Labs.Add(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Delete Lab API test

        [Fact]
        public async Task DeleteLab_ShouldReturnNoContent_WhenLabDeleted()
        {
            //Arange

            var labId = _fixture.Create<Guid>();
            var response = _fixture.Create<Lab>();

            _mock.Setup(x => x.Labs.GetById(labId)).ReturnsAsync(response);
            _mock.Setup(x => x.Labs.Delete(labId)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteLab(labId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mock.Verify(x => x.Labs.GetById(labId), Times.Once);
            _mock.Verify(x => x.Labs.Delete(labId), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteLab_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange

            var labId = _fixture.Create<Guid>();
            Lab labResponce = null;

            _mock.Setup(x => x.Labs.GetById(labId)).ReturnsAsync(labResponce);

            //Act
            var result = await _controller.DeleteLab(labId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Labs.GetById(labId), Times.Once);
            _mock.Verify(x => x.Labs.Delete(labId), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Update Lab API tests

        [Fact]
        public async Task UpdateLab_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateLabRequest>();
            var labEquipmentId = _fixture.Create<Guid>();
            var response = _fixture.Create<Lab>();
            _controller.ModelState.AddModelError("EquipmentName", "The EquipmentName field is required.");


            //Act
            var result = await _controller.UpdateLab(labEquipmentId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _mockMapper.Verify(x => x.Map<Lab>(request), Times.Never);
            _mock.Verify(x => x.Labs.Update(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        [Fact]
        public async Task UpdateLab_ShouldReturnNoContentResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateLabRequest>();
            var labId = _fixture.Create<Guid>();
            var response = _fixture.Create<Lab>();

            _mockMapper.Setup(x => x.Map<Lab>(request)).Returns(response);
            _mock.Setup(x => x.Labs.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);


            //Act
            var result = await _controller.UpdateLab(labId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mockMapper.Verify(x => x.Map<Lab>(request), Times.Once);
            _mock.Verify(x => x.Labs.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task UpdateLab_ShouldReturnStatusCode500_WhenErrorOccursSavingToDatabase()
        {
            //Arange
            var request = _fixture.Create<UpdateLabRequest>();
            var labEquipmentId = _fixture.Create<Guid>();
            var response = _fixture.Create<Lab>();

            _mockMapper.Setup(x => x.Map<Lab>(request)).Returns(response);
            _mock.Setup(x => x.Labs.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ThrowsAsync(new Exception("Database error occurred"));


            //Act
            var result = await _controller.UpdateLab(labEquipmentId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var statusCodeResult = (ObjectResult)result;
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("An error occurred while updating the lab.");
            _mockMapper.Verify(x => x.Map<Lab>(request), Times.Once);
            _mock.Verify(x => x.Labs.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

    }
}