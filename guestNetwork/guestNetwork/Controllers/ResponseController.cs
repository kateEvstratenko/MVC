using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using DAL;
using DAL.Models;
using guestNetwork.ViewModels;
using Microsoft.AspNet.Identity;

namespace guestNetwork.Controllers
{
    [Authorize]
    public class ResponseController : Controller
    {
        private readonly IUnitOfWork uow;

        public ResponseController(IUnitOfWork uowInstance)
        {
            uow = uowInstance;
        }

        public ActionResult Index()
        {
            var responses = uow.ResponseRepository.GetAll();

            var model = Mapper.Map<IList<ResponseViewModel>>(responses);

            return View(model);
        }

        public ActionResult ViewForAdvertisement(int id)
        {
            var advertisement = uow.AdvertisementRepository.Get(id);

            if(advertisement.Response == null )
                if (Int32.Parse(User.Identity.GetUserId()) != advertisement.UserId)
                {
                    return PartialView("_ViewForAdvertisement", id);
                }
                else
                {
                    return new EmptyResult();
                }

            var response = uow.ResponseRepository.Get(id);
            var model = Mapper.Map<ResponseViewModel>(response);

            return PartialView("_Details", model);
        }

        public ActionResult Details(int id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            Response response = uow.ResponseRepository.GetAll().Include("User").Include("Advertisement").SingleOrDefault(x => x.AdvertisementId == id);
            if (response == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<ResponseViewModel>(response);

            ViewBag.backUrl = Request.Url.ToString();

            return PartialView("_Details", model);
        }

        public ActionResult Create(int id)
        {
            var user = uow.UserRepository.Get(Int32.Parse(User.Identity.GetUserId())); 

            var model = new ResponseViewModel
            {
                AdvertisementId = id,
                UserId = user.Id,
                AdvertisementUserId = uow.AdvertisementRepository.Get(id).UserId,
                UserName = user.UserName,
                Message = ""
            };
            return PartialView("_Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ResponseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = Mapper.Map<Response>(model);

                uow.ResponseRepository.Insert(response);
                uow.Commit();

                response = uow.ResponseRepository.Get(model.AdvertisementId);
                return Details(response.AdvertisementId);
            }
            return PartialView("_Create", model);
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

            var model = Mapper.Map<ResponseViewModel>(response);

            return PartialView("_Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ResponseViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = Mapper.Map<Response>(model);

            uow.ResponseRepository.Update(response);
            uow.Commit();

            return PartialView("_Details", model);
        }

        // GET: /Response/Delete/5
        public ActionResult Delete(int? id, string backUrl)
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

            var model = Mapper.Map<ResponseViewModel>(response);

            ViewBag.backUrl = backUrl;

            return View("_Delete", model);
        }

        // POST: /Response/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string backUrl)
        {
            uow.ResponseRepository.Delete(id);
            uow.Commit();
            return BackTo(backUrl);
        }


        public ActionResult BackTo(string backUrl)
        {
            return Redirect(backUrl);
        }
    }
}
