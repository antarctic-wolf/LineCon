using LineCon.Data;
using LineCon.Data.Exceptions;
using LineCon.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LineCon.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly LineConContext _context;

        public RegistrationService(LineConContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new Attendee
        /// </summary>
        /// <param name="newAttendee"></param>
        /// <returns></returns>
        public async Task Register(NewAttendee newAttendee)
        {
            if (_context.Attendees.Any(a => a.ConfirmationNumber == newAttendee.ConfirmationNumber))
            {
                throw new AttendeeExistsException(newAttendee.ConfirmationNumber);
            }

            _context.Attendees.Add(new Attendee()
            {
                AttendeeId = Guid.NewGuid(),
                ConfirmationNumber = newAttendee.ConfirmationNumber,
                BadgeName = newAttendee.BadgeName
            });
            await _context.SaveChangesAsync();
        }
    }
}
