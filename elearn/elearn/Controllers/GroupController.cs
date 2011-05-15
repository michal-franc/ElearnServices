using System.Web.Mvc;
using elearn.GroupService;

namespace elearn.Controllers
{
    public class GroupController : Controller
    {

        private IGroupService _service;

        public GroupController(IGroupService service)
        {
            _service = service;
        }

        //
        // Get: /Group/Details/id
        public ActionResult Details(int id)
        {
            var group = _service.GetGroup(id);
            return View(group);
        }
    }
}
