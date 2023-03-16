using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private EmployeeController _controller;
        private Mock<IEmployeeRepository> _repository;

        [SetUp] 
        public void SetUp() 
        {
            _repository = new Mock<IEmployeeRepository>();
            _controller = new EmployeeController(_repository.Object);
        }

        [Test] 
        public void ShouldReturnEmployee_WhenCalled_RemoveTheEmployeeFromDb()
        {
            _controller.DeleteEmployee(1);

            _repository.Verify(r => r.RemoveEmployee(1));
        }
    }
}
