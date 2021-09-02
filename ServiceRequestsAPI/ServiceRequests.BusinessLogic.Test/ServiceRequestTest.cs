using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ServiceRequests.BusinessLogic.Test
{
    [TestClass]
    public class ServiceRequestTest
    {
        [TestMethod]
        public void CreateServiceRequestTest()
        {
            Guid testGuid = new Guid("f73cbe20-3af3-449e-8254-ddab03baf299");
            DateTime currentDateTime = DateTime.Now;
            ServiceRequest newServiceRequest = new ServiceRequest()
            {
                Id = testGuid,
                BuildingCode = "Code 001",
                Description = "Description",
                CurrentStatus = "Created",
                CreatedBy = "User 001",
                LastModifiedBy = "User 002",
                CreatedDate = currentDateTime,
                LastModifiedDate = currentDateTime
            };

            Assert.AreEqual(newServiceRequest.Id, testGuid);
            Assert.AreEqual(newServiceRequest.BuildingCode, "Code 001");
            Assert.AreEqual(newServiceRequest.Description, "Description");
            Assert.AreEqual(newServiceRequest.CurrentStatus, "Created");
            Assert.AreEqual(newServiceRequest.CreatedBy, "User 001");
            Assert.AreEqual(newServiceRequest.LastModifiedBy, "User 002");
            Assert.AreEqual(newServiceRequest.CreatedDate, currentDateTime);
            Assert.AreEqual(newServiceRequest.LastModifiedDate, currentDateTime);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidServiceRequestDataException))]
        public void CreateServiceRequestInvalidStatusTest()
        {
            ServiceRequest newServiceRequest = new ServiceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingCode = "Code 001",
                Description = "Description",
                CurrentStatus = "StatusCreated",
                CreatedBy = "User 001",
                LastModifiedBy = "User 002",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };
        }
    }
}
