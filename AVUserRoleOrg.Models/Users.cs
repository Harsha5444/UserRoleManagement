using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVUserRoleOrg.Models
{
    public class Users
    {
        public int UserID { get; set; }  
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserLoginId { get; set; } = string.Empty;
        public int? OrganizationID { get; set; } 
        public bool IsActive { get; set; } = true;  
    }
}
