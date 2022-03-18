using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using HotelBookingSystem.Data;
using System;

//using Moq;
namespace UnitTests
{
    [TestClass]
    public class BookingTest
    {
        [TestMethod]
        public void BookingTest_GetById()
        {
            BookingService bookingService = new BookingService();
            RoomService roomService = new RoomService();
            CustomerService customerService = new CustomerService();
            bookingService.DeleteAll();
            roomService.DeleteAll();
            customerService.DeleteAll();
            DateTime date = DateTime.Today.AddDays(4);
            Customer customer = new Customer { Id = 1, Name = "Peshi", Email = "abv.bg@abv.bg" };
            Room room = new Room { Id = 1, Description = "A" };
            Booking booking1 = new Booking { Id = 55, StartDate = date, EndDate = date.AddDays(14), CustomerId = 1, RoomId = 1 };
            customerService.Create(customer);
            roomService.Create(room);
            bookingService.Create(booking1);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(date, bookingService.GetById(55).StartDate);

        }
        [TestMethod]
        public void BookingTest_FulluOccupied()
        {
            BookingService bookingService = new BookingService();
            RoomService roomService = new RoomService();
            CustomerService customerService = new CustomerService();
            bookingService.DeleteAll();
            roomService.DeleteAll();
            customerService.DeleteAll();

            DateTime date = DateTime.Today.AddDays(4);
            Booking booking1 = new Booking { StartDate = date, EndDate = date.AddDays(1), IsActive = true, CustomerId = 1, RoomId = 1 };
            Booking booking2 = new Booking { StartDate = date, EndDate = date.AddDays(1), IsActive = true, CustomerId = 2, RoomId = 2 };
            Booking booking3 = new Booking { StartDate = date, EndDate = date.AddDays(1), IsActive = true, CustomerId = 1, RoomId = 3 };
            bookingService.Create(booking1);
            bookingService.Create(booking2);
            bookingService.Create(booking3);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, bookingService.FullyOccupiedDates().Count);
        }
        [TestMethod]
        public void BookingTest_YearToDisplay()
        {
            //Arrange
            BookingService service = new BookingService();
            service.DeleteAll();
            //Act
            DateTime date = DateTime.Today.AddDays(4);

            Booking booking1 = new Booking { StartDate = date, EndDate = date.AddDays(14), IsActive = true, CustomerId = 1, RoomId = 1 };
            Booking booking2 = new Booking { StartDate = date, EndDate = date.AddDays(14), IsActive = true, CustomerId = 2, RoomId = 2 };
            Booking booking3 = new Booking { StartDate = date, EndDate = date.AddDays(14), IsActive = true, CustomerId = 1, RoomId = 3 };
            service.Create(booking1);
            service.Create(booking2);
            service.Create(booking3);


            //Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2022, service.YearToDisplay(1));

        }
        [TestMethod]
        public void BookingTest_RoomId()
        {
            //Arrange
            BookingService bookingService = new BookingService();
            RoomService roomService = new RoomService();
            CustomerService customerService = new CustomerService();
            bookingService.DeleteAll();
            roomService.DeleteAll();
            customerService.DeleteAll();
            DateTime date = DateTime.Today.AddDays(4);
            //Act
            Booking booking1 = new Booking { StartDate = date, EndDate = date.AddDays(14), CustomerId = 1 };

            Room room = new Room { Id = 1, Description = "A" };

            bookingService.Create(booking1);
            roomService.Create(room);
            var roomId = bookingService.RoomId(booking1);

            //Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, bookingService.RoomId(booking1));

        }

        [TestMethod]
        public void TestBooking_Create()
        {
            BookingService service = new BookingService();
            service.DeleteAll();
            Booking booking = new Booking { Id = 3, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), IsActive = true, CustomerId = 2, RoomId = 2 };

            service.Create(booking);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, service.GetAll().Count());
        }
        [TestMethod]
        public void TestBooking_Edit()
        {
            BookingService bookingService = new BookingService();
            RoomService roomService = new RoomService();
            CustomerService customerService = new CustomerService();
            bookingService.DeleteAll();
            roomService.DeleteAll();
            customerService.DeleteAll();
            DateTime date = DateTime.Today.AddDays(4);
            Booking booking1 = new Booking { Id = 1, StartDate = date, EndDate = date.AddDays(14), CustomerId = 1, RoomId = 1 };
            Customer customer = new Customer { Id = 1, Name = "Peshi", Email = "abv.bg@abv.bg" };
            Room room = new Room { Id = 1, Description = "A" };
            customerService.Create(customer);
            roomService.Create(room);
            bookingService.Create(booking1);
            booking1.StartDate = date.AddDays(1);
            bookingService.Edit(booking1);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(date.AddDays(1), bookingService.GetById(1).StartDate);
        }
        [TestMethod]
        public void TestBooking_Delete()
        {
            BookingService bookingService = new BookingService();
            RoomService roomService = new RoomService();
            CustomerService customerService = new CustomerService();
            bookingService.DeleteAll();
            roomService.DeleteAll();
            customerService.DeleteAll();
            DateTime date = DateTime.Today.AddDays(4);
            Booking booking1 = new Booking { Id = 1, StartDate = date, EndDate = date.AddDays(14), CustomerId = 1, RoomId = 1 };
            Customer customer = new Customer { Id = 1, Name = "Peshi", Email = "abv.bg@abv.bg" };
            Room room = new Room { Id = 1, Description = "A" };
            customerService.Create(customer);
            roomService.Create(room);
            bookingService.Create(booking1);
            bookingService.Delete(1);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(false, bookingService.BookingExist(1));
        }
        [TestMethod]
        public void TestBooking_GetAll()
        {
            BookingService bookingService = new BookingService();
            RoomService roomService = new RoomService();
            CustomerService customerService = new CustomerService();
            bookingService.DeleteAll();
            roomService.DeleteAll();
            customerService.DeleteAll();
            DateTime date = DateTime.Today.AddDays(4);
            Booking booking1 = new Booking { Id = 1, StartDate = date, EndDate = date.AddDays(14), CustomerId = 1, RoomId = 1 };
            Customer customer = new Customer { Id = 1, Name = "Peshi", Email = "abv.bg@abv.bg" };
            Room room = new Room { Id = 1, Description = "A" };
            customerService.Create(customer);
            roomService.Create(room);
            bookingService.Create(booking1);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, bookingService.GetAll().Count);
        }
    }
}
 