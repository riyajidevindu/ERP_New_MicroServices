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
using ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests;
using ERP.LabScheduleManagement.Core.DTOs.Requests.UpdateRequests;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetById;
using ERP.LabScheduleManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace ERP.LabScheduleManagement.Api.Tests.Controllers
{
    public class ModuleControllersTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _mock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ModuleControllers _controller;

        public ModuleControllersTests()
        {
            _fixture = new Fixture();
            _mock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _controller = new ModuleControllers(_mock.Object, _mockMapper.Object);

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }



        //Get All Modules API test
        [Fact]
        public async Task GetAllModules_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var modulesMock = _fixture.Create<IEnumerable<Module>>();
            object value = _mock.Setup(x => x.Modules.All()).ReturnsAsync(modulesMock);

            var modulesListMock = _fixture.Create<IEnumerable<GetModuleResponse>>();
            object listValue = _mockMapper.Setup(x => x.Map<IEnumerable<GetModuleResponse>>(modulesMock)).Returns(modulesListMock);

            //Act
            var result = await _controller.GetAllModule().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(modulesListMock.GetType());
            _mock.Verify(x => x.Modules.All(), Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<GetModuleResponse>>(modulesMock), Times.Once);

        }

        [Fact]
        public async Task GetAllModules_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            List<Module> response = null;
            object value = _mock.Setup(x => x.Modules.All()).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAllModule().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Modules.All(), Times.Once);

        }


        //Get Module By Id API tests

        [Fact]
        public async Task GetModule_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var moduleMock = _fixture.Create<Module>();
            var moduleId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Modules.GetById(moduleId)).ReturnsAsync(moduleMock);

            var moduleDTOMock = _fixture.Create<GetModuleByIdResponse>();
            object mappedValue = _mockMapper.Setup(x => x.Map<GetModuleByIdResponse>(moduleMock)).Returns(moduleDTOMock);

            //Act
            var result = await _controller.GetModule(moduleId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(moduleDTOMock.GetType());
            _mock.Verify(x => x.Modules.GetById(moduleId), Times.Once);
            _mockMapper.Verify(x => x.Map<GetModuleByIdResponse>(moduleMock), Times.Once);

        }

        [Fact]
        public async Task GetModule_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            Module response = null;
            var moduleId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Modules.GetById(moduleId)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetModule(moduleId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Modules.GetById(moduleId), Times.Once);

        }



        //Add Module API tests
        [Fact]
        public async Task AddModule_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateModuleRequest>();
            var response = _fixture.Create<Module>();

            _mockMapper.Setup(x => x.Map<Module>(request)).Returns(response);
            _mock.Setup(x => x.Modules.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddModule(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
            _mockMapper.Verify(x => x.Map<Module>(request), Times.Once);
            _mock.Verify(x => x.Modules.Add(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task AddModule_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateModuleRequest>();
            _controller.ModelState.AddModelError("ModuleName", "The ModuleName field is required.");
            var response = _fixture.Create<Module>();

            _mockMapper.Setup(x => x.Map<Module>(request)).Returns(response);
            _mock.Setup(x => x.Modules.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddModule(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _mockMapper.Verify(x => x.Map<Module>(request), Times.Never);
            _mock.Verify(x => x.Modules.Add(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Delete Module API test

        [Fact]
        public async Task DeleteModule_ShouldReturnNoContent_WhenModuleDeleted()
        {
            //Arange

            var moduleId = _fixture.Create<Guid>();
            var response = _fixture.Create<Module>();

            _mock.Setup(x => x.Modules.GetById(moduleId)).ReturnsAsync(response);
            _mock.Setup(x => x.Modules.Delete(moduleId)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteModule(moduleId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mock.Verify(x => x.Modules.GetById(moduleId), Times.Once);
            _mock.Verify(x => x.Modules.Delete(moduleId), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteModule_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange

            var moduleId = _fixture.Create<Guid>();
            Module moduleResponce = null;

            _mock.Setup(x => x.Modules.GetById(moduleId)).ReturnsAsync(moduleResponce);

            //Act
            var result = await _controller.DeleteModule(moduleId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Modules.GetById(moduleId), Times.Once);
            _mock.Verify(x => x.Modules.Delete(moduleId), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Update Lab API tests

        [Fact]
        public async Task UpdateModule_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateModuleRequest>();
            var moduleId = _fixture.Create<Guid>();
            var response = _fixture.Create<Module>();
            _controller.ModelState.AddModelError("ModuleName", "The ModuleName field is required.");


            //Act
            var result = await _controller.UpdateModule(moduleId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _mockMapper.Verify(x => x.Map<Module>(request), Times.Never);
            _mock.Verify(x => x.Modules.Update(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        [Fact]
        public async Task UpdateModule_ShouldReturnNoContentResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateModuleRequest>();
            var moduleId = _fixture.Create<Guid>();
            var response = _fixture.Create<Module>();

            _mockMapper.Setup(x => x.Map<Module>(request)).Returns(response);
            _mock.Setup(x => x.Modules.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);


            //Act
            var result = await _controller.UpdateModule(moduleId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mockMapper.Verify(x => x.Map<Module>(request), Times.Once);
            _mock.Verify(x => x.Modules.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task UpdateModule_ShouldReturnStatusCode500_WhenErrorOccursSavingToDatabase()
        {
            //Arange
            var request = _fixture.Create<UpdateModuleRequest>();
            var moduleId = _fixture.Create<Guid>();
            var response = _fixture.Create<Module>();

            _mockMapper.Setup(x => x.Map<Module>(request)).Returns(response);
            _mock.Setup(x => x.Modules.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ThrowsAsync(new Exception("Database error occurred"));


            //Act
            var result = await _controller.UpdateModule(moduleId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var statusCodeResult = (ObjectResult)result;
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("An error occurred while updating the module.");
            _mockMapper.Verify(x => x.Map<Module>(request), Times.Once);
            _mock.Verify(x => x.Modules.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }
    }
}
