using System;
using System.Net;
using System.Web.Mvc;
using guestNetwork.Models;
using Microsoft.AspNet.Identity;

namespace guestNetwork.Controllers
{
    public class ResponseController : Controller
    {
        private readonly IUnitOfWork uow;

        public ResponseController(IUnitOfWork uowInstance)
        {
            uow = uowInstance;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(uow.ResponseRepository.GetAll());
           // var responses = db.Responses.Include(r => r.Advertisement).Include(r => r.User);
           // return View(responses.ToList());
        }

        // GET: /Response/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = uow.ResponseRepository.Get(id.Value);
            if (response == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", response);
        }

        [Authorize]
        public ActionResult Create(int id)
        {
            //ViewBag.Id = new SelectList(uow.AdvertisementRepository.Get(id), "Id", "Title");
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Firstname");

            var model = new Response
            {
                AdvertisementId = id,
                UserId = Int32.Parse(User.Identity.GetUserId()),
                //UserName = uow.UserRepository.Get(Int32.Parse(User.Identity.GetUserId())).UserName,
                //UserName = User.Identity.GetUserName()
            };
            return PartialView("_Create", model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Response responseModel)
        {
            if (ModelState.IsValid)
            {
                var response = new Response
                {
                    AdvertisementId = responseModel.AdvertisementId,
                    UserId = responseModel.UserId,
                    Message = responseModel.Message,
                };

                uow.ResponseRepository.Insert(response);
                uow.Save();

                response = uow.ResponseRepository.Get(responseModel.AdvertisementId);
                return Details(response.AdvertisementId);
            }
            return PartialView("_Create", responseModel);
        }

        // GET: /Response/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = uow.ResponseRepository.Get(id.Value);
            if (response == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Id = new SelectList(uow.AdvertisementRepository.GetAll(), "Id", "Title", response.AdvertisementId);
            //ViewBag.UserId = new SelectList(uow.UserRepository.GetAll(), "Id", "Firstname", response.UserId);
            return PartialView("_Edit", response);
        }

        // POST: /Response/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Response response)
        {
            if (ModelState.IsValid)
            {
                uow.ResponseRepository.Update(response);
                uow.Save();
                return PartialView("_Details", response);
            }

            return View(response);
        }

        // GET: /Response/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = uow.ResponseRepository.Get(id.Value);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View("_Delete", response);
        }

        // POST: /Response/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            uow.ResponseRepository.Delete(id);
            uow.ResponseRepository.Save();
            return RedirectToAction("Details", "Advertisement", new { id = id });
        }

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
