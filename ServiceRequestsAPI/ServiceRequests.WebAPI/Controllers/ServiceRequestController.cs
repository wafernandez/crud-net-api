using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceRequests.BusinessLogic;
using ServiceRequests.DataAccess;
using ServiceRequests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceRequests.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceRequestController : ControllerBase
    {
        private IRequestServiceRepository _requestServiceRepository;
        private INotifier _emailNotifier;

        public ServiceRequestController(IRequestServiceRepository repository, INotifier notifier)
        {
            _requestServiceRepository = repository;
            _emailNotifier = notifier;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                if (_requestServiceRepository.IsEmpty())
                {
                    return StatusCode(204, "Service Requests list is empty");
                }

                return StatusCode(200, _requestServiceRepository.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                return StatusCode(200, _requestServiceRepository.Get(new Guid(id)));
            }
            catch (ServiceRequestNotFoundException notFoundEx)
            {
                return StatusCode(404, notFoundEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(ServiceRequest newServiceRequestData)
        {
            try
            {
                return StatusCode(201, _requestServiceRepository.Add(newServiceRequestData));
            }
            catch (InvalidServiceRequestDataException invalidDataEx)
            {
                return StatusCode(400, invalidDataEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, ServiceRequest serviceRequestData)
        {
            try
            {
                serviceRequestData.Id = new Guid(id);
                ServiceRequest updated = _requestServiceRepository.Update(serviceRequestData);

                if (updated.CurrentStatus == Enums.CurrentStatus.Complete.ToString() ||
                    updated.CurrentStatus == Enums.CurrentStatus.Canceled.ToString())
                {
                    _emailNotifier.SendNotification(
                        $"Service Request {updated.Id} was closed by {updated.LastModifiedBy}. \n" +
                        $"Final status is {updated.CurrentStatus}");
                }

                return StatusCode(200, updated);
            }
            catch (InvalidServiceRequestDataException invalidDataEx)
            {
                return StatusCode(400, invalidDataEx.Message);
            }
            catch (ServiceRequestNotFoundException notFoundEx)
            {
                return StatusCode(404, notFoundEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _requestServiceRepository.Delete(new Guid(id));
                
                return StatusCode(201);
            }
            catch (ServiceRequestNotFoundException notFoundEx)
            {
                return StatusCode(404, notFoundEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
