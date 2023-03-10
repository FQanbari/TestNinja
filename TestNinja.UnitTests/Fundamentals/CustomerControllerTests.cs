using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private CustomerController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new CustomerController();
        }

        [Test]
        public void GetCustomer_IdIsGreaterThanZero_ReturnCustomer ()
        {
            var result = _controller.GetCustomer(1);

            // Ok
            Assert.That(result, Is.TypeOf<Ok>());
            // Ok or one of its derivatives
            Assert.That(result, Is.TypeOf<Ok>());
        }
        [Test]
        public void GetCustomer_IdIsZero_ReturnCustomer()
        {
            var result = _controller.GetCustomer(0);

            // NotFound
            Assert.That(result, Is.TypeOf<NotFound>());
            // NotFound or one of its derivatives
            Assert.That(result, Is.TypeOf<NotFound>());
        }
        [Test]
        public void GetCustomer_IdIsLessThanZero_ReturnCustomer()
        {
            var result = _controller.GetCustomer(-1);

            // Ok
            Assert.That(result, Is.TypeOf<Ok>());
            // Ok or one of its derivatives
            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
