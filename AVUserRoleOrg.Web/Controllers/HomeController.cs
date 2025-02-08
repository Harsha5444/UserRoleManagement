using AVUserRoleOrg.BLL;
using System.Web.Mvc;

namespace AVUserRoleOrg.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRoleOrgBLL _userRoleOrgBLL;
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
    }
}