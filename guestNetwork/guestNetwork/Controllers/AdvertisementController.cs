using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using guestNetwork.Models;
using guestNetwork.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;
using PagedList;

namespace guestNetwork.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly UnitOfWork uow = new UnitOfWork();

        // GET: /Advertisement/
        [Authorize]
        public ActionResult Index(List<Advertisement> advertisements, int? page)
        {
            int pageNumber = page ?? 1;
            var pageSize = 10;

            if(advertisements == null)
                advertisements = uow.UserRepository.Get(Int32.Parse(User.Identity.GetUserId())).Advertisements.ToList();

            return PartialView(advertisements.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult BackTo(string backUrl)
        {
            return Redirect(backUrl);
        }

        public ActionResult ViewAll(string backUrl, bool? onlyActive, AdvertisementViewType? type, int? page)
        {
           /* if (Request.HttpMethod == "GET")
            {
                page = 1;
            } */

            int pageNumber = page ?? 1;
            int pageSize = 10;

            var advertisements = uow.AdvertisementRepository.GetAll().ToList();

           // if (filterModel != null)
            {
                if (onlyActive == true)
                {
                    advertisements = advertisements.Where(item => item.Response == null).ToList();
                }

                if (type != null && type != AdvertisementViewType.All)
                {
                    advertisements = advertisements.Where(item => item.Type.ToString() == type.ToString()).ToList();
                }
            }

            var url = Request.Url.ToString();
            ViewBag.Url = url;
            ViewBag.onlyActive = onlyActive;
            ViewBag.type = type;

            return View("ViewAll", advertisements.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ShowFilterForm(FilterAdvertisementViewModel model, string backUrl)
        {
            ViewBag.Url = backUrl;

            return PartialView("_FilterForm", model);
        }

        /*public ActionResult ViewAdvertisements(FilterAdvertisementViewModel model, IPagedList<Advertisement> advertisements)
        {
            ViewBag.FilterModel = model;
            return PartialView("_AdvertisementsList", advertisements);
        }*/

        public ActionResult LastAdvertisements()
        {
            var advertisements = uow.AdvertisementRepository.GetAll().ToList();
            advertisements.Reverse();

            var advertisementsList = new List<Advertisement>();
            for (int i = 0; i < 3 && i < advertisements.Count; i++)
            {

                advertisementsList.Add(advertisements[i]);
            }

            return PartialView("_LastAdvertisements", advertisementsList);
        }
        // GET: /Advertisement/Details/5
        public ActionResult Details(int id, string backUrl)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            Advertisement advertisement = uow.AdvertisementRepository.Get(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }

            ViewBag.BackUrl = backUrl;
            return View(advertisement);
        }

        // GET: /Advertisement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Advertisement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( AdvertisementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var advertisement = new Advertisement()
                {
                    Title = model.Title,
                    Content = model.Content,
                    Type = model.Type,
                    UserId = uow.UserRepository.Get(Int32.Parse(User.Identity.GetUserId())).Id,
                };

                var file = Request.Files["file"];
                var fileName = Path.GetFileName(file.FileName);

                var advPath = Guid.NewGuid() + Path.GetExtension(file.FileName);

                var path = Path.Combine(Server.MapPath("~/Content/Images"), advPath);
                file.SaveAs(path);

                advertisement.mainImagePath = "~/Content/Images/" + advPath;
                
                uow.AdvertisementRepository.Insert(advertisement);
                uow.Save();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: /Advertisement/Edit/5
        public ActionResult Edit(int id, string backUrl)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            Advertisement advertisement = uow.AdvertisementRepository.Get(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }

            ViewBag.BackUrl = backUrl;
            return View(advertisement);
        }

        // POST: /Advertisement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Advertisement advertisement, string backUrl)
        {
            if (ModelState.IsValid)
            {
                uow.AdvertisementRepository.Update(advertisement);
                uow.Save();
                return Redirect(backUrl);
            }

            ViewBag.BackUrl = backUrl;
            return View(advertisement);
        }

        // GET: /Advertisement/Delete/5
        public ActionResult Delete(int id, string backUrl)
        {
            Advertisement advertisement = uow.AdvertisementRepository.Get(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }

            ViewBag.BackUrl = backUrl;
            return View(advertisement);
        }

        // POST: /Advertisement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var advertisement = uow.AdvertisementRepository.Get(id);
            var response = advertisement.Response;

            if (response != null)
                uow.ResponseRepository.Delete(response.AdvertisementId);

            uow.AdvertisementRepository.Delete(id);
            uow.Save();
            return RedirectToAction("Index");
        }


        /*public ActionResult Respond()
        {
            
        }*/

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
