using System.Data.Linq.Mapping;

namespace AVUserRoleOrg.Models
{
    [Table(Name = "Users")]
    public class Users
    {
        [Column(IsPrimaryKey = true, IsDbGenerated =true)]
        public int UserID { get; set; }

        [Column]
        public string UserName { get; set; }

        [Column]
        public string Email { get; set; }

        [Column]
        public string UserLoginId { get; set; }

        [Column]
        public int? OrganizationID { get; set; }

        [Column]
        public bool IsActive { get; set; } = true;
    }

}
