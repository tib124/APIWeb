using APIWeb.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace APIWeb.Tests
{
    public class EmployeeRepositoryTests
    {
        [Fact]
        public async Task AddEmployee_Should_Add_New_Employee()
        {
            // Arrange
            var mockDbContext = new Mock<MyDbContext>();
            var employeeRepository = new EmployeeRpository(mockDbContext.Object);
            var newEmployee = new Employee { IdEmployee = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "123456789" };

            // Act
            var addedEmployee = await employeeRepository.AddEmployee(newEmployee);

            // Assert
            mockDbContext.Verify(db => db.Employees.AddAsync(newEmployee, default), Times.Once);
            mockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once);
            Assert.NotNull(addedEmployee);
            Assert.AreEqual(newEmployee, addedEmployee);
        }

        [Fact]
        public async Task DeleteEmployee_Should_Delete_Existing_Employee()
        {
            // Arrange
            var existingEmployeeId = Guid.NewGuid();
            var existingEmployee = new Employee { IdEmployee = existingEmployeeId, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com", Phone = "987654321" };
            var mockDbSet = new Mock<DbSet<Employee>>();
            mockDbSet.Setup(x => x.FindAsync(existingEmployeeId)).ReturnsAsync(existingEmployee);

            var mockDbContext = new Mock<MyDbContext>();
            mockDbContext.Setup(db => db.Employees).Returns(mockDbSet.Object);

            var employeeRepository = new EmployeeRpository(mockDbContext.Object);

            // Act
            await employeeRepository.DeleteEmployee(existingEmployeeId);

            // Assert
            mockDbContext.Verify(db => db.Employees.Remove(existingEmployee), Times.Once);
            mockDbContext.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        // Similarly, you can implement tests for other methods...
    }
}
