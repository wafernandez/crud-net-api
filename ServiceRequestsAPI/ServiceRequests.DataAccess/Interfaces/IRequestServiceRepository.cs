using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceRequests.BusinessLogic;


namespace ServiceRequests.DataAccess
{
    public interface IRequestServiceRepository
    {
        ServiceRequest Add(ServiceRequest serviceRequest);
        
        bool IsEmpty();
        
        ServiceRequest Get(Guid id);
        
        IEnumerable<ServiceRequest> GetAll();
        
        void Delete(Guid id);

        ServiceRequest Update(ServiceRequest serviceRequest);

        public void Clear();
    }
}
