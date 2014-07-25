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

namespace guestNetwork.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly UnitOfWork uow = new UnitOfWork();

        // GET: /Advertisement/
        public ActionResult Index()
        {
            return View(uow.UserRepository.Get(Int32.Parse(User.Identity.GetUserId())).Advertisements.ToList());
        }

        public ActionResult ViewAll()
        {
            return View(new FilterAdvertisementViewModel());
        }

        public ActionResult ViewAdvertisementsList(FilterAdvertisementViewModel model)
        {
            var advertisements = uow.AdvertisementRepository.GetAll().ToList();

            if (model == null)
            {
                return PartialView("_AdvertisementsList", advertisements);
            }
            else
            {
                return PartialView("_AdvertisementsList", advertisements.Where(item => item.Type == model.Type));
            }
        }
        // GET: /Advertisement/Details/5
        public ActionResult Details(int id)
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
        public ActionResult Create([Bind(Include="Id,UserId,Title,Content,Type")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                advertisement.UserId = uow.UserRepository.Get(Int32.Parse(User.Identity.GetUserId())).Id;
                uow.AdvertisementRepository.Insert(advertisement);
                uow.Save();
                return RedirectToAction("Index");
            }

            return View(advertisement);
        }

        // GET: /Advertisement/Edit/5
        public ActionResult Edit(int id)
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
            return View(advertisement);
        }

        // POST: /Advertisement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,UserId,Title,Content,Type")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                uow.AdvertisementRepository.Update(advertisement);
                uow.Save();
                return RedirectToAction("Index");
            }
            return View(advertisement);
        }

        // GET: /Advertisement/Delete/5
        public ActionResult Delete(int id)
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
            return View(advertisement);
        }

        // POST: /Advertisement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
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
