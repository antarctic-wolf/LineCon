using LineCon.Models;
using System.Threading.Tasks;

namespace LineCon.Services
{
    public interface ITicketWindowService
    {
        TicketWindow GetNextAvailable();
        Task<TicketWindow> Create();
    }
}
