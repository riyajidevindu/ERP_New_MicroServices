using AutoFixture;
using AutoMapper;
using ERP.GraduateManagement.Api.Controllers;
using ERP.GraduateManagement.Core.DTOs.Requests;
using ERP.GraduateManagement.Core.DTOs.Responses;
using ERP.GraduateManagement.Core.Entities;
using ERP.GraduateManagement.DataServices.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ERP.GraduateManagement.Api.Tests.Controllers
{
    public class GraduatesControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _mock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GraduatesController _controller;

        public GraduatesControllerTests()
        {
            _fixture = new Fixture();
            _mock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _controller = new GraduatesController(_mock.Object,_mockMapper.Object);
        }



        //Get Graduate API test
        [Fact]
        public async Task GetAllGraduate_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var graduateMock = _fixture.Create<IEnumerable<Graduate>>();
            object value = _mock.Setup(x => x.GraduateRepo.All()).ReturnsAsync(graduateMock);

            var graduateListMock = _fixture.Create<IEnumerable<GetGraduateResponse>>();
            object listValue = _mockMapper.Setup(x => x.Map<IEnumerable<GetGraduateResponse>>(graduateMock)).Returns(graduateListMock);

            //Act
            var result = await _controller.GetAllGraduate().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            //result.Should().BeAssignableTo<IEnumerable<GetGraduateResponse>>();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(graduateListMock.GetType());
            _mock.Verify(x => x.GraduateRepo.All(),Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<GetGraduateResponse>>(graduateMock),Times.Once);

        }

        [Fact]
        public async Task GetAllGraduate_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            List<Graduate> response = null;
            object value = _mock.Setup(x => x.GraduateRepo.All()).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAllGraduate().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.GraduateRepo.All(), Times.Once);

        }


        //Get Graduate By Id API tests

        [Fact]
        public async Task GetGraduateById_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var graduateMock = _fixture.Create<Graduate>();
            var graduateId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.GraduateRepo.GetById(graduateId)).ReturnsAsync(graduateMock);

            var graduateListMock = _fixture.Create<GetGraduateByIdResponse>();
            object listValue = _mockMapper.Setup(x => x.Map<GetGraduateByIdResponse>(graduateMock)).Returns(graduateListMock);

            //Act
            var result = await _controller.GetGraduate(graduateId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(graduateListMock.GetType());
            _mock.Verify(x => x.GraduateRepo.GetById(graduateId), Times.Once);
            _mockMapper.Verify(x => x.Map<GetGraduateByIdResponse>(graduateMock), Times.Once);

        }

        [Fact]
        public async Task GetGraduateById_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            Graduate response = null;
            var graduateId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.GraduateRepo.GetById(graduateId)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetGraduate(graduateId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.GraduateRepo.GetById(graduateId), Times.Once);

        }

        //Add graduate API tests
        [Fact]
        public async Task AddGraduate_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateGraduateRequest>();
            var response = _fixture.Create<Graduate>();            

            _mockMapper.Setup(x => x.Map<Graduate>(request)).Returns(response);
            _mock.Setup(x => x.GraduateRepo.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);            

            //Act
            var result = await _controller.AddGraduate(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
            _mockMapper.Verify(x => x.Map<Graduate>(request),Times.Once);
            _mock.Verify(x => x.GraduateRepo.Add(response),Times.Once);
            _mock.Verify(x => x.CompleteAsync(),Times.Once);

        }

        [Fact]
        public async Task AddGraduate_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateGraduateRequest>();
            _controller.ModelState.AddModelError("FirstName", "The FirstName field is required.");
            var response = _fixture.Create<Graduate>();

            _mockMapper.Setup(x => x.Map<Graduate>(request)).Returns(response);
            _mock.Setup(x => x.GraduateRepo.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddGraduate(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _mockMapper.Verify(x => x.Map<Graduate>(request), Times.Never);
            _mock.Verify(x => x.GraduateRepo.Add(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Delete Graduate API test

        [Fact]
        public async Task DeleteGraduate_ShouldReturnNoContent_WhenGraduateDeleted()
        {
            //Arange

            var graduateId = _fixture.Create<Guid>();
            var response = _fixture.Create<Graduate>();

            _mock.Setup(x => x.GraduateRepo.GetById(graduateId)).ReturnsAsync(response);
            _mock.Setup(x => x.GraduateRepo.Delete(graduateId)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteGraduate(graduateId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mock.Verify(x => x.GraduateRepo.GetById(graduateId),Times.Once);
            _mock.Verify(x => x.GraduateRepo.Delete(graduateId), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteGraduate_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange

            var graduateId = _fixture.Create<Guid>();
            Graduate graduateResponce = null;

            _mock.Setup(x => x.GraduateRepo.GetById(graduateId)).ReturnsAsync(graduateResponce);

            //Act
            var result = await _controller.DeleteGraduate(graduateId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.GraduateRepo.GetById(graduateId), Times.Once);
            _mock.Verify(x => x.GraduateRepo.Delete(graduateId), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Update Graduate API tests

        [Fact]
        public async Task UpdateGraduate_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateGraduateRequest>();
            var graduateId = _fixture.Create<Guid>();
            var response = _fixture.Create<Graduate>();
            _controller.ModelState.AddModelError("FirstName", "The FirstName field is required.");
            

            //Act
            var result = await _controller.UpdateGraduate(graduateId,request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _mockMapper.Verify(x => x.Map<Graduate>(request), Times.Never);
            _mock.Verify(x => x.GraduateRepo.Update(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        [Fact]
        public async Task UpdateGraduate_ShouldReturnNoContentResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateGraduateRequest>();
            var graduateId = _fixture.Create<Guid>();
            var response = _fixture.Create<Graduate>();

            _mockMapper.Setup(x => x.Map<Graduate>(request)).Returns(response);
            _mock.Setup(x => x.GraduateRepo.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);


            //Act
            var result = await _controller.UpdateGraduate(graduateId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mockMapper.Verify(x => x.Map<Graduate>(request), Times.Once);
            _mock.Verify(x => x.GraduateRepo.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task UpdateGraduate_ShouldReturnStatusCode500_WhenErrorOccursSavingToDatabase()
        {
            //Arange
            var request = _fixture.Create<UpdateGraduateRequest>();
            var graduateId = _fixture.Create<Guid>();
            var response = _fixture.Create<Graduate>();

            _mockMapper.Setup(x => x.Map<Graduate>(request)).Returns(response);
            _mock.Setup(x => x.GraduateRepo.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ThrowsAsync(new Exception("Database error occurred"));


            //Act
            var result = await _controller.UpdateGraduate(graduateId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var statusCodeResult = (ObjectResult)result;
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("An error occurred while updating the graduate.");
            _mockMapper.Verify(x => x.Map<Graduate>(request), Times.Once);
            _mock.Verify(x => x.GraduateRepo.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }



    }
}