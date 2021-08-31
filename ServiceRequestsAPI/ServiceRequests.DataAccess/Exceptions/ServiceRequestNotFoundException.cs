using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequests.DataAccess
{
    public class ServiceRequestNotFoundException : Exception
    {
        public ServiceRequestNotFoundException(string msg) : base(msg)
        {

        }
    }
}
