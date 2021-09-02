using Microsoft.AspNetCore.Mvc;
using ServiceRequests.BusinessLogic;
using ServiceRequests.DataAccess;
using ServiceRequests.Utilities;
using ServiceRequests.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace ServiceRequests.WebAPI.Test
{
    public class ServiceRequestControllerTest
    {
        private ServiceRequestController _controller;
        private IRequestServiceRepository _repository;
        private INotifier _notifier;
        private ServiceRequest _testService;

        public ServiceRequestControllerTest()
        {
            _repository = new ServiceRequestListRepository();
            _notifier = new EmailNotifier();
            _controller = new ServiceRequestController(_repository, _notifier);
        }

        [Fact]
        public void GetAllReturnsOkResult()
        {
            var result = _controller.GetAll();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllReturnsItems()
        {
            var result = _controller.GetAll().Result as OkObjectResult;
            var items = Assert.IsType<List<ServiceRequest>>(result.Value);

            Assert.Equal(4, items.Count);
        }

        [Fact]
        public void AddServiceRequestReturnsCreatedResult()
        {
            ServiceRequest newServiceRequest = new ServiceRequest();
            var result = _controller.Create(newServiceRequest);

            Assert.IsType<CreatedResult>(result.Result);
        }

        [Fact]
        public void AddServiceRequestReturnsCreatedItemResult()
        {
            ServiceRequest newServiceRequest = new ServiceRequest();
            var result = _controller.Create(newServiceRequest).Result as CreatedResult;
            _testService = Assert.IsType<ServiceRequest>(result.Value);

            Assert.NotNull(_testService);
        }

        [Fact]
        public void GetServiceRequestReturnsOkResult()
        {
            var result = _controller.Get("233d29d6-1859-4ded-90f0-addd5bc4f8b4");

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetServiceRequestReturnsItem()
        {
            var result = _controller.Get("233d29d6-1859-4ded-90f0-addd5bc4f8b4").Result as OkObjectResult;
            var serviceRequest = Assert.IsType<ServiceRequest>(result.Value);

            Assert.NotNull(serviceRequest);
        }

        [Fact]
        public void GetServiceRequestReturnsNotFound()
        {
            var result = _controller.Get(Guid.NewGuid().ToString());

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void UpdateServiceRequestReturnsOkResult()
        {
            ServiceRequest toUpdate = new ServiceRequest()
            {
                CreatedBy = "User",
                Description = "Description"
            };
            var result = _controller.Update("233d29d6-1859-4ded-90f0-addd5bc4f8b4", toUpdate);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void UpdateServiceRequestReturnsUpdatedItem()
        {
            string newStatus = "Complete";
            ServiceRequest toUpdate = new ServiceRequest()
            {
                CreatedBy = "Updated user",
                CurrentStatus = newStatus
            };

            var result = _controller.Update("233d29d6-1859-4ded-90f0-addd5bc4f8b4", toUpdate).Result as OkObjectResult;
            var updatedService = Assert.IsType<ServiceRequest>(result.Value);

            Assert.NotNull(updatedService);
            Assert.Equal(newStatus, updatedService.CurrentStatus);
        }

        [Fact]
        public void UpdateServiceRequestReturnsNotFound()
        {
            var result = _controller.Update(Guid.NewGuid().ToString(), new ServiceRequest());

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteServiceRequestReturnsAcceptedResult()
        {
            var result = _controller.Delete("233d29d6-1859-4ded-90f0-addd5bc4f8b4");

            Assert.IsType<AcceptedResult>(result);
        }

        [Fact]
        public void DeleteServiceRequestReturnsNotFoudResult()
        {
            var result = _controller.Delete("233d29d6-1859-4ded-90f0-addd5bc4f8b5");

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void GetAllReturnsNoContent()
        {
            _repository.Clear();
            var result = _controller.GetAll();

            Assert.IsType<NoContentResult>(result.Result);
        }
    }
}
