using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {
        public static string OverlappingBookingsExist(Booking booking, IBookingRepository repository)
        {
            if (booking.Status == BookingStatus.Cancelled)
                return string.Empty;

            var bookings = repository.GetActiveBookings(booking.Id);

            var overlappingBooking =
                bookings.FirstOrDefault(
                    b =>
                        booking.ArrivalDate < b.DepartureDate
                        && booking.DepartureDate > b.ArrivalDate);

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }
    public class BooingRepository : IBookingRepository
    {
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            var bookings =
               unitOfWork.Query<Booking>()
                   .Where(
                       b => b.Status != BookingStatus.Cancelled);

            if(excludedBookingId.HasValue)
                bookings = bookings.Where(b => b.Id == excludedBookingId.Value);

            return bookings;
        }
    }

    public interface IUnitOfwork
    {
        IQueryable<T> Query<T>();
    }
    public class UnitOfWork: IUnitOfwork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public BookingStatus Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
    public enum BookingStatus
    {
        Reserved,
        Cancelled
    }
}