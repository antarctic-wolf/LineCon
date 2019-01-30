using LineCon.Models;
using System.Threading.Tasks;

namespace LineCon.Services
{
    public interface ITicketQueueService
    {
        Task<TicketWindow> Enqueue(Attendee attendee);
        Task Dequeue(Attendee attendee);
    }
}
