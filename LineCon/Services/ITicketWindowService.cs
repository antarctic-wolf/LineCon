using LineCon.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LineCon.Services
{
    public interface ITicketWindowService
    {
        Task<IEnumerable<TicketWindow>> GetAllAvailable(Guid conventionId);
        Task<TicketWindow> GetNextAvailable(Guid conventionId);
        Task<TicketWindow> Create(Guid conventionId);
    }
}
