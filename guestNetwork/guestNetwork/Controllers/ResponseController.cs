using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DAL;
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

        public ActionResult ViewForAdvertisement(int id)
        {
            var response = uow.ResponseRepository.Get(id);

            if(response == null)
                return PartialView("_ViewForAdvertisement", id);

            var model = new ResponseViewModel()
            {
                AdvertisementId = response.AdvertisementId,
                UserId = response.UserId,
                Message = response.Message,
                UserName = response.User.UserName
            };

            return PartialView("_Details", model);
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

            var model = new ResponseViewModel
            {
                AdvertisementId = response.AdvertisementId,
                UserId = response.UserId,
                Message = response.Message,
               // UserName = uow.ResponseRepository.GetAll().Include("User").SingleOrDefault(x => x.AdvertisementId == response.AdvertisementId).User.UserName;
            };

            return PartialView("_Details", model);
        }

        [Authorize]
        public ActionResult Create(int id)
        {
            var user = uow.UserRepository.Get(Int32.Parse(User.Identity.GetUserId()));
            var model = new ResponseViewModel
            {
                AdvertisementId = id,
                UserId = user.Id,
                UserName = user.UserName,
                Message = ""
            };
            return PartialView("_Create", model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ResponseViewModel responseModel)
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

            var model = new ResponseViewModel
            {
                AdvertisementId = response.AdvertisementId,
                UserId = response.UserId,
                UserName = response.User.UserName,
                Message = response.Message
            };
            return PartialView("_Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ResponseViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = new Response
            {
                AdvertisementId = model.AdvertisementId,
                UserId = model.UserId,
                Message = model.Message,
            };

            uow.ResponseRepository.Update(response);
            uow.Commit();

            //response = uow.ResponseRepository.GetAll().Include("User").SingleOrDefault(x => x.AdvertisementId == response.AdvertisementId);

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
            uow.Commit();
            return RedirectToAction("Details", "Advertisement", new { id = id });
        }
    }
}
