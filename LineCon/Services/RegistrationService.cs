using LineCon.Data;
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
            //I'd like this to be SingleOrDefault, as we should ideally never have more than one,
            //  but it's not a Guid and it's technically possible that there would be multiple.
            var existingAttendee = _context.Attendees.FirstOrDefault(a => a.ConfirmationNumber == newAttendee.ConfirmationNumber);
            if (existingAttendee != null)
            {
                throw new AttendeeExistsException(existingAttendee);
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
