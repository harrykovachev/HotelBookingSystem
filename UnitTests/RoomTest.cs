using HotelBookingSystem.Models;
using HotelBookingSystem.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using HotelBookingSystem.Data;
using System;

namespace UnitTests
{
    [TestClass]
   public class RoomTest
    {

        [TestMethod]

        public void TestRoom_GetAll()
        {
            //Arrange
            RoomService service = new RoomService();
            service.DeleteAll();
            Room room1 = new Room { Id = 1, Description = "A" };
            Room room2 = new Room { Id = 2, Description = "B" };

            //Act
            service.Create(room1);
            service.Create(room2);
           
            //Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, service.GetAll().Count());
        }

        [TestMethod]

        public void TestRoom_GetById()
        {
            //Arrange
            RoomService service = new RoomService();
            service.DeleteAll();
            Room room = new Room { Id = 99, Description = "A" };
            //Act
            service.Create(room);
            //Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(room, service.GetById(99));
        }

        [TestMethod]

        public void TestRoom_Create()
        {

            RoomService service = new RoomService();
            service.DeleteAll();
            Room room = new Room { Id = 99, Description = "A" };
            service.Create(room);
            
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, service.GetAll().Count());
        }

        [TestMethod]

        public void TestRoom_Delete()
        {
            RoomService service = new RoomService();
            service.DeleteAll();
            Room room = new Room { Id = 1, Description = "A" };
            service.Create(room);
            service.Delete(1);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(false, service.RoomExist(1));
        }

        [TestMethod]
        public void TestRoom_Edit()
        {
            RoomService service = new RoomService();
            service.DeleteAll();
            Room room = new Room { Id = 2, Description = "A" };
            service.Create(room);
            room.Description = "B";
            service.Edit( room);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("B", service.GetById(2).Description);
        }
    }
}
