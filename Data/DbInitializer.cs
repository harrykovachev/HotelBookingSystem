﻿using HotelBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HotelBookingContext context)
        {
            // Delete the database, if it already exists. I do this because an
            // existing database may not be compatible with the entity model,
            // if the entity model was changed since the database was created.
            context.Database.EnsureDeleted();

            // Create the database, if it does not already exists. This operation
            // is necessary, if you use an SQL Server database.
            context.Database.EnsureCreated();

            // Look for any bookings.
            if (context.Booking.Any())
            {
                return;   // DB has been seeded
            }

            List<Customer> customers = new List<Customer>
            {
                new Customer { Name="Pesho", Email="pesho@gmail.com" },
                new Customer { Name="Gosho", Email="gosho@gmail.com" }
            };

            List<Room> rooms = new List<Room>
            {
                new Room { Description="A" },
                new Room { Description="B" },
                new Room { Description="C" }
            };

            DateTime date = DateTime.Today.AddDays(4);
           /* List<Booking> bookings = new List<Booking>
            {
                new Booking { StartDate=new DateTime(2022,3,22), EndDate= new DateTime(2022,3,25), IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { StartDate=new DateTime(2022,3,22), EndDate=new DateTime(2022,3,25), IsActive=true, CustomerId=2, RoomId=2 },
                new Booking { StartDate=new DateTime(2022,3,22), EndDate=new DateTime(2022,3,25), IsActive=true, CustomerId=1, RoomId=3 }
            };*/
            List<Booking> bookings = new List<Booking>
            {
                new Booking { StartDate=date, EndDate=date.AddDays(14), IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { StartDate=date, EndDate=date.AddDays(14), IsActive=true, CustomerId=2, RoomId=2 },
                new Booking { StartDate=date, EndDate=date.AddDays(14), IsActive=true, CustomerId=1, RoomId=3 }
            };

            context.Customer.AddRange(customers);
            context.Room.AddRange(rooms);
            context.SaveChanges();
            context.Booking.AddRange(bookings);
            context.SaveChanges();
        }
    }
}
