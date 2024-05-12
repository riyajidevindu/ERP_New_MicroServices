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
    public class LabInstructorControllersTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _mock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LabInstructorControllers _controller;

        public LabInstructorControllersTests()
        {
            _fixture = new Fixture();
            _mock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _controller = new LabInstructorControllers(_mock.Object, _mockMapper.Object);

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        //Get All Lab Instructors API test
        [Fact]
        public async Task GetAllLabInstructors_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var labInstructorsMock = _fixture.Create<IEnumerable<LabInstructor>>();
            object value = _mock.Setup(x => x.Instructors.All()).ReturnsAsync(labInstructorsMock);

            var labInstructorsListMock = _fixture.Create<IEnumerable<GetLabInstructorResponse>>();
            object listValue = _mockMapper.Setup(x => x.Map<IEnumerable<GetLabInstructorResponse>>(labInstructorsMock)).Returns(labInstructorsListMock);

            //Act
            var result = await _controller.GetAllLabInstructors().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(labInstructorsListMock.GetType());
            _mock.Verify(x => x.Instructors.All(), Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<GetLabInstructorResponse>>(labInstructorsMock), Times.Once);

        }

        [Fact]
        public async Task GetAllLabInstructors_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            List<LabInstructor> response = null;
            object value = _mock.Setup(x => x.Instructors.All()).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAllLabInstructors().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Instructors.All(), Times.Once);

        }

        //Get Lab Instructor By Id API tests

        [Fact]
        public async Task GetLabInstructor_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var labInstructorMock = _fixture.Create<LabInstructor>();
            var labInstructorId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Instructors.GetById(labInstructorId)).ReturnsAsync(labInstructorMock);

            var labInstructorDTOMock = _fixture.Create<GetLabInstructorByIdResponse>();
            object mappedValue = _mockMapper.Setup(x => x.Map<GetLabInstructorByIdResponse>(labInstructorMock)).Returns(labInstructorDTOMock);

            //Act
            var result = await _controller.GetLabInstructor(labInstructorId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(labInstructorDTOMock.GetType());
            _mock.Verify(x => x.Instructors.GetById(labInstructorId), Times.Once);
            _mockMapper.Verify(x => x.Map<GetLabInstructorByIdResponse>(labInstructorMock), Times.Once);

        }

        [Fact]
        public async Task GetLabInstructor_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            LabInstructor response = null;
            var labInstructorId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Instructors.GetById(labInstructorId)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetLabInstructor(labInstructorId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Instructors.GetById(labInstructorId), Times.Once);

        }

        //Add Lab Instructor API tests
        [Fact]
        public async Task AddLabInstructor_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateLabInstructorRequest>();
            var response = _fixture.Create<LabInstructor>();

            _mockMapper.Setup(x => x.Map<LabInstructor>(request)).Returns(response);
            _mock.Setup(x => x.Instructors.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLabInstructor(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
            _mockMapper.Verify(x => x.Map<LabInstructor>(request), Times.Once);
            _mock.Verify(x => x.Instructors.Add(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task AddLabInstructor_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateLabInstructorRequest>();
            _controller.ModelState.AddModelError("InstructorName", "The InstructorName field is required.");
            var response = _fixture.Create<LabInstructor>();

            _mockMapper.Setup(x => x.Map<LabInstructor>(request)).Returns(response);
            _mock.Setup(x => x.Instructors.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddLabInstructor(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _mockMapper.Verify(x => x.Map<LabInstructor>(request), Times.Never);
            _mock.Verify(x => x.Instructors.Add(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }

        //Delete Lab Instructor API test

        [Fact]
        public async Task DeleteLabInstructor_ShouldReturnNoContent_WhenLabInstructorDeleted()
        {
            //Arange

            var labInstructorId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabInstructor>();

            _mock.Setup(x => x.Instructors.GetById(labInstructorId)).ReturnsAsync(response);
            _mock.Setup(x => x.Instructors.Delete(labInstructorId)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteLabInstructor(labInstructorId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mock.Verify(x => x.Instructors.GetById(labInstructorId), Times.Once);
            _mock.Verify(x => x.Instructors.Delete(labInstructorId), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteLabInstructor_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange

            var labInstructorId = _fixture.Create<Guid>();
            LabInstructor labResponce = null;

            _mock.Setup(x => x.Instructors.GetById(labInstructorId)).ReturnsAsync(labResponce);

            //Act
            var result = await _controller.DeleteLabInstructor(labInstructorId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Instructors.GetById(labInstructorId), Times.Once);
            _mock.Verify(x => x.Instructors.Delete(labInstructorId), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }

        //Update Lab Instructor API tests

        [Fact]
        public async Task UpdateLabInstructor_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateLabInstructorRequest>();
            var labInstructorId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabInstructor>();
            _controller.ModelState.AddModelError("InstructorName", "The InstructorName field is required.");


            //Act
            var result = await _controller.UpdateLabInstructor(labInstructorId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _mockMapper.Verify(x => x.Map<LabInstructor>(request), Times.Never);
            _mock.Verify(x => x.Instructors.Update(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        [Fact]
        public async Task UpdateLabInstrctor_ShouldReturnNoContentResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateLabInstructorRequest>();
            var labInstructorId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabInstructor>();

            _mockMapper.Setup(x => x.Map<LabInstructor>(request)).Returns(response);
            _mock.Setup(x => x.Instructors.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);


            //Act
            var result = await _controller.UpdateLabInstructor(labInstructorId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mockMapper.Verify(x => x.Map<LabInstructor>(request), Times.Once);
            _mock.Verify(x => x.Instructors.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task UpdateLabInstructor_ShouldReturnStatusCode500_WhenErrorOccursSavingToDatabase()
        {
            //Arange
            var request = _fixture.Create<UpdateLabInstructorRequest>();
            var labInstructorId = _fixture.Create<Guid>();
            var response = _fixture.Create<LabInstructor>();

            _mockMapper.Setup(x => x.Map<LabInstructor>(request)).Returns(response);
            _mock.Setup(x => x.Instructors.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ThrowsAsync(new Exception("Database error occurred"));


            //Act
            var result = await _controller.UpdateLabInstructor(labInstructorId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var statusCodeResult = (ObjectResult)result;
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("An error occurred while updating the lab instructors.");
            _mockMapper.Verify(x => x.Map<LabInstructor>(request), Times.Once);
            _mock.Verify(x => x.Instructors.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }
    }
}
