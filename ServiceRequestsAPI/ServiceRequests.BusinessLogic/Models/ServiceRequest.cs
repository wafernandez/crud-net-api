using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServiceRequests.BusinessLogic
{
    public class ServiceRequest
    {
        private Enums.CurrentStatus _currentStatus;

        public Guid Id { get; set; }

        [Required]
        public string BuildingCode { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string CurrentStatus { get { return _currentStatus.ToString(); } set { SetCurrentStatus(value); } }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        private void SetCurrentStatus(string status)
        {
            if (!Enum.TryParse(status, out _currentStatus)){
                throw new InvalidServiceRequestDataException($"{status} is not a valid Status value.");
            }
        }
    }
}
