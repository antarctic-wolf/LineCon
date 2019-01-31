using LineCon.Models;
using System;

namespace LineCon.Data.Exceptions
{
    public class AttendeeExistsException : Exception
    {
        public Attendee Attendee { get; set; }
        public string ConfirmationNumber { get; set; }

        public AttendeeExistsException(Attendee attendee)
            : base($"An attendee with the confirmation number {attendee.ConfirmationNumber} already exists")
        {
            Attendee = attendee;
            ConfirmationNumber = attendee.ConfirmationNumber;
        }

        public AttendeeExistsException(string confirmationNumber)
            : base($"An attendee with the confirmation number {confirmationNumber} already exists")
        {
            ConfirmationNumber = confirmationNumber;
        }
    }
}
