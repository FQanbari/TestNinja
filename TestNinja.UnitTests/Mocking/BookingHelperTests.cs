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
    public class BookingHelperTests
    {
        private Mock<IBookingRepository> _repository;
        private Booking _existingBooking;
        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IBookingRepository>();
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2023, 3, 27),
                DepartureDate = DepartOn(2023, 3, 29),
                Reference = "a"
            };
            _repository.Setup(s => s.GetActiveBookings(1)).Returns(new List<Booking>
            {
               _existingBooking
            }.AsQueryable());
        }
       
        [Test]
        public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesInTheMiddleOfExistingBooking_ReturnExistingBookingsReference()
        {
            var booking = new Booking
            {
                Id = 1,
                Status = BookingStatus.Reserved,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate),
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }
        [Test]
        public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookingsReference()
        {
            var booking = new Booking
            {
                Id = 1,
                Status = BookingStatus.Reserved,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }
        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingsReference()
        {
            var booking = new Booking
            {
                Id = 1,
                Status = BookingStatus.Reserved,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate),
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }
        [Test]
        public void OverlappingBookingsExist_BookingStartsInTheMiddleAndFinishesAfterAnExistingBooking_ReturnExistingBookingsReference()
        {
            var booking = new Booking
            {
                Id = 1,
                Status = BookingStatus.Reserved,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }
        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
        {
            var booking = new Booking
            {
                Id = 1,
                Status = BookingStatus.Reserved,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate,2),
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.That(result, Is.Empty);
        }
        [Test]
        public void OverlappingBookingsExist_BookingOverlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            var booking = new Booking
            {
                Id = 1,
                Status = BookingStatus.Cancelled,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),

            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.That(result, Is.Empty);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }
        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
        private DateTime Before(DateTime dateTime, int days = 1) 
        { 
            return dateTime.AddDays(-days);
        }
        private DateTime After(DateTime dateTime, int days =1)
        {
            return dateTime.AddDays(days);
        }

    }
}
