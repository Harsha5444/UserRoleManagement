using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVUserRoleOrg.Models
{
    public class Roles
    {
        public int RoleID { get; set; }  // Primary Key
        public string RoleName { get; set; } = string.Empty;
        public string RoleDescription { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;  // Default to active
    }
}
