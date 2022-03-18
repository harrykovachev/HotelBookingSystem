using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingSystem.Services
{
    public class Class
    {
        Controller _callingController = null;
        private readonly HotelBookingContext _context;
        public Class(Controller callingController)
        {
            this._callingController = callingController;
            var options = new DbContextOptionsBuilder<HotelBookingContext>()
                .UseInMemoryDatabase("HotelBookingDb")
                .Options;
            _context = new HotelBookingContext(options);
        }
        public void ViewData()
        {
            _callingController.ViewData["CustomerId"] = new SelectList(_context.Set<Customer>(), "Id", "Id");
        }
        public void ViewDataCustomer(Booking booking)
        {
            _callingController.ViewData["CustomerId"] = new SelectList(_context.Set<Customer>(), "Id", "Id", booking.CustomerId);
        }
        public void ViewDataRoom(Booking booking)
        {
              _callingController.ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "Id", "Id", booking.RoomId);
        }
    }
}
