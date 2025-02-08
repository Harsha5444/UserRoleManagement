using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVUserRoleOrg.Models
{
    public class Organization
    {
        public int OrgID { get; set; } 
        public string OrgName { get; set; } = string.Empty;
        public string OrgType { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
