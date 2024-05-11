using AutoFixture;
using AutoMapper;
using ERP.LabScheduleManagement.Api.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.LabScheduleManagement.DataServices.Repositories.Interfaces;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll;
using ERP.LabScheduleManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetById;
using ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests;
using ERP.LabScheduleManagement.Core.DTOs.Requests.UpdateRequests;

namespace ERP.LabScheduleManagement.Api.Tests.Controllers
{
    public class LabGroupControllersTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _mock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LabGroupControllers _controller;

        public LabGroupControllersTests()
        {
            _fixture = new Fixture();
            _mock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _controller = new LabGroupControllers(_mock.Object, _mockMapper.Object);

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        //Get All Labs API test
        [Fact]
        public async Task GetAllLabGroups_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var labGroupsMock = _fixture.Create<IEnumerable<LabGroup>>();
            object value = _mock.Setup(x => x.Groups.All()).ReturnsAsync(labGroupsMock);

            var labGroupsListMock = _fixture.Create<IEnumerable<GetLabGroupResponse>>();
            object listValue = _mockMapper.Setup(x => x.Map<IEnumerable<GetLabGroupResponse>>(labGroupsMock)).Returns(labGroupsListMock);

            //Act
            var result = await _controller.GetAllLabGroups().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(labGroupsListMock.GetType());
            _mock.Verify(x => x.Groups.All(), Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<GetLabGroupResponse>>(labGroupsMock), Times.Once);

        }

        [Fact]
        public async Task GetAllLabGroups_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            List<LabGroup> response = null;
            object value = _mock.Setup(x => x.Groups.All()).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAllLabGroups().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Groups.All(), Times.Once);

        }

        //Get Lab Group By Id API tests

        [Fact]
        public async Task GetLabGroup_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var labMock = _fixture.Create<LabGroup>();
            var labGroupId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Groups.GetById(labGroupId)).ReturnsAsync(labMock);

            var labGroupDTOMock = _fixture.Create<GetLabGroupByIdResponse>();
            object mappedValue = _mockMapper.Setup(x => x.Map<GetLabGroupByIdResponse>(labMock)).Returns(labGroupDTOMock);

            //Act
            var result = await _controller.GetLabGroup(labGroupId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(labGroupDTOMock.GetType());
            _mock.Verify(x => x.Groups.GetById(labGroupId), Times.Once);
            _mockMapper.Verify(x => x.Map<GetLabGroupByIdResponse>(labMock), Times.Once);

        }

        [Fact]
        public async Task GetLabGroup_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            LabGroup response = null;
            var labGroupId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Groups.GetById(labGroupId)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetLabGroup(labGroupId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Groups.GetById(labGroupId), Times.Once);

        }

        //Add Lab API tests
        [Fact]
        public async Task AddLabGroup_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateLabGroupRequest>();
            var response = _fixture.Create<LabGroup>();

            _mockMapper.Setup(x => x.Map<LabGroup>(request)).Returns(response);
            _mock.Setup(x => x.Groups.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLabGroup(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
            _mockMapper.Verify(x => x.Map<LabGroup>(request), Times.Once);
            _mock.Verify(x => x.Groups.Add(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task AddLabGroup_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateLabGroupRequest>();
            _controller.ModelState.AddModelError("GroupNumber", "The GroupNumber field is required.");
            var response = _fixture.Create<LabGroup>();

            _mockMapper.Setup(x => x.Map<LabGroup>(request)).Returns(response);
            _mock.Setup(x => x.Groups.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLabGroup(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _mockMapper.Verify(x => x.Map<LabGroup>(request), Times.Never);
            _mock.Verify(x => x.Groups.Add(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }

        //Delete Lab API test

        [Fact]
        public async Task DeleteLabGroup_ShouldReturnNoContent_WhenLabGroupDeleted()
        {
            //Arange

            var labGroupId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabGroup>();

            _mock.Setup(x => x.Groups.GetById(labGroupId)).ReturnsAsync(response);
            _mock.Setup(x => x.Groups.Delete(labGroupId)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteLabGroup(labGroupId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mock.Verify(x => x.Groups.GetById(labGroupId), Times.Once);
            _mock.Verify(x => x.Groups.Delete(labGroupId), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteLabGroup_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange

            var labGroupId = _fixture.Create<Guid>();
            LabGroup labResponce = null;

            _mock.Setup(x => x.Groups.GetById(labGroupId)).ReturnsAsync(labResponce);

            //Act
            var result = await _controller.DeleteLabGroup(labGroupId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Groups.GetById(labGroupId), Times.Once);
            _mock.Verify(x => x.Groups.Delete(labGroupId), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }

        //Update Lab API tests

        [Fact]
        public async Task UpdateLabGroup_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateLabGroupRequest>();
            var labGroupId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabGroup>();
            _controller.ModelState.AddModelError("GroupNumber", "The GroupNumber field is required.");


            //Act
            var result = await _controller.UpdateLabGroup(labGroupId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _mockMapper.Verify(x => x.Map<LabGroup>(request), Times.Never);
            _mock.Verify(x => x.Groups.Update(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        [Fact]
        public async Task UpdateLabGroup_ShouldReturnNoContentResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateLabGroupRequest>();
            var labGroupId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabGroup>();

            _mockMapper.Setup(x => x.Map<LabGroup>(request)).Returns(response);
            _mock.Setup(x => x.Groups.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);


            //Act
            var result = await _controller.UpdateLabGroup(labGroupId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mockMapper.Verify(x => x.Map<LabGroup>(request), Times.Once);
            _mock.Verify(x => x.Groups.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task UpdateLabGroup_ShouldReturnStatusCode500_WhenErrorOccursSavingToDatabase()
        {
            //Arange
            var request = _fixture.Create<UpdateLabGroupRequest>();
            var labGroupId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabGroup>();

            _mockMapper.Setup(x => x.Map<LabGroup>(request)).Returns(response);
            _mock.Setup(x => x.Groups.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ThrowsAsync(new Exception("Database error occurred"));


            //Act
            var result = await _controller.UpdateLabGroup(labGroupId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var statusCodeResult = (ObjectResult)result;
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("An error occurred while updating the lab group.");
            _mockMapper.Verify(x => x.Map<LabGroup>(request), Times.Once);
            _mock.Verify(x => x.Groups.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }
    }
}
