using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWeb.Models;
using APIWeb;
using NUnit.Framework.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace EmployeeRepositoryTests
{
    public class EmployeeRepositoryTests
    {
            [Test]
            public async Task AddEmployeeSholdReturnEmployee()
            {
                var EmployeeToAdd = new Employee { IdEmployee = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", Email = "doe.gmail.com", Phone = "4444444" };
                var ExpectedEmployee = new Employee { IdEmployee = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", Email = "doe.gmail.com", Phone = "4444444" };
                var mockDbSet = new Mock<DbSet<Employee>>();
                mockDbSet.Setup(m => m.AddAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Employee e, CancellationToken _) =>
                {
                    return new Mock<EntityEntry<Employee>>().Object;
                });

            var mockDbContext = new Mock<MyDbContext>();
            mockDbContext.Setup(m => m.Employees).Returns(mockDbSet.Object);

            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(repo => repo.AddEmployee(It.IsAny<Employee>()))
                .ReturnsAsync((Employee e) => e);

            var employeeService = new EmployeeRepository(mockDbContext.Object);

                // Act
                var result = await employeeService.AddEmployee(EmployeeToAdd);

                // Assert
               
                NUnit.Framework.Assert.Equals(ExpectedEmployee.FirstName, result.FirstName);
                NUnit.Framework.Assert.Equals(ExpectedEmployee.LastName, result.LastName);
            }
        }
    }

