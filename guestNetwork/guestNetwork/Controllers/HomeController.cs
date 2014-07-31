using System.Web.Mvc;

namespace guestNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork uow;

        public HomeController(IUnitOfWork uowInstance)
        {
            uow = uowInstance;
        }
        public ActionResult Index()
        {
            return View();
        }
  
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}