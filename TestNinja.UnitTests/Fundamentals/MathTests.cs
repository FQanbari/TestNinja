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
        private TestNinja.Fundamentals.Math _math;

        [SetUp]
        public void SetUp()
        {
            _math = new TestNinja.Fundamentals.Math();
        }

        [Test]
        [TestCase(1,2,3)]
        [TestCase(1,1,2)]
        public void Add_WhenCalled_ReturnSumArgument(int a, int b, int exceptionResult)
        {

            var result = _math.Add(a, b);

            Assert.That(result, Is.EqualTo(exceptionResult));
        }

        [Test]
        [TestCase(1,1,1)]
        [TestCase(1,2,2)]
        public void Max_WhenCalled_ReturnMaxArgument(int a, int b, int exceptionResult)
        {
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(exceptionResult));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOnlyOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);

            Assert.That(result.Count, Is.EqualTo(3));            
            Assert.That(result, Has.Member(1));
            Assert.That(result, Has.Member(3));
            Assert.That(result, Has.Member(5));
            Assert.That(result, Is.EquivalentTo(new[] {1,3,5} ));
        }
        [Test]
        public void GetOddNumbers_LimitIsEqualZero_ReturnOnlyOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(0);

            Assert.That(result.Count, Is.EqualTo(0));
        }
        [Test]
        public void GetOddNumbers_LimitIsLessThanZero_ReturnOnlyOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(-5);

            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}
