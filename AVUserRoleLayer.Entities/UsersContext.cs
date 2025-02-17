using AVUserRoleOrg.Models;
using System.Data.Linq;

namespace AVUserRoleLayer.Entities
{
    public class UsersContext : DataContext
    {
        public Table<Users> Users => GetTable<Users>();
        public Table<UserRoles> UserRoles => GetTable<UserRoles>();

        public UsersContext(string connectionString) : base(connectionString)
        {
        }
    }
}
