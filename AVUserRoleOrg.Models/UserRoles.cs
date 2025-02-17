using System.Data.Linq.Mapping;

namespace AVUserRoleOrg.Models
{
    [Table(Name = "UserRoles")]
    public class UserRoles
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int UserRoleID { get; set; } 

        [Column]
        public int UserID { get; set; } 

        [Column]
        public int RoleID { get; set; } 
    }
}
