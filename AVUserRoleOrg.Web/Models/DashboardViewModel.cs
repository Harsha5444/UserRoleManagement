using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVUserRoleOrg.Web.Models
{
    public class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public string OrgWiseUsersJson { get; set; }
        public string RoleWiseUsersJson { get; set; }
        public List<OrgWiseUser> OrgWiseUsers { get; set; } = new List<OrgWiseUser>();
        public List<RoleWiseUser> RoleWiseUsers { get; set; } = new List<RoleWiseUser>();
    }

    public class OrgWiseUser
    {
        public string OrgName { get; set; }
        public int Count { get; set; }
    }

    public class RoleWiseUser
    {
        public string Rolename { get; set; }
        public int Count { get; set; }
    }
}