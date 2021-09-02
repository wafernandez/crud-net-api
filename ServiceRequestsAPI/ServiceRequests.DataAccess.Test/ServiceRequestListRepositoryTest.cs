using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceRequests.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceRequests.DataAccess.Test
{
    [TestClass]
    public class ServiceRequestListRepositoryTest
    {
        private ServiceRequestListRepository _testRepository;
        private Guid testGuidOne;
        private Guid testGuidTwo;

        [TestInitialize]
        public void SetUp()
        {
            _testRepository = new ServiceRequestListRepository();
            _testRepository.Clear();
            testGuidOne = new Guid("f73cbe20-3af3-449e-8254-ddab03baf299");
            testGuidTwo = new Guid("4240d975-5573-43a6-98c6-b8ef01d98f90");
        }

        [TestMethod]
        public void RepositoryIsEmpty()
        {
            Assert.IsTrue(_testRepository.IsEmpty());
        }

        [TestMethod]
        public void RepositoryIsNotEmpty()
        {
            ServiceRequest newService = _testRepository.Add(new ServiceRequest());

            Assert.IsFalse(_testRepository.IsEmpty());
        }

        [TestMethod]
        public void ExistsServiceRequest()
        {
            ServiceRequest newService =  _testRepository.Add(new ServiceRequest()
            {
                CreatedBy = "User One"
            });

            Assert.IsNotNull(_testRepository.Get(newService.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceRequestNotFoundException))]
        public void NotExistsServiceRequest()
        {
            Assert.IsNotNull(_testRepository.Get(testGuidOne));
        }

        [TestMethod]
        public void AddServiceRequest()
        {
            ServiceRequest newServiceRequest = new ServiceRequest()
            {
                CreatedBy = "User One"
            };
             ServiceRequest addedServiceRequest = _testRepository.Add(newServiceRequest);

            Assert.AreEqual(newServiceRequest.CreatedBy, _testRepository.Get(addedServiceRequest.Id).CreatedBy);
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceRequestNotFoundException))]
        public void DeleteServiceRequest()
        {
            ServiceRequest added = _testRepository.Add(new ServiceRequest());
            _testRepository.Delete(added.Id);
            
            Assert.IsNull(_testRepository.Get(added.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceRequestNotFoundException))]
        public void DeleteNotExistentServiceRequest()
        {
            _testRepository.Delete(testGuidOne);
            
            Assert.IsNull(_testRepository.Get(testGuidOne));
        }

        [TestMethod]
        public void GetAllServiceRequests()
        {
            _testRepository.Clear();

            _testRepository.Add(new ServiceRequest());
            _testRepository.Add(new ServiceRequest());
            _testRepository.Add(new ServiceRequest());

            IEnumerable<ServiceRequest> serviceRequests = _testRepository.GetAll();
            
            Assert.AreEqual(3, serviceRequests.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceRequestNotFoundException))]
        public void UpdateNotExistentServiceRequest()
        {
            ServiceRequest toUpdate = new ServiceRequest()
            {
                Id = testGuidTwo,
                CreatedBy = "Another User"
            };
            ServiceRequest updated = _testRepository.Update(toUpdate);

            Assert.AreEqual(toUpdate.CreatedBy, updated.CreatedBy);
        }

        [TestMethod]
        public void UpdateServiceRequest()
        {
            ServiceRequest newServiceRequest = new ServiceRequest()
            {
                BuildingCode = "Code 001",
                Description = "Description",
                CurrentStatus = "Created",
                CreatedBy = "User 001",
                LastModifiedBy = "User 001"
            };
            ServiceRequest added = _testRepository.Add(newServiceRequest);

            ServiceRequest updatedServiceRequest = new ServiceRequest()
            {
                Id = added.Id,
                BuildingCode = "Code 002",
                Description = "Description Updated",
                CurrentStatus = "InProgress",
                CreatedBy = "User 002",
                LastModifiedBy = "User 002",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };
            ServiceRequest updated = _testRepository.Update(updatedServiceRequest);

            ServiceRequest current = _testRepository.Get(added.Id);

            Assert.AreEqual(updated.Id, current.Id);
            Assert.AreEqual(updated.BuildingCode, current.BuildingCode);
            Assert.AreEqual(updated.Description, current.Description);
            Assert.AreEqual(updated.CurrentStatus, current.CurrentStatus);
            Assert.AreEqual(updated.CreatedBy, current.CreatedBy);
            Assert.AreEqual(updated.LastModifiedBy, current.LastModifiedBy);
            Assert.AreEqual(updated.CreatedDate, current.CreatedDate);
            Assert.AreEqual(updated.LastModifiedDate, current.LastModifiedDate);
        }
    }
}
