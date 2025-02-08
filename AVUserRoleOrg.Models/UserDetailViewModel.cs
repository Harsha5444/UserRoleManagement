using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVUserRoleOrg.Models
{
    public class UserDetailViewModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserLoginId { get; set; }
        public bool IsActive { get; set; }
        public int? OrganizationID { get; set; }
        public string OrgName { get; set; }
        public string OrgType { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
    public class RoleViewModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }   
}
