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
    public class ReservationTests
    {
        [Test]
        public void CanBeCancelledBy_UserIsAdmin_ReturnTrue()
        {
            var reservation = new Reservation();
            var user = new User { IsAdmin = true};

            var result = reservation.CanBeCancelledBy(user);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CanBeCancelledBy_CancellByUserMadeBy_ReturnTrue()
        {
            var reservation = new Reservation();
            var user = new User();
            reservation.MadeBy= user;

            var result = reservation.CanBeCancelledBy(user);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CanBeCancelledBy_OtherUser_ReturnFalse()
        {
            var reservation = new Reservation();
            var user = new User();

            var result = reservation.CanBeCancelledBy(user);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void CanBeCancelledBy_WhoMadeAndAdmin_ReturnTrue()
        {
            var reservation = new Reservation();
            var user = new User { IsAdmin = true};
            reservation.MadeBy = user;

            var result = reservation.CanBeCancelledBy(user);

            Assert.That(result, Is.EqualTo(true));
        }
    }
}
