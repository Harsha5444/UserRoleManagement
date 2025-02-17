namespace AVUserRoleOrg.Models
{
    public class UserRoles
    {
        public int UserRoleID { get; set; }  // Primary Key
        public int UserID { get; set; }  // Foreign Key
        public int RoleID { get; set; }  // Foreign Key
    }
}
