﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL;
using guestNetwork.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using PagedList;

namespace guestNetwork.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IUnitOfWork uow;

        public AdvertisementController(IUnitOfWork uowInstance)
        {
            uow = uowInstance;
        }

        // GET: /Advertisement/
        [Authorize]
        public ActionResult Index(List<AdvertisementViewModel> model, int? page)
        {
            int pageNumber = page ?? GuestNetworkConstants.DefaultPageNumber;

            var advertisements = new List<Advertisement>();

            if (model == null)
            {
                advertisements = uow.UserRepository.Get(Int32.Parse(User.Identity.GetUserId())).Advertisements.ToList();
                
                model = new List<AdvertisementViewModel>();

                foreach (var adv in advertisements)
                {
                    model.Add(new AdvertisementViewModel
                    {
                        Id = adv.Id,
                        Title = adv.Title,
                        MainImagePath = adv.MainImagePath,
                        Content = adv.Content,
                        Type = adv.Type,
                        UserId = adv.UserId,
                        UserName = adv.User.UserName
                    });
                }
            }

            return View(model.ToPagedList(pageNumber, GuestNetworkConstants.PageSize));
        }

        public ActionResult LastAdvertisements()
        {
            var advertisements = uow.AdvertisementRepository.GetAll().OrderByDescending(x => x.Id).ToList().Take(GuestNetworkConstants.IndexPageAdvertisementsCount);

            var model = new List<AdvertisementViewModel>();
            foreach (var adv in advertisements)
            {
                model.Add(new AdvertisementViewModel
                {
                    Id = adv.Id,
                    Title = adv.Title,
                    MainImagePath = adv.MainImagePath,
                    Content = adv.Content,
                    Type = adv.Type,
                    UserId = adv.UserId,
                    UserName = adv.User.UserName
                });
            }

            return PartialView("_LastAdvertisements", model);
        }

        public ActionResult BackTo(string backUrl)
        {
            return Redirect(backUrl);
        }

        public ActionResult ViewAll(string backUrl, bool? onlyActive, AdvertisementViewType? type, int? page)
        {
            int pageNumber = page ?? GuestNetworkConstants.DefaultPageNumber;

            var advertisements = uow.AdvertisementRepository.GetAll().ToList();

            if (onlyActive == true)
            {
                advertisements = advertisements.Where(item => item.Response == null).ToList();
            }

            if (type != null && type != AdvertisementViewType.All)
            {
                advertisements = advertisements.Where(item => item.Type.ToString() == type.ToString()).ToList();
            }

            ViewBag.Url = backUrl;
            ViewBag.onlyActive = onlyActive;
            ViewBag.type = type;

            return View("ViewAll", advertisements.ToPagedList(pageNumber, GuestNetworkConstants.PageSize));
        }

        public ActionResult ShowFilterForm(FilterAdvertisementViewModel model, string backUrl)
        {
            ViewBag.Url = backUrl;

            return PartialView("_FilterForm", model);
        }

        public ActionResult Details(int id, string backUrl)
        {
            var advertisement = uow.AdvertisementRepository.Get(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }

            var model = new AdvertisementViewModel
            {
                Id = advertisement.Id,
                Title = advertisement.Title,
                MainImagePath = advertisement.MainImagePath,
                Content = advertisement.Content,
                Type = advertisement.Type,
                UserId = advertisement.UserId,
                UserName = advertisement.User.UserName
            };

            return View(model);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(AdvertisementViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var advertisement = new Advertisement
            {
                Title = model.Title,
                Content = model.Content,
                Type = model.Type,
                UserId = uow.UserRepository.Get(Int32.Parse(User.Identity.GetUserId())).Id,
            };

            var file = Request.Files["file"];
            var advPath = "";

            if (file != null && file.ContentLength > 0)
            {
                advPath = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(StringResources.ImagesPath), advPath);
                file.SaveAs(path);
            }
            else
            {
                advPath = StringResources.NoImageFile;
            }

            advertisement.MainImagePath = StringResources.ImagesPath + advPath;

            uow.AdvertisementRepository.Insert(advertisement);
            uow.Commit();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Edit(int id, string backUrl)
        {
            Advertisement advertisement = uow.AdvertisementRepository.Get(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }

            ViewBag.BackUrl = backUrl;
            return View(advertisement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Advertisement advertisement, string backUrl)
        {
            if (ModelState.IsValid)
            {
                uow.AdvertisementRepository.Update(advertisement);
                uow.Commit();
                return Redirect(backUrl);
            }

            ViewBag.BackUrl = backUrl;
            return View(advertisement);
        }

        [Authorize]
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            var advertisement = uow.AdvertisementRepository.Get(id);
            var response = advertisement.Response;

            if (response != null)
            {
                uow.ResponseRepository.Delete(response.AdvertisementId);
            }

            var fullImagePath = Server.MapPath(advertisement.MainImagePath);

            if (System.IO.File.Exists(fullImagePath) &&
                advertisement.MainImagePath != StringResources.ImagesPath + StringResources.NoImageFile)
            {
                System.IO.File.Delete(fullImagePath);
            }

            uow.AdvertisementRepository.Delete(id);
            uow.Commit();

            return RedirectToAction("Index");
        }
    }
}
