using APIWeb.Controllers;
using APIWeb.Models;
using APIWeb;
using Moq;
using Microsoft.AspNetCore.Mvc;
using NuGet.Frameworks;
using NUnit.Framework.Legacy;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAll_Returns_OkResult_With_Employees()
        {
            // Arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            var employees = new List<Employee>
        {
            new Employee { IdEmployee = Guid.NewGuid(), FirstName = "John", LastName = "Smith", Phone = "129301231"},
            new Employee { IdEmployee = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", Phone = "1231313231"}
        };
            mockEmployeeRepository.Setup(repo => repo.GetAll()).ReturnsAsync(employees);
            var controller = new EmployeeController(mockEmployeeRepository.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.InstanceOf<IEnumerable<Employee>>());
            var model = okResult.Value as IEnumerable<Employee>;
            Assert.That(employees.Count, Is.EqualTo(model.Count()));
        }
        [Test]
        public async Task GetById_Returns_OkResult_With_Employee()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employee = new Employee { IdEmployee = employeeId, FirstName = "John", LastName = "Smith", Phone = "129301231" };

            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(repo => repo.GetById(employeeId)).ReturnsAsync(employee);

            var controller = new EmployeeController(mockEmployeeRepository.Object);

            // Act
            var actionResult = await controller.GetById(employeeId);

            // Assert
            var okObjectResult = actionResult.Result as OkObjectResult;
            var returnedEmployee = okObjectResult.Value as Employee;

            ClassicAssert.NotNull(okObjectResult);
            ClassicAssert.NotNull(returnedEmployee);
            Assert.That(employeeId, Is.EqualTo(returnedEmployee.IdEmployee));
        }

        [Test]
        public async Task GetById_Returns_NotFoundIfNotFound()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            Employee employee = null!;

            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(repo => repo.GetById(employeeId)).ReturnsAsync(employee);

            var controller = new EmployeeController(mockEmployeeRepository.Object);

            // Act
            var actionResult = await controller.GetById(employeeId);

            ClassicAssert.IsInstanceOf<BadRequestResult>(actionResult.Result);
        }
    }
}