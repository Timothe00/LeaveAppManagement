using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveAppManagement.dataAccess.Dto
{
    public class RequestStatusDto
    {
        public int Id { get; set; }
        public string RequestStatus { get; set; } = string.Empty;
    }
}
