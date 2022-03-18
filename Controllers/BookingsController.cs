using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelBookingSystem.Models;
using HotelBookingSystem.Data;
using HotelBookingSystem.Services;
namespace HotelBookingSystem.Controllers
{
    public class BookingsController : Controller
    {
        private readonly BookingService bookingService;
        
        public BookingsController(BookingService bookingService){
            // Create database context
            
            this.bookingService = bookingService;
           
        }

        // GET: Bookings
        public IActionResult Index(int id)
        {

            
            ViewBag.FullyOccupiedDates = bookingService.FullyOccupiedDates();

           

            ViewBag.YearToDisplay = bookingService.YearToDisplay(id);
            return View(bookingService.Get());
            
        }

        // GET: Bookings/Details/5
        public IActionResult Details(int id)
        {


            var booking = bookingService.GetById(id);

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            var externalclass = new Class(this);
            externalclass.ViewData();
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StartDate,EndDate,CustomerId")] Booking booking)
        {
            var externalclass = new Class(this);
            if (ModelState.IsValid)
            {
                int roomId = -1;
                DateTime startDate = booking.StartDate;
                DateTime endDate = booking.EndDate;

                if (startDate <= DateTime.Today || startDate > endDate)
                {
                    externalclass.ViewDataCustomer(booking);
                    ViewBag.Status = "The start date cannot be in the past or later than the end date.";
                    return View(booking);
                }
                roomId = bookingService.RoomId(booking);
                if (roomId >= 0)
                {
                    booking.RoomId = roomId;
                    booking.IsActive = true;
                    bookingService.Create(booking);
                    return RedirectToAction(nameof(Index));
                }
            }

            externalclass.ViewDataCustomer(booking);
            ViewBag.Status = "The booking could not be created. There were no available room.";
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var externalclass = new Class(this);
            var booking = bookingService.GetById(id);
            externalclass.ViewDataCustomer(booking);
            externalclass.ViewDataRoom(booking);

            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("StartDate,EndDate,IsActive,CustomerId,RoomId")] Booking booking)
        {
            var externalclass = new Class(this);

            if (ModelState.IsValid)
            {
                try
                {
                    bookingService.Edit(id, booking);
                    bookingService.Delete(id);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            externalclass.ViewDataCustomer(booking);
            externalclass.ViewDataRoom(booking);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public IActionResult Delete(int id)
        {


            var booking = bookingService.GetById(id);
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var booking = bookingService.GetById(id);
            bookingService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return bookingService.BookingExist(id);
        }
    }
}