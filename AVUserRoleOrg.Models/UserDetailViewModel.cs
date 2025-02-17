using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AVUserRoleOrg.Models
{
    public class UserDetailViewModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Login ID is required.")]
        [EmailAddress(ErrorMessage = "Invalid LoginId.")]
        public string UserLoginId { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Organization is required.")]
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
