using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequests.BusinessLogic
{
    public class Enums
    {
        public enum CurrentStatus
        {
            NotApplicable,
            Created,
            InProgress,
            Complete,
            Canceled
        }
    }
}
