using LineCon.Models;
using System;

namespace LineCon.Data
{
    public class AttendeeExistsException : Exception
    {
        public Attendee Attendee { get; set; }
        public AttendeeExistsException(Attendee attendee)
            : base($"An attendee with the confirmation number {attendee.ConfirmationNumber} already exists")
        {
            Attendee = attendee;
        }
    }
}
