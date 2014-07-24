using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using guestNetwork.DAL;
using guestNetwork.Models;
using Microsoft.AspNet.Identity;

namespace guestNetwork.Controllers
{
    public class ResponseController : Controller
    {
        private UnitOfWork uow = new UnitOfWork();

        // GET: /Response/
        public ActionResult Index()
        {
            return View(uow.ResponseRepository.GetAll());
           // var responses = db.Responses.Include(r => r.Advertisement).Include(r => r.User);
           // return View(responses.ToList());
        }

        // GET: /Response/Details/5
       /* public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }*/

        // GET: /Response/Create
        public ActionResult Create(int id)
        {
            //ViewBag.Id = new SelectList(uow.AdvertisementRepository.Get(id), "Id", "Title");
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Firstname");

            ResponseCreateViewModel model = new ResponseCreateViewModel()
            {
                Id = id,
                UserId = Int32.Parse(User.Identity.GetUserId()),
                UserName = uow.UserRepository.Get(Int32.Parse(User.Identity.GetUserId())).UserName
            };
            return PartialView("_Create", model);
        }

        // POST: /Response/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Message,UserId, UserName")] ResponseCreateViewModel responseModel)
        {
            if (ModelState.IsValid)
            {
                var response = new Response()
                {
                    AdvertisementId = responseModel.Id,
                    UserId = responseModel.UserId,
                    Message = responseModel.Message,
                };

                uow.ResponseRepository.Insert(response);
                uow.Save();
                return PartialView("_AddedResponse", responseModel);
            }

            //ViewBag.Id = new SelectList(db.Advertisements, "Id", "Title", response.Id);
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Firstname", response.UserId);
            return PartialView("_Create", responseModel);
        }

        // GET: /Response/Edit/5
        /*public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Advertisements, "Id", "Title", response.Id);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Firstname", response.UserId);
            return View(response);
        }

        // POST: /Response/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Message,UserId")] Response response)
        {
            if (ModelState.IsValid)
            {
                db.Entry(response).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Advertisements, "Id", "Title", response.Id);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Firstname", response.UserId);
            return View(response);
        }

        // GET: /Response/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // POST: /Response/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Response response = db.Responses.Find(id);
            db.Responses.Remove(response);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
