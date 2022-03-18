using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelBookingSystem;
using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
namespace HotelBookingSystem.Services
{
    public class CustomerService
    {
        private readonly  HotelBookingContext _context;
        public CustomerService()
        {
            var options = new DbContextOptionsBuilder<HotelBookingContext>()
                .UseInMemoryDatabase("HotelBookingDb")
                .Options;
            _context = new HotelBookingContext(options);
           
        }
        public List<Customer> GetAll()
        {
            return _context.Customer.ToList();
        }
        public Customer GetById(int id)
        {
            Console.WriteLine("_context.Customer");
            return _context.Customer.FirstOrDefault(m => m.Id == id);
        }
       public void Create([Bind("Id,Name,Email")] Customer customer)
        {

                _context.Add(customer);
                _context.SaveChanges();
                
            
        }
        public void Edit( [Bind("Id,Name,Email")] Customer customer)
        {
            _context.Update(customer);
            
            _context.SaveChanges();

        }
        public void Delete(int id)
        {
            Customer dbcustomer = GetById(id);
            _context.Customer.Remove(dbcustomer);
            _context.SaveChanges();
        }
        public void DeleteAll()
        {
            _context.Customer.RemoveRange(_context.Customer.ToList());
            _context.SaveChanges();
        }
        public bool CustomerExist(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }

    }
}
