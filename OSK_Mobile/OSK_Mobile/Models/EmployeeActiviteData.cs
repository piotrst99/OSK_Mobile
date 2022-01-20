using System;
using System.Collections.Generic;
using System.Text;

namespace OSK_Mobile.Models
{
    public class EmployeeActiviteData {
        public int UserID { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public int CountOfPlaned { get; set; }
        public int CountOfDone { get; set; }
        public int CountOfCancel { get; set; }
    }
}
