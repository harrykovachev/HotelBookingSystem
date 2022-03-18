using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace HotelBookingSystem.Services
{
    public class BookingService 
    {
        private readonly HotelBookingContext _context;
        //ControllerBase _callingController = null;
        public BookingService()
        {
           // this._callingController = callingController;
            var options = new DbContextOptionsBuilder<HotelBookingContext>()
                .UseInMemoryDatabase("HotelBookingDb")
                .Options;
                _context = new HotelBookingContext(options);
        }
        public List<Booking> GetAll()
        {
            return _context.Booking.ToList();
        }
        public Booking GetById(int id)
        {
            return _context.Booking.Include(b => b.Customer)
                    .Include(b => b.Room)
                    .SingleOrDefault(m => m.Id == id);
        }
        public List<Booking> Get()
        {
            var bookings = _context.Booking.Include(b => b.Customer).Include(b => b.Room);
            return bookings.ToList();
        }
        //  public object Room() => _context.Room();
        public List<DateTime> FullyOccupiedDates()
        {
            var bookings = _context.Booking.Include(b => b.Customer).Include(b => b.Room);

            var bookingStartDates = bookings.Select(b => b.StartDate);
            DateTime minBookingDate = bookingStartDates.Any() ? bookingStartDates.Min() : DateTime.MinValue;

            var bookingEndDates = bookings.Select(b => b.EndDate);
            DateTime maxBookingDate = bookingEndDates.Any() ? bookingEndDates.Max() : DateTime.MaxValue;
            int noOfRooms = _context.Room.Count();

            List<DateTime> fullyOccupiedDates = new List<DateTime>();
            
            if (_context.Booking.Any())
            {
                for (DateTime d = minBookingDate; d <= maxBookingDate; d = d.AddDays(1))
                {
                    var noOfBookings = from b in _context.Booking
                                       where b.IsActive && d >= b.StartDate && d <= b.EndDate
                                       select b;
                    if (noOfBookings.Count() >= noOfRooms)
                        fullyOccupiedDates.Add(d);
                    
                   
                }
            }
            return fullyOccupiedDates;
        }
        public int YearToDisplay(int id)
        {
            var bookings = _context.Booking.Include(b => b.Customer).Include(b => b.Room);
            var bookingStartDates = bookings.Select(b => b.StartDate);
            DateTime minBookingDate = bookingStartDates.Any() ? bookingStartDates.Min() : DateTime.MinValue;

            var bookingEndDates = bookings.Select(b => b.EndDate);
            DateTime maxBookingDate = bookingEndDates.Any() ? bookingEndDates.Max() : DateTime.MaxValue;
            int minBookingYear = minBookingDate.Year;
            int maxBookingYear = maxBookingDate.Year;
            if (id == null)
                id = DateTime.Today.Year;
            else if (id < minBookingYear)
                id = minBookingYear;
            else if (id > maxBookingYear)
                id = maxBookingYear;
            return id;
        }

        /*public IQueryable ActiveBooking()
        {

            return _context.Booking.Where(b => b.IsActive);
        }*/
        
        public void Create([Bind("StartDate,EndDate,CustomerId")] Booking booking)
        {
            _context.Booking.Add(booking);
            _context.SaveChanges();
        }
        public void Edit(int id, [Bind("StartDate,EndDate,IsActive,CustomerId,RoomId")] Booking booking)
        {
            _context.Update(booking);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Booking dbbooking = GetById(id);
            _context.Booking.Remove(dbbooking);
            _context.SaveChanges();
        }
        public void DeleteAll()
        {
            _context.Booking.RemoveRange(_context.Booking.ToList());
            _context.SaveChanges();
        }
        public bool BookingExist(int id)
        {
            return _context.Booking.Any(e => e.Id == id);
        }
        public int RoomId(Booking booking)
        {
            DateTime startDate = booking.StartDate;
            DateTime endDate = booking.EndDate;
            var activeBookings = _context.Booking.Where(b => b.IsActive);
            foreach (var room in _context.Room)
            {
                var activeBookingsForCurrentRoom = activeBookings.Where(b => b.RoomId == room.Id);
                if (activeBookingsForCurrentRoom.All(b => startDate < b.StartDate &&
                    endDate < b.StartDate || startDate > b.EndDate && endDate > b.EndDate))
                {
                    return room.Id;
                    break;
                }
            }
            return -1;
        }
        
    }
}
