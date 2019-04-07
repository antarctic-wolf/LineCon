using LineCon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineCon.Models.ViewModels.App
{
    public class IndexViewModel
    {
        public bool RequireConfirmationNumber { get; set; }
        public NewAttendee NewAttendee { get; set; }
        public IEnumerable<TicketWindow> AailableWindows { get; set; }
        public TicketWindow SelectedWindow { get; set; }
    }
}
