using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IOrderMesageFirebase
    {
        Task<string> SendFirebaseNotificationAsync(string deviceToken, string title, string body, Dictionary<string, string> data = null);
    }
}
