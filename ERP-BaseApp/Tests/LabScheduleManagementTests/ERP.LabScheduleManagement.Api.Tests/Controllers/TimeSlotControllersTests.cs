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
using ERP.LabScheduleManagement.Core.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetById;
using ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests;
using ERP.LabScheduleManagement.Core.DTOs.Requests.UpdateRequests;

namespace ERP.LabScheduleManagement.Api.Tests.Controllers
{
    public class TimeSlotControllersTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _mock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TimeSlotControllers _controller;

        public TimeSlotControllersTests()
        {
            _fixture = new Fixture();
            _mock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _controller = new TimeSlotControllers(_mock.Object, _mockMapper.Object);

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _fixture.Customize<DateOnly>(composer =>
            {
                return composer.FromFactory(() => DateOnly.FromDateTime(DateTime.Now)); 
            });

            _fixture.Customize<TimeOnly>(composer =>
            {
                return composer.FromFactory(() => TimeOnly.FromDateTime(DateTime.Now));
            });
        }

        //Get All TimeSlots API test
        [Fact]
        public async Task GetAllTimeSlot_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var timeSlotMock = _fixture.Create<IEnumerable<TimeSlot>>();
            object value = _mock.Setup(x => x.TimeSlots.All()).ReturnsAsync(timeSlotMock);

            var timeSlotListMock = _fixture.Create<IEnumerable<GetTimeSlotResponse>>();
            object listValue = _mockMapper.Setup(x => x.Map<IEnumerable<GetTimeSlotResponse>>(timeSlotMock)).Returns(timeSlotListMock);

            //Act
            var result = await _controller.GetAllTimeSlots().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(timeSlotListMock.GetType());
            _mock.Verify(x => x.TimeSlots.All(), Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<GetTimeSlotResponse>>(timeSlotMock), Times.Once);

        }

        [Fact]
        public async Task GetAllTimeSlot_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            List<TimeSlot> response = null;
            object value = _mock.Setup(x => x.TimeSlots.All()).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAllTimeSlots().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.TimeSlots.All(), Times.Once);

        }


        //Get TimeSlot By Id API tests

        [Fact]
        public async Task GetTimeSlot_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var timeSlotMock = _fixture.Create<TimeSlot>();
            var timeSlotId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.TimeSlots.GetById(timeSlotId)).ReturnsAsync(timeSlotMock);

            var timeSlotDTOMock = _fixture.Create<GetTimeSlotByIdResponse>();
            object mappedValue = _mockMapper.Setup(x => x.Map<GetTimeSlotByIdResponse>(timeSlotMock)).Returns(timeSlotDTOMock);

            //Act
            var result = await _controller.GetTimeSlot(timeSlotId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(timeSlotDTOMock.GetType());
            _mock.Verify(x => x.TimeSlots.GetById(timeSlotId), Times.Once);
            _mockMapper.Verify(x => x.Map<GetTimeSlotByIdResponse>(timeSlotMock), Times.Once);

        }

        [Fact]
        public async Task GetTimeSlot_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            TimeSlot response = null;
            var timeSlotId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.TimeSlots.GetById(timeSlotId)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetTimeSlot(timeSlotId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.TimeSlots.GetById(timeSlotId), Times.Once);

        }



        //Add TimeSlot API tests
        [Fact]
        public async Task AddTimeSlot_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateTimeSlotRequest>();
            var response = _fixture.Create<TimeSlot>();

            _mockMapper.Setup(x => x.Map<TimeSlot>(request)).Returns(response);
            _mock.Setup(x => x.TimeSlots.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddTimeSlot(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
            _mockMapper.Verify(x => x.Map<TimeSlot>(request), Times.Once);
            _mock.Verify(x => x.TimeSlots.Add(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task AddTimeSlot_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateTimeSlotRequest>();
            _controller.ModelState.AddModelError("TimeSlot", "The TimeSlot field is required.");
            var response = _fixture.Create<TimeSlot>();

            _mockMapper.Setup(x => x.Map<TimeSlot>(request)).Returns(response);
            _mock.Setup(x => x.TimeSlots.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddTimeSlot(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _mockMapper.Verify(x => x.Map<TimeSlot>(request), Times.Never);
            _mock.Verify(x => x.TimeSlots.Add(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Delete TImeSLot API test

        [Fact]
        public async Task DeleteTimeSlot_ShouldReturnNoContent_WhenStudentDeleted()
        {
            //Arange

            var timeSlotId = _fixture.Create<Guid>();
            var response = _fixture.Create<TimeSlot>();

            _mock.Setup(x => x.TimeSlots.GetById(timeSlotId)).ReturnsAsync(response);
            _mock.Setup(x => x.TimeSlots.Delete(timeSlotId)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteTimeSlot(timeSlotId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mock.Verify(x => x.TimeSlots.GetById(timeSlotId), Times.Once);
            _mock.Verify(x => x.TimeSlots.Delete(timeSlotId), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteTimeSlot_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange

            var timeSlotId = _fixture.Create<Guid>();
            TimeSlot timeSlotResponce = null;

            _mock.Setup(x => x.TimeSlots.GetById(timeSlotId)).ReturnsAsync(timeSlotResponce);

            //Act
            var result = await _controller.DeleteTimeSlot(timeSlotId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.TimeSlots.GetById(timeSlotId), Times.Once);
            _mock.Verify(x => x.TimeSlots.Delete(timeSlotId), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Update TimeSlot API tests

        [Fact]
        public async Task UpdateTimeSlot_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateTimeSlotRequest>();
            var studentId = _fixture.Create<Guid>();
            var response = _fixture.Create<TimeSlot>();
            _controller.ModelState.AddModelError("TimeSlot", "The TImeSlot field is required.");


            //Act
            var result = await _controller.UpdateTimeSlot(studentId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _mockMapper.Verify(x => x.Map<TimeSlot>(request), Times.Never);
            _mock.Verify(x => x.TimeSlots.Update(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        [Fact]
        public async Task UpdateTimeSlot_ShouldReturnNoContentResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateTimeSlotRequest>();
            var timeSlotId = _fixture.Create<Guid>();
            var response = _fixture.Create<TimeSlot>();

            _mockMapper.Setup(x => x.Map<TimeSlot>(request)).Returns(response);
            _mock.Setup(x => x.TimeSlots.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);


            //Act
            var result = await _controller.UpdateTimeSlot(timeSlotId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mockMapper.Verify(x => x.Map<TimeSlot>(request), Times.Once);
            _mock.Verify(x => x.TimeSlots.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task UpdateTimeSlot_ShouldReturnStatusCode500_WhenErrorOccursSavingToDatabase()
        {
            //Arange
            var request = _fixture.Create<UpdateTimeSlotRequest>();
            var timeSlotId = _fixture.Create<Guid>();
            var response = _fixture.Create<TimeSlot>();

            _mockMapper.Setup(x => x.Map<TimeSlot>(request)).Returns(response);
            _mock.Setup(x => x.TimeSlots.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ThrowsAsync(new Exception("Database error occurred"));


            //Act
            var result = await _controller.UpdateTimeSlot(timeSlotId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var statusCodeResult = (ObjectResult)result;
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("An error occurred while updating the timeslot.");
            _mockMapper.Verify(x => x.Map<TimeSlot>(request), Times.Once);
            _mock.Verify(x => x.TimeSlots.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }
    }
}
