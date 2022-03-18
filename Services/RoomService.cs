using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public class RoomService
    {
        private readonly HotelBookingContext _context;
        public RoomService()
        {
            
            var options = new DbContextOptionsBuilder<HotelBookingContext>()
               .UseInMemoryDatabase("HotelBookingDb")
               .Options;
            _context = new HotelBookingContext(options);
        }
        public List<Room> GetAll()
        {
            return _context.Room.ToList();
        }
        public Room GetById(int id)
        {
            return _context.Room.FirstOrDefault(m => m.Id == id);
        }
        public void Create([Bind("Id,Description")] Room room)
        {

            _context.Add(room);
            _context.SaveChanges();
        }
        public void Edit(int id, [Bind("Id,Description")] Room room)
        {
            _context.Update(room);
            _context.SaveChanges();

        }
        public void Delete(int id)
        {
            Room dbroom = GetById(id);
            _context.Room.Remove(dbroom);
            _context.SaveChanges();
        }
        public void DeleteAll()
        {
            _context.Room.RemoveRange(_context.Room.ToList());
            _context.SaveChanges();
        }
        public bool RoomExist(int id)
        {
            return _context.Room.Any(e => e.Id == id);
        }
    }
}
