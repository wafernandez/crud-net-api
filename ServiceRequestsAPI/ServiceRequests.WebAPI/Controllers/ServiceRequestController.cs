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
        public ActionResult<IEnumerable<ServiceRequest>> GetAll()
        {
            try
            {
                if (_requestServiceRepository.IsEmpty())
                {
                    return NoContent();
                }

                return Ok(_requestServiceRepository.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ServiceRequest> Get(string id)
        {
            try
            {
                return Ok(_requestServiceRepository.Get(new Guid(id)));
            }
            catch (ServiceRequestNotFoundException notFoundEx)
            {
                return NotFound(notFoundEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<ServiceRequest> Create(ServiceRequest newServiceRequestData)
        {
            try
            {
                ServiceRequest added = _requestServiceRepository.Add(newServiceRequestData);
                return Created($"~api/employees/{added.Id}", added);
            }
            catch (InvalidServiceRequestDataException invalidDataEx)
            {
                return BadRequest(invalidDataEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<ServiceRequest> Update(string id, ServiceRequest serviceRequestData)
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

                return Ok(updated);
            }
            catch (InvalidServiceRequestDataException invalidDataEx)
            {
                return BadRequest(invalidDataEx.Message);
            }
            catch (ServiceRequestNotFoundException notFoundEx)
            {
                return NotFound(notFoundEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                _requestServiceRepository.Delete(new Guid(id));
                
                return Accepted();
            }
            catch (ServiceRequestNotFoundException notFoundEx)
            {
                return NotFound(notFoundEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
