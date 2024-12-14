using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IAzureComputerVisionService
    {
        Task<bool> IsBookOrAgendaAsync(Stream imageStream);
    }
}
