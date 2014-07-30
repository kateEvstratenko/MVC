using guestNetwork.Models;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult LastAdvertisements()
        {
            var advertisements = uow.AdvertisementRepository.GetAll().ToList();
            advertisements.Reverse();

            var advertisementsList = new List<Advertisement>();
            for (var i = 0; i < 3 && i < advertisements.Count; i++)
            {

                advertisementsList.Add(advertisements[i]);
            }

            return PartialView("_LastAdvertisements", advertisementsList);
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