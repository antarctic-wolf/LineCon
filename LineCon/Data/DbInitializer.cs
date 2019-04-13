using LineCon.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LineCon.Data
{
    public class DbInitializer
    {
        public static void Initialize(LineConContext context)
        {
            context.Database.EnsureCreated();
            if (context.Conventions.Any()) return;

            var conId = Guid.NewGuid();

            var configId = Guid.NewGuid();
            var registrationHours = new List<RegistrationHours>()
            {
                new RegistrationHours()
                {
                    RegistrationHoursId = Guid.NewGuid(),
                    ConConfigId = configId,
                    StartTime = new DateTime(2020, 1, 1, 8, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 14, 0, 0)
                }
            };
            var config = new ConConfig()
            {
                ConConfigId = configId,
                RegistrationHours = registrationHours,
                TicketWindowInterval = new TimeSpan(0, 15, 0),
                TicketWindowCapacity = 15,
                RequireConfirmationNumber = false
            };

            var confNumbers = new List<ConfirmationNumber>()
            {
                new ConfirmationNumber()
                {
                    ConventionId = conId,
                    ConConfigId = configId,
                    Number = "abc123"
                }
            };

            var attendees = new List<Attendee>()
            {
                new Attendee()
                {
                    AttendeeId = Guid.NewGuid(),
                    ConventionId = conId,
                    ConfirmationNumber = "abc123",
                    BadgeName = "Nerok"
                }
            };

            var windows = new List<TicketWindow>()
            {
                new TicketWindow()
                {
                    TicketWindowId = Guid.NewGuid(),
                    ConventionId = conId,
                    StartTime = new DateTime(2020, 1, 1, 8, 0, 0),
                    Length = new TimeSpan(0, 15, 0),
                    AttendeeTickets = new List<AttendeeTicket>(),
                    AttendeeCapacity = 15
                }
            };

            var convention = new Convention()
            {
                ConventionId = conId,
                ConConfigId = configId,
                UrlIdentifier = "nerokcon",
                ConfirmationNumbers = confNumbers,
                Attendees = attendees,
                TicketWindows = windows,
                AttendeeTickets = new List<AttendeeTicket>()
            };

            context.ConfirmationNumbers.AddRange(confNumbers);
            context.ConConfigs.Add(config);
            context.RegistrationHours.AddRange(registrationHours);
            context.Attendees.AddRange(attendees);
            context.TicketWindows.AddRange(windows);
            context.Conventions.Add(convention);
            context.SaveChanges();
        }
    }
}
