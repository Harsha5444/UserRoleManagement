using AVUserRoleOrg.DAL;
using AVUserRoleOrg.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AVUserRoleOrg.BLL
{
    public class UserRoleOrgBLL
    {
        private readonly UserRoleOrgDAL _userRoleOrgDAL;
        public UserRoleOrgBLL()
        {
            _userRoleOrgDAL = new UserRoleOrgDAL();
        }
        public List<UserDetailViewModel> GetAllUserDetails()
        {
            var users = _userRoleOrgDAL.GetUsers();
            var orgs = _userRoleOrgDAL.GetOrganizations();
            var userRoles = _userRoleOrgDAL.GetUserRoles();
            var roles = _userRoleOrgDAL.GetRoles();
            var userDetails = users.Select(user => new UserDetailViewModel
            {
                UserID = user.UserID,
                UserName = user.UserName,
                Email = user.Email,
                UserLoginId = user.UserLoginId,
                IsActive = user.IsActive,
                OrganizationID = user.OrganizationID,
                OrgName = orgs.FirstOrDefault(o => o.OrgID == user.OrganizationID)?.OrgName,
                OrgType = orgs.FirstOrDefault(o => o.OrgID == user.OrganizationID)?.OrgType,
                Roles = userRoles
                    .Where(ur => ur.UserID == user.UserID)
                    .Select(ur => roles.FirstOrDefault(r => r.RoleID == ur.RoleID))
                    .Where(r => r != null)
                    .Select(r => new RoleViewModel
                    {
                        RoleID = r.RoleID,
                        RoleName = r.RoleName,
                        RoleDescription = r.RoleDescription
                    })
                    .ToList()
            }).ToList();

            return userDetails;
        }
        public IEnumerable<Organization> GetOrganizations()
        {
            return _userRoleOrgDAL.GetOrganizations();
        }
        public IEnumerable<Roles> GetRoles()
        {
            return _userRoleOrgDAL.GetRoles();
        }
        public void CreateUser(UserDetailViewModel model, int? selectedRole)
        {
            var user = new Users
            {
                UserName = model.UserName,
                Email = model.Email,
                UserLoginId = model.UserLoginId,
                OrganizationID = model.OrganizationID,
                IsActive = model.IsActive
            };

            int userId = _userRoleOrgDAL.CreateUser(user);

            if (selectedRole.HasValue && selectedRole > 0)
            {
                _userRoleOrgDAL.UpdateUserRole(userId, selectedRole.Value);
            }
            else
            {
                selectedRole = 0;
                _userRoleOrgDAL.UpdateUserRole(userId, selectedRole.Value);
            }

        }
        public void UpdateUser(UserDetailViewModel model, int? selectedRole)
        {
            var user = new Users
            {
                UserID = model.UserID,
                UserName = model.UserName,
                Email = model.Email,
                UserLoginId = model.UserLoginId,
                OrganizationID = model.OrganizationID,
                IsActive = model.IsActive
            };
            _userRoleOrgDAL.UpdateUser(user);
            if (selectedRole.HasValue && selectedRole > 0)
            {
                _userRoleOrgDAL.UpdateUserRole(model.UserID, selectedRole.Value);
            }
            else
            {
                selectedRole = 0;
                _userRoleOrgDAL.UpdateUserRole(model.UserID, selectedRole.Value);
            }
        }
        public void DeleteUser(int userId)
        {
            _userRoleOrgDAL.UpdateUserRole(userId, 0);
            _userRoleOrgDAL.DeleteUser(userId);
        }
    }
}
