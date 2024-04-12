using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, Stream attachment, string attachmentName);
    }
}
