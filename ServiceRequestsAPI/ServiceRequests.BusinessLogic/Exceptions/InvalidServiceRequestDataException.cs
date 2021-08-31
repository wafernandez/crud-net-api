using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequests.BusinessLogic
{
    public class InvalidServiceRequestDataException : Exception
    {
        public InvalidServiceRequestDataException(string msg) : base(msg)
        {

        }
    }
}
