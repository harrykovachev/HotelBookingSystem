using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelBookingSystem.Models;
using HotelBookingSystem.Data;
using HotelBookingSystem.Services;

namespace HotelBookingSystem.Controllers
{
    public class CustomersController : Controller
    {
        
        private readonly CustomerService customerService;
        public CustomersController(CustomerService customerService){
            // Create database context
            this.customerService = customerService;
            
        }

        // GET: Customers
        public IActionResult Index()
        {
            
                List<Customer> customer = customerService.GetAll();
                  return View(customer);
            
        }

        // GET: Customers/Details/5
        public IActionResult Details(int id)
        {
            Customer customer = customerService.GetById(id);
            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Customer customer)
        {
            customerService.Create(customer);
            return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Edit/5
        public IActionResult Edit(int id)
        {
            Customer customer = customerService.GetById(id);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( [Bind("Id,Name,Email")] Customer customer)
        {
            customerService.Edit( customer);
            return RedirectToAction(nameof(Index));
            return View(customer);
        }

        // GET: Customers/Delete/5
        public IActionResult Delete(int id)
        {
            Customer customer = customerService.GetById(id);
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            customerService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return customerService.CustomerExist(id);
        }
    }
}
