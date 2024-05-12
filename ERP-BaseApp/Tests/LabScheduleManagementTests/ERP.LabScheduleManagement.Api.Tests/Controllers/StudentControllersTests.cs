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
    public class StudentControllersTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _mock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StudentControllers _controller;

        public StudentControllersTests()
        {
            _fixture = new Fixture();
            _mock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _controller = new StudentControllers(_mock.Object, _mockMapper.Object);

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        //Get All Students API test
        [Fact]
        public async Task GetAllStudents_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var studentMock = _fixture.Create<IEnumerable<Student>>();
            object value = _mock.Setup(x => x.Students.All()).ReturnsAsync(studentMock);

            var studentListMock = _fixture.Create<IEnumerable<GetStudentResponse>>();
            object listValue = _mockMapper.Setup(x => x.Map<IEnumerable<GetStudentResponse>>(studentMock)).Returns(studentListMock);

            //Act
            var result = await _controller.GetAllStudents().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(studentListMock.GetType());
            _mock.Verify(x => x.Students.All(), Times.Once);
            _mockMapper.Verify(x => x.Map<IEnumerable<GetStudentResponse>>(studentMock), Times.Once);

        }

        [Fact]
        public async Task GetAllStudent_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            List<Student> response = null;
            object value = _mock.Setup(x => x.Students.All()).ReturnsAsync(response);

            //Act
            var result = await _controller.GetAllStudents().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Students.All(), Times.Once);

        }


        //Get Student By Id API tests

        [Fact]
        public async Task GetStudent_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arange
            var studentMock = _fixture.Create<Student>();
            var studentId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Students.GetById(studentId)).ReturnsAsync(studentMock);

            var studentDTOMock = _fixture.Create<GetStudentByIdResponse>();
            object mappedValue = _mockMapper.Setup(x => x.Map<GetStudentByIdResponse>(studentMock)).Returns(studentDTOMock);

            //Act
            var result = await _controller.GetStudent(studentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(studentDTOMock.GetType());
            _mock.Verify(x => x.Students.GetById(studentId), Times.Once);
            _mockMapper.Verify(x => x.Map<GetStudentByIdResponse>(studentMock), Times.Once);

        }

        [Fact]
        public async Task GetStudent_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange
            Student response = null;
            var studentId = _fixture.Create<Guid>();
            object value = _mock.Setup(x => x.Students.GetById(studentId)).ReturnsAsync(response);

            //Act
            var result = await _controller.GetStudent(studentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Students.GetById(studentId), Times.Once);

        }



        //Add Student API tests
        [Fact]
        public async Task AddStudent_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateStudentRequest>();
            var response = _fixture.Create<Student>();

            _mockMapper.Setup(x => x.Map<Student>(request)).Returns(response);
            _mock.Setup(x => x.Students.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddStudent(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
            _mockMapper.Verify(x => x.Map<Student>(request), Times.Once);
            _mock.Verify(x => x.Students.Add(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task AddStudent_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<CreateStudentRequest>();
            _controller.ModelState.AddModelError("StudentName", "The StudentName field is required.");
            var response = _fixture.Create<Student>();

            _mockMapper.Setup(x => x.Map<Student>(request)).Returns(response);
            _mock.Setup(x => x.Students.Add(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.AddStudent(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _mockMapper.Verify(x => x.Map<Student>(request), Times.Never);
            _mock.Verify(x => x.Students.Add(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Delete Student API test

        [Fact]
        public async Task DeleteStudent_ShouldReturnNoContent_WhenStudentDeleted()
        {
            //Arange

            var studentId = _fixture.Create<Guid>();
            var response = _fixture.Create<Student>();

            _mock.Setup(x => x.Students.GetById(studentId)).ReturnsAsync(response);
            _mock.Setup(x => x.Students.Delete(studentId)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteStudent(studentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mock.Verify(x => x.Students.GetById(studentId), Times.Once);
            _mock.Verify(x => x.Students.Delete(studentId), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteStudent_ShouldReturnNotFound_WhenNoDataFound()
        {
            //Arange

            var studentId = _fixture.Create<Guid>();
            Student studentResponce = null;

            _mock.Setup(x => x.Students.GetById(studentId)).ReturnsAsync(studentResponce);

            //Act
            var result = await _controller.DeleteStudent(studentId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _mock.Verify(x => x.Students.GetById(studentId), Times.Once);
            _mock.Verify(x => x.Students.Delete(studentId), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        //Update Student API tests

        [Fact]
        public async Task UpdateStudent_ShouldReturnBadResponse_WhenInvalidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateStudentRequest>();
            var studentId = _fixture.Create<Guid>();
            var response = _fixture.Create<Student>();
            _controller.ModelState.AddModelError("StudentName", "The StudentName field is required.");


            //Act
            var result = await _controller.UpdateStudent(studentId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            _mockMapper.Verify(x => x.Map<Student>(request), Times.Never);
            _mock.Verify(x => x.Students.Update(response), Times.Never);
            _mock.Verify(x => x.CompleteAsync(), Times.Never);

        }


        [Fact]
        public async Task UpdateStudent_ShouldReturnNoContentResponse_WhenValidRequest()
        {
            //Arange
            var request = _fixture.Create<UpdateStudentRequest>();
            var studentId = _fixture.Create<Guid>();
            var response = _fixture.Create<Student>();

            _mockMapper.Setup(x => x.Map<Student>(request)).Returns(response);
            _mock.Setup(x => x.Students.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ReturnsAsync(true);


            //Act
            var result = await _controller.UpdateStudent(studentId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
            _mockMapper.Verify(x => x.Map<Student>(request), Times.Once);
            _mock.Verify(x => x.Students.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }

        [Fact]
        public async Task UpdateStudent_ShouldReturnStatusCode500_WhenErrorOccursSavingToDatabase()
        {
            //Arange
            var request = _fixture.Create<UpdateStudentRequest>();
            var studentId = _fixture.Create<Guid>();
            var response = _fixture.Create<Student>();

            _mockMapper.Setup(x => x.Map<Student>(request)).Returns(response);
            _mock.Setup(x => x.Students.Update(response)).ReturnsAsync(true);
            _mock.Setup(x => x.CompleteAsync()).ThrowsAsync(new Exception("Database error occurred"));


            //Act
            var result = await _controller.UpdateStudent(studentId, request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ObjectResult>();
            var statusCodeResult = (ObjectResult)result;
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be("An error occurred while updating the student.");
            _mockMapper.Verify(x => x.Map<Student>(request), Times.Once);
            _mock.Verify(x => x.Students.Update(response), Times.Once);
            _mock.Verify(x => x.CompleteAsync(), Times.Once);

        }
    }
}
