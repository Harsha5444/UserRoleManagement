using AVUserRoleOrg.BLL;
using AVUserRoleOrg.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AVUserRoleOrg.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRoleOrgBLL _userRoleOrgBLL;
        UserRoleOrgDBEntities dBEntities = new UserRoleOrgDBEntities();
        public HomeController()
        {
            _userRoleOrgBLL = new UserRoleOrgBLL();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Organizations()
        {
            var organizations = _userRoleOrgBLL.GetOrganizations();
            return View(organizations);
        }
        public ActionResult Roles()
        {
            var roles = _userRoleOrgBLL.GetRoles();
            return View(roles);
        }
        public ActionResult Dashboard()
        {
            //var totalUsers = dBEntities.Dashboard_Vw.Count();
            //var activeUsers = dBEntities.Dashboard_Vw.Count(u => u.isactive == true);

            //var orgWiseUsers = dBEntities.Dashboard_Vw
            //                        .GroupBy(u => u.Orgname)
            //                        .Select(g => new OrgWiseUser { OrgName = g.Key, Count = g.Count() })
            //                        .ToList();

            //var roleWiseUsers = dBEntities.Dashboard_Vw
            //                        .GroupBy(u => u.rolename)
            //                        .Select(g => new RoleWiseUser { Rolename = g.Key, Count = g.Count() })
            //                        .ToList();
            //var dashboardData = new DashboardViewModel
            //{
            //    TotalUsers = totalUsers,
            //    ActiveUsers = activeUsers,
            //    OrgWiseUsers = orgWiseUsers,
            //    RoleWiseUsers = roleWiseUsers
            //};
            //var dashboardData2 = dBEntities.Database.SqlQuery<DashboardViewModel>("EXEC GetDashboardData").FirstOrDefault();
            var result = dBEntities.Database.SqlQuery<DashboardViewModel>("EXEC GetDashboardData").FirstOrDefault();
            result.OrgWiseUsers = JsonConvert.DeserializeObject<List<OrgWiseUser>>(result.OrgWiseUsersJson);
            result.RoleWiseUsers = JsonConvert.DeserializeObject<List<RoleWiseUser>>(result.RoleWiseUsersJson);

            return View(result); 
        }
    }
}