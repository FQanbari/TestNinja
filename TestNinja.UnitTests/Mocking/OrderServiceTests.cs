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
    public class OrderServiceTests
    {
        private Mock<IStorage> _storage;
        private OrderService _service;
        [SetUp]
        public void SetUp()
        {
            _storage = new Mock<IStorage>();
            _service = new OrderService(_storage.Object);
        }

        [Test] 
        public void PlaceOrder_WhenCalled_StoreTheOrder() 
        {
            var order = new Order();
            _service.PlaceOrder(order);

            _storage.Verify(s => s.Store(order));
        }
    }
}
