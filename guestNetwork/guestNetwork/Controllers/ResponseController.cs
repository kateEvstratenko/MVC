﻿using System;
using System.Data.Entity;
using System.Linq;
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
            var model = new Response
            {
                AdvertisementId = id,
                UserId = Int32.Parse(User.Identity.GetUserId()),
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
                uow.Commit();

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
            return PartialView("_Edit", response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Response response)
        {
            if (!ModelState.IsValid)
                return View(response);

            uow.ResponseRepository.Update(response);
            uow.Commit();

            var model = uow.ResponseRepository.GetAll().Include("User").SingleOrDefault(x => x.AdvertisementId == response.AdvertisementId);

            return PartialView("_Details", model);
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
    }
}
