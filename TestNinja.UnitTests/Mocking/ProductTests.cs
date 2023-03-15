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
    public class ProductTests
    {
        private Product _product;
        private Mock<ICustomer> _cutstomer;
        [SetUp] 
        public void SetUp() 
        {
            _cutstomer = new Mock<ICustomer>();
            _product = new Product();
        }

        [Test]
        public void GetPrice_GoldCustomer_Applying30PercentDiscount()
        {
            _product.ListPrice = 100;
            var result = _product.GetPrice(new Customer { IsGold= true });

            Assert.That(result, Is.EqualTo(70f));
        }

        [Test]
        public void GetPrice_GoldCustomer_Applying30PercentDiscount2()
        {
            _cutstomer.Setup(c => c.IsGold).Returns(true);

            _product.ListPrice = 100;
            var result = _product.GetPrice(_cutstomer.Object);

            Assert.That(result, Is.EqualTo(70f));
        }
    }
}
