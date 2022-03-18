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
    public class CustomerTest
    {
        


        [TestMethod]
        
        public void TestCustomer_GetById()
        {
            //Arrange
            CustomerService service = new CustomerService();
            service.DeleteAll();
            Customer customer = new Customer {Id=99, Name = "Sasho", Email = "sasho@gmail.com" };
            //Act
             service.Create(customer);
            //Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(customer, service.GetById(99));
        }
        [TestMethod]
        public void TestCustomer_GetAll()
        {
            //Arrange

            CustomerService service = new CustomerService();
            service.DeleteAll();
            Customer customer1 = new Customer { Name = "Gosho", Email = "gosho@gmail.com" };
            Customer customer2 = new Customer { Name = "Sasho", Email = "sasho@gmail.com" };

            //Act
            service.Create(customer1);
            service.Create(customer2);
            //Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, service.GetAll().Count());
        }
        [TestMethod]
        public void TestCustomer_Create()
        {
            //Arrange
            CustomerService service = new CustomerService();
            service.DeleteAll();
            Customer customer1 = new Customer { Name = "Gosho", Email = "gosho@gmail.com" };

            //Act
            service.Create(customer1);
            //Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, service.GetAll().Count());
        }
        [TestMethod]
        public void TestCustomer_Edit()
        {
            //Arrange
            CustomerService service = new CustomerService();
            service.DeleteAll();
            Customer customer3 = new Customer {Id = 1, Name = "Kaloyan", Email = "Kaloyan@gmail.com" };
         //   Customer customer4 = new Customer {Id = 1, Name = "Ivan", Email = "Ivan@gmail.com" };
            //Act
            service.Create(customer3);
            customer3.Name = "Ivan";
            service.Edit( customer3);
            //Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Ivan", service.GetById(1).Name);
        }
        [TestMethod]
        public void TestCustomer_Delete()
        {
            //Arrange
            CustomerService service = new CustomerService();
            service.DeleteAll();
            Customer customer3 = new Customer {Id=1, Name = "Kaloyan", Email = "Kaloyan@gmail.com" };
            //Act
            service.Create(customer3);
            service.Delete(1);
            //Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(false, service.CustomerExist(1));
        }
    }
}
 