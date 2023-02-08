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
    public class MathTests
    {
        [Test]
        [TestCase(1,2,3)]
        [TestCase(1,1,2)]
        public void Add_WhenCalled_ReturnSumArgument(int a, int b, int exceptionResult)
        {
            var math = new TestNinja.Fundamentals.Math();

            var result = math.Add(a, b);

            Assert.That(result, Is.EqualTo(exceptionResult));
        }
    }
}
