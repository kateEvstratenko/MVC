using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult Index(List<Advertisement> advertisements, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;

            if(advertisements == null)
                advertisements = uow.UserRepository.Get(Int32.Parse(User.Identity.GetUserId())).Advertisements.ToList();

            return PartialView(advertisements.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult BackTo(string backUrl)
        {
            return Redirect(backUrl);
        }

        public ActionResult ViewAll()
        {
            return View();
        }

        public ActionResult ShowFilterForm(FilterAdvertisementViewModel model)
        {
            return PartialView("_FilterForm");
        }

        public ActionResult ShowAdvertisements(List<Advertisement> advertisements, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;

            if (advertisements == null)
                advertisements = uow.AdvertisementRepository.GetAll().ToList();

            return PartialView("_AdvertisementsList", advertisements.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ViewAdvertisementsList(FilterAdvertisementViewModel model)
        {
            var advertisements = uow.AdvertisementRepository.GetAll().ToList();

            if (model != null)
            {
                var onlyActiveAdvertisements = model.onlyActiveAdvertisements;

                if (onlyActiveAdvertisements)
                {
                    advertisements = advertisements.Where(item => item.Response == null).ToList();
                }

                if (model.advertisementViewType != AdvertisementViewType.All)
                {
                    advertisements = advertisements.Where(item => item.Type.ToString() == model.advertisementViewType.ToString()).ToList();
                }
            }

            return ShowAdvertisements(advertisements, null);
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
