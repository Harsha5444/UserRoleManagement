namespace AVUserRoleOrg.Models
{
    public class Roles
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool IsActive { get; set; } = true;  
    }
}
