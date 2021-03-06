

namespace UpExcise.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuditTB
    {
        public int UsersAuditID { get; set; }
        public Int64 UserID { get; set; }
        public string SessionID { get; set; }
        public string IPAddress { get; set; }
        public string PageAccessed { get; set; }
        public Nullable<System.DateTime> LoggedInAt { get; set; }
        public Nullable<System.DateTime> LoggedOutAt { get; set; }
        public string LoginStatus { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}
