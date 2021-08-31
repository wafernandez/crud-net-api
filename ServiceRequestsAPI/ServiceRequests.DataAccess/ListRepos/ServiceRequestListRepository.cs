using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceRequests.BusinessLogic;

namespace ServiceRequests.DataAccess
{
    public class ServiceRequestListRepository : IRequestServiceRepository
    {
        List<ServiceRequest> _repository;

        public ServiceRequestListRepository()
        {
            _repository = new List<ServiceRequest>();
            _repository.Add(new ServiceRequest()
            {
                Id = new Guid("233d29d6-1859-4ded-90f0-addd5bc4f8b4"),
                BuildingCode = "Building Code 001",
                Description = "Description 001",
                CurrentStatus = "InProgress",
                CreatedBy = "User 001",
                LastModifiedBy = "User 001"
            });
            _repository.Add(new ServiceRequest()
            {
                Id = new Guid("bc0d193d-2871-4688-95f0-81c419703900"),
                BuildingCode = "Building Code 002",
                Description = "Description 002",
                CurrentStatus = "Created",
                CreatedBy = "User 002",
                LastModifiedBy = "User 002"

            });
            _repository.Add(new ServiceRequest()
            {
                Id = new Guid("15e6e9ad-4cb3-4acb-8e64-4a1143933ece"),
                BuildingCode = "Building Code 003",
                Description = "Description 003",
                CurrentStatus = "NotApplicable",
                CreatedBy = "User 003",
                LastModifiedBy = "User 003"
            });
            _repository.Add(new ServiceRequest()
            {
                Id = new Guid("d96741f7-5bdd-42d7-b4d2-0ee1aba658ff"),
                BuildingCode = "Building Code 004",
                Description = "Description 004",
                CurrentStatus = "InProgress",
                CreatedBy = "User 004",
                LastModifiedBy = "User 004"
            });
        }

        public ServiceRequest Add(ServiceRequest serviceRequest)
        {
            Guid newId = Guid.NewGuid();
            serviceRequest.Id = newId;
            try
            {
                _repository.Add(serviceRequest);
            }
            catch (Exception)
            {
                throw;
            }
            
            return Get(newId);
        }

        public bool IsEmpty()
        {
            return !_repository.Any();
        }

        public ServiceRequest Get(Guid id)
        {
            ServiceRequest result = _repository.FirstOrDefault(s => s.Id == id);
            if (result == null)
            {
                throw new ServiceRequestNotFoundException($"Service Request {id} was not found.");
            }
            
            return result;
        }

        public IEnumerable<ServiceRequest> GetAll()
        {
            return _repository.ToList();
        }

        public void Delete(Guid id)
        {
            ServiceRequest toDelete = _repository.FirstOrDefault(s => s.Id == id);
            if (toDelete == null)
            {
                throw new ServiceRequestNotFoundException($"Service Request {id} was not found.");
            }

            _repository.Remove(toDelete);
        }

        public ServiceRequest Update(ServiceRequest serviceRequest)
        {
            ServiceRequest toUpdate = Get(serviceRequest.Id);
            if (toUpdate == null)
            {
                throw new ServiceRequestNotFoundException($"Service Request {serviceRequest.Id} was not found.");
            }

            toUpdate.BuildingCode = serviceRequest.BuildingCode;
            toUpdate.Description = serviceRequest.Description;
            toUpdate.CurrentStatus = serviceRequest.CurrentStatus;
            toUpdate.CreatedBy = serviceRequest.CreatedBy;
            toUpdate.CreatedDate = serviceRequest.CreatedDate;
            toUpdate.LastModifiedBy = serviceRequest.LastModifiedBy;
            toUpdate.LastModifiedDate = serviceRequest.LastModifiedDate;

            return Get(serviceRequest.Id);
        }
    }
}
