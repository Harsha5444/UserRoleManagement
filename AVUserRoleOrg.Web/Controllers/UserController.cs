using AVUserRoleOrg.BLL;
using AVUserRoleOrg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AVUserRoleOrg.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRoleOrgBLL _userRoleOrgBLL;
        public UserController()
        {
            _userRoleOrgBLL = new UserRoleOrgBLL();
        }
        public ActionResult Index()
        {
            try
            {
                var userDetails = _userRoleOrgBLL.GetAllUserDetails();
                ViewBag.Organizations = GetOrganizationSelectList();
                ViewBag.Roles = GetRoleSelectList();
                return View(userDetails);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error fetching users: {ex.Message}";
                return View(new List<UserDetailViewModel>());
            }
        }
        private IEnumerable<SelectListItem> GetOrganizationSelectList()
        {
            var orgs = _userRoleOrgBLL.GetOrganizations();
            return orgs.Select(o => new SelectListItem
            {
                Value = o.OrgID.ToString(),
                Text = $"{o.OrgName} ({o.OrgType})"
            });
        }
        private IEnumerable<SelectListItem> GetRoleSelectList()
        {
            var roles = _userRoleOrgBLL.GetRoles();
            return roles.Select(r => new SelectListItem
            {
                Value = r.RoleID.ToString(),
                Text = r.RoleName
            });
        }
        [HttpPost]
        public ActionResult Create(UserDetailViewModel model, string selectedRoles)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(selectedRoles))
                    {
                        selectedRoles = "0";
                    }
                    int? roleId = Convert.ToInt32(selectedRoles);
                    _userRoleOrgBLL.CreateUser(model, roleId);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating user: {ex.Message}");
            }
            ViewBag.Organizations = GetOrganizationSelectList();
            ViewBag.Roles = GetRoleSelectList();
            return View("Index", _userRoleOrgBLL.GetAllUserDetails());
        }
        [HttpPost]
        public ActionResult Edit(UserDetailViewModel model, int? selectedRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRoleOrgBLL.UpdateUser(model, selectedRole);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating user: {ex.Message}");
            }
            ViewBag.Organizations = GetOrganizationSelectList();
            ViewBag.Roles = GetRoleSelectList();
            return View("Index", _userRoleOrgBLL.GetAllUserDetails());
        }
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _userRoleOrgBLL.DeleteUser(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting user: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}