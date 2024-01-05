using LeaveAppManagement.dataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveAppManagement.dataAccess.Dto
{
    public class LeaveReportingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TotalRequest { get; set; }
        public int TotalPending { get; set; }
        public int TotalApproved { get; set; }
        public int TotalRejected { get; set; }
        public int TotalUsers { get; set; }
    }
}
