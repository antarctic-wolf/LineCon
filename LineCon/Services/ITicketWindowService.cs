using LineCon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LineCon.Services
{
    public interface ITicketWindowService
    {
        IEnumerable<TicketWindow> GetAllAvailable();
        Task<TicketWindow> GetNextAvailable();
        Task<TicketWindow> Create();
    }
}
