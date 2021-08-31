using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequests.Utilities
{
    public interface INotifier
    {
        void SendNotification(string messageText);
    }
}
