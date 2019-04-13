using LineCon.Data;
using LineCon.Data.Exceptions;
using LineCon.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Attendee> Register(NewAttendee newAttendee)
        {
            var conConfig = (await _context.Conventions
                .Include(c => c.ConConfig)
                    .ThenInclude(cc => cc.RegistrationHours)
                .SingleOrDefaultAsync(c => c.ConventionId == newAttendee.ConventionId))
                .ConConfig;
            if (await _context.Attendees.AnyAsync(a => a.ConfirmationNumber == newAttendee.ConfirmationNumber))
            {
                throw new AttendeeExistsException(newAttendee.ConfirmationNumber);
            }

            var attendee = new Attendee()
            {
                AttendeeId = Guid.NewGuid(),
                ConfirmationNumber = newAttendee.ConfirmationNumber,
                BadgeName = newAttendee.BadgeName
            };
            _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();
            return attendee;
        }
    }
}
