using LineCon.Data;
using LineCon.Models;
using System.Threading.Tasks;

namespace LineCon.Services
{
    public interface IRegistrationService
    {
        Task<Attendee> Register(NewAttendee attendee);
    }
}
