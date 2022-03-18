using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelBookingSystem.Models;
using HotelBookingSystem.Data;
using HotelBookingSystem.Services;
using System.Collections.Generic;

namespace HotelBookingSystem.Controllers
{
    public class RoomsController : Controller
    {
        private readonly RoomService roomService;

        public RoomsController(RoomService roomService){
            // Create database context
            this.roomService = roomService;
        }

        // GET: Rooms
        public IActionResult Index()
        {
            List<Room> room = roomService.GetAll();
            return View(room);
        }

        // GET: Rooms/Details/5
        public IActionResult Details(int id)
        {
            Room room = roomService.GetById(id);
            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Room room)
        {
            roomService.Create(room);
            return RedirectToAction(nameof(Index));
        }

        // GET: Rooms/Edit/5
        public IActionResult Edit(int id)
        {
            Room room = roomService.GetById(id);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("Id,Description")] Room room)
        {
            roomService.Edit(id, room);
            return RedirectToAction(nameof(Index));
            return View(room);
        }

        // GET: Rooms/Delete/5
        public IActionResult Delete(int id)
        {
            Room room = roomService.GetById(id);
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            roomService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return roomService.RoomExist(id);
        }
    }
}
