using LineCon.Data;
using System.Threading.Tasks;

namespace LineCon.Services
{
    public interface IRegistrationService
    {
        Task Register(NewAttendee attendee);
    }
}
