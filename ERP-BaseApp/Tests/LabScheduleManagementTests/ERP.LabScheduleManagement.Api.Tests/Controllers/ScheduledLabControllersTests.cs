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
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetById;
using ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests;
using ERP.LabScheduleManagement.Core.DTOs.Requests.UpdateRequests;

namespace ERP.LabScheduleManagement.Api.Tests.Controllers
{
    public class ScheduledLabControllersTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _mock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ScheduledLabControllers _controller;

        public ScheduledLabControllersTests()
        {
            _fixture = new Fixture();
            _mock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _controller = new ScheduledLabControllers(_mock.Object, _mockMapper.Object);

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        //Get All Scheduled lab tests

        [Fact]
        public async Task GetAllScheduledLab_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var scheduledLabMock = _fixture.Create<IEnumerable<ScheduledLab>>();
            object value = _mock.Setup(x => x.ScheduledLabs.All()).ReturnsAsync(scheduledLabMock);

            var scheduledLabListMock = _fixture.Create<IEnumerable<GetScheduledLabResponse>>();
            object listValue = _mockMapper.Setup(x => x.Map<IEnumerable<GetScheduledLabResponse>>(scheduledLabMock)).Returns(scheduledLabListMock);

            //Act
            var result = await _controller.GetAllScheduledLab().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(scheduledLabListMock.GetType());
            _mock.Verify(x => x.ScheduledLabs.All(), Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<GetScheduledLabResponse>>(scheduledLabMock), Times.Once);

        }

        [Fact]
        public async Task GetAllScheduledLab_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            List<ScheduledLab> response = null;
            object value = _mock.Setup(x => x.ScheduledLabs.All()).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAllScheduledLab().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.ScheduledLabs.All(), Times.Once);

        }


        //Get Scheduled Lab By Id API tests

        [Fact]
        public async Task GetScheduledLab_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var scheduledLabMock = _fixture.Create<ScheduledLab>();
            var scheduledLabId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.ScheduledLabs.GetById(scheduledLabId)).ReturnsAsync(scheduledLabMock);

            var scheduledLabDTOMock = _fixture.Create<GetScheduledLabByIdResponse>();
            object mappedValue = _mockMapper.Setup(x => x.Map<GetScheduledLabByIdResponse>(scheduledLabMock)).Returns(scheduledLabDTOMock);

            //Act
            var result = await _controller.GetScheduledLab(scheduledLabId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(scheduledLabDTOMock.GetType());
            _mock.Verify(x => x.ScheduledLabs.GetById(scheduledLabId), Times.Once);
            _mockMapper.Verify(x => x.Map<GetScheduledLabByIdResponse>(scheduledLabMock), Times.Once);

        }

        [Fact]
        public async Task GetScheduledLab_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            ScheduledLab response = null;
            var scheduledLabId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.ScheduledLabs.GetById(scheduledLabId)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetScheduledLab(scheduledLabId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.ScheduledLabs.GetById(scheduledLabId), Times.Once);

        }



        //Add Scheduled Lab API tests
        [Fact]
        public async Task AddScheduledLab_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateScheduledLabRequest>();
            var response = _fixture.Create<ScheduledLab>();

            _mockMapper.Setup(x => x.Map<ScheduledLab>(request)).Returns(response);
            _mock.Setup(x => x.ScheduledLabs.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddScheduledLab(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
            _mockMapper.Verify(x => x.Map<ScheduledLab>(request), Times.Once);
            _mock.Verify(x => x.ScheduledLabs.Add(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task AddScheduledLab_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateScheduledLabRequest>();
            _controller.ModelState.AddModelError("Completed", "The Completed field is required.");
            var response = _fixture.Create<ScheduledLab>();

            _mockMapper.Setup(x => x.Map<ScheduledLab>(request)).Returns(response);
            _mock.Setup(x => x.ScheduledLabs.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddScheduledLab(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _mockMapper.Verify(x => x.Map<Module>(request), Times.Never);
            _mock.Verify(x => x.ScheduledLabs.Add(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Delete Scheduled Lab API test

        [Fact]
        public async Task DeleteModule_ShouldReturnNoContent_WhenModuleDeleted()
        {
            //Arange

            var scheduledLabId = _fixture.Create<Guid>();
            var response = _fixture.Create<ScheduledLab>();

            _mock.Setup(x => x.ScheduledLabs.GetById(scheduledLabId)).ReturnsAsync(response);
            _mock.Setup(x => x.ScheduledLabs.Delete(scheduledLabId)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteScheduledLab(scheduledLabId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mock.Verify(x => x.ScheduledLabs.GetById(scheduledLabId), Times.Once);
            _mock.Verify(x => x.ScheduledLabs.Delete(scheduledLabId), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteScheduledLab_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange

            var scheduledLabId = _fixture.Create<Guid>();
            ScheduledLab scheduledLanResponce = null;

            _mock.Setup(x => x.ScheduledLabs.GetById(scheduledLabId)).ReturnsAsync(scheduledLanResponce);

            //Act
            var result = await _controller.DeleteScheduledLab(scheduledLabId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.ScheduledLabs.GetById(scheduledLabId), Times.Once);
            _mock.Verify(x => x.ScheduledLabs.Delete(scheduledLabId), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Update Scheduled Lab API tests

        [Fact]
        public async Task UpdateScheduledLab_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateScheduledLabRequest>();
            var moduleId = _fixture.Create<Guid>();
            var response = _fixture.Create<ScheduledLab>();
            _controller.ModelState.AddModelError("Completed", "The Completed field is required.");


            //Act
            var result = await _controller.UpdateScheduledLab(moduleId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _mockMapper.Verify(x => x.Map<ScheduledLab>(request), Times.Never);
            _mock.Verify(x => x.ScheduledLabs.Update(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        [Fact]
        public async Task UpdateScheduledlab_ShouldReturnNoContentResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateScheduledLabRequest>();
            var scheduledLabId = _fixture.Create<Guid>();
            var response = _fixture.Create<ScheduledLab>();

            _mockMapper.Setup(x => x.Map<ScheduledLab>(request)).Returns(response);
            _mock.Setup(x => x.ScheduledLabs.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);


            //Act
            var result = await _controller.UpdateScheduledLab(scheduledLabId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mockMapper.Verify(x => x.Map<ScheduledLab>(request), Times.Once);
            _mock.Verify(x => x.ScheduledLabs.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task UpdateScheduledLab_ShouldReturnStatusCode500_WhenErrorOccursSavingToDatabase()
        {
            //Arange
            var request = _fixture.Create<UpdateScheduledLabRequest>();
            var moduleId = _fixture.Create<Guid>();
            var response = _fixture.Create<ScheduledLab>();

            _mockMapper.Setup(x => x.Map<ScheduledLab>(request)).Returns(response);
            _mock.Setup(x => x.ScheduledLabs.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ThrowsAsync(new Exception("Database error occurred"));


            //Act
            var result = await _controller.UpdateScheduledLab(moduleId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var statusCodeResult = (ObjectResult)result;
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("An error occurred while updating the lab schedule.");
            _mockMapper.Verify(x => x.Map<ScheduledLab>(request), Times.Once);
            _mock.Verify(x => x.ScheduledLabs.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }
    }
}
