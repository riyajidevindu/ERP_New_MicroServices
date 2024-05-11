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
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetById;
using ERP.LabScheduleManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests;
using ERP.LabScheduleManagement.Core.DTOs.Requests.UpdateRequests;

namespace ERP.LabScheduleManagement.Api.Tests.Controllers
{
    public class LabSpaceControllersTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _mock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LabSpaceControllers _controller;

        public LabSpaceControllersTests()
        {
            _fixture = new Fixture();
            _mock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _controller = new LabSpaceControllers(_mock.Object, _mockMapper.Object);

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        //Get All Labs API test
        [Fact]
        public async Task GetAllLabSpaces_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var labSpacesMock = _fixture.Create<IEnumerable<LabSpace>>();
            object value = _mock.Setup(x => x.Spaces.All()).ReturnsAsync(labSpacesMock);

            var labSpacesListMock = _fixture.Create<IEnumerable<GetLabSpaceResponse>>();
            object listValue = _mockMapper.Setup(x => x.Map<IEnumerable<GetLabSpaceResponse>>(labSpacesMock)).Returns(labSpacesListMock);

            //Act
            var result = await _controller.GetAllLabSpaces().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(labSpacesListMock.GetType());
            _mock.Verify(x => x.Spaces.All(), Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<GetLabSpaceResponse>>(labSpacesMock), Times.Once);

        }

        [Fact]
        public async Task GetAllLabSpaces_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            List<LabSpace> response = null;
            object value = _mock.Setup(x => x.Spaces.All()).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAllLabSpaces().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Spaces.All(), Times.Once);

        }


        //Get Lab By Id API tests

        [Fact]
        public async Task GetLabSpace_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var labSpaceMock = _fixture.Create<LabSpace>();
            var labSpsceId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Spaces.GetById(labSpsceId)).ReturnsAsync(labSpaceMock);

            var labSpaceDTOMock = _fixture.Create<GetLabSpaceByIdResponse>();
            object mappedValue = _mockMapper.Setup(x => x.Map<GetLabSpaceByIdResponse>(labSpaceMock)).Returns(labSpaceDTOMock);

            //Act
            var result = await _controller.GetLabSpace(labSpsceId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(labSpaceDTOMock.GetType());
            _mock.Verify(x => x.Spaces.GetById(labSpsceId), Times.Once);
            _mockMapper.Verify(x => x.Map<GetLabSpaceByIdResponse>(labSpaceMock), Times.Once);

        }

        [Fact]
        public async Task GetLabSpace_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            LabSpace response = null;
            var labSpaceId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Spaces.GetById(labSpaceId)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetLabSpace(labSpaceId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Spaces.GetById(labSpaceId), Times.Once);

        }



        //Add Lab Space API tests
        [Fact]
        public async Task AddLabSpace_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateLabSpaceRequest>();
            var response = _fixture.Create<LabSpace>();

            _mockMapper.Setup(x => x.Map<LabSpace>(request)).Returns(response);
            _mock.Setup(x => x.Spaces.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLabSpace(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
            _mockMapper.Verify(x => x.Map<LabSpace>(request), Times.Once);
            _mock.Verify(x => x.Spaces.Add(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task AddLabSpace_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateLabSpaceRequest>();
            _controller.ModelState.AddModelError("SpaceName", "The SpaceName field is required.");
            var response = _fixture.Create<LabSpace>();

            _mockMapper.Setup(x => x.Map<LabSpace>(request)).Returns(response);
            _mock.Setup(x => x.Spaces.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLabSpace(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _mockMapper.Verify(x => x.Map<LabSpace>(request), Times.Never);
            _mock.Verify(x => x.Spaces.Add(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Delete Lab Spaces API test

        [Fact]
        public async Task DeleteLabSpace_ShouldReturnNoContent_WhenLabSpaceDeleted()
        {
            //Arange

            var labSpaceId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabSpace>();

            _mock.Setup(x => x.Spaces.GetById(labSpaceId)).ReturnsAsync(response);
            _mock.Setup(x => x.Spaces.Delete(labSpaceId)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteLabSpace(labSpaceId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mock.Verify(x => x.Spaces.GetById(labSpaceId), Times.Once);
            _mock.Verify(x => x.Spaces.Delete(labSpaceId), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteLabSpace_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange

            var labSpaceId = _fixture.Create<Guid>();
            LabSpace labSpaceResponce = null;

            _mock.Setup(x => x.Spaces.GetById(labSpaceId)).ReturnsAsync(labSpaceResponce);

            //Act
            var result = await _controller.DeleteLabSpace(labSpaceId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Spaces.GetById(labSpaceId), Times.Once);
            _mock.Verify(x => x.Spaces.Delete(labSpaceId), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }

        //Update LabSpace API tests

        [Fact]
        public async Task UpdateLabSpace_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateLabSpaceRequest>();
            var labSpaceId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabSpace>();
            _controller.ModelState.AddModelError("SpaceName", "The SpaceName field is required.");


            //Act
            var result = await _controller.UpdateLabSpace(labSpaceId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _mockMapper.Verify(x => x.Map<LabSpace>(request), Times.Never);
            _mock.Verify(x => x.Spaces.Update(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        [Fact]
        public async Task UpdateLabSpace_ShouldReturnNoContentResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateLabSpaceRequest>();
            var labSpaceId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabSpace>();

            _mockMapper.Setup(x => x.Map<LabSpace>(request)).Returns(response);
            _mock.Setup(x => x.Spaces.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);


            //Act
            var result = await _controller.UpdateLabSpace(labSpaceId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mockMapper.Verify(x => x.Map<LabSpace>(request), Times.Once);
            _mock.Verify(x => x.Spaces.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task UpdateLabSpace_ShouldReturnStatusCode500_WhenErrorOccursSavingToDatabase()
        {
            //Arange
            var request = _fixture.Create<UpdateLabSpaceRequest>();
            var labSpaceId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabSpace>();

            _mockMapper.Setup(x => x.Map<LabSpace>(request)).Returns(response);
            _mock.Setup(x => x.Spaces.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ThrowsAsync(new Exception("Database error occurred"));


            //Act
            var result = await _controller.UpdateLabSpace(labSpaceId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var statusCodeResult = (ObjectResult)result;
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("An error occurred while updating the lab spaces.");
            _mockMapper.Verify(x => x.Map<LabSpace>(request), Times.Once);
            _mock.Verify(x => x.Spaces.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }



    }
}
