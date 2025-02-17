using System;

namespace AVUserRoleOrg.Models
{
    public class Organization
    {
        public int OrgID { get; set; }
        public string OrgName { get; set; } 
        public string OrgType { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
