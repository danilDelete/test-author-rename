﻿using System.Web.Mvc;
using BusinessFacade.Repositories;
using CsStat.Web.Helpers;
using CsStat.Web.Models;

namespace CsStat.Web.Controllers
{
    public class SignInController : Controller
    {
        private static UserRegistrationService _registrationService;
        private static IUserRepository _userRepository;

        public SignInController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _registrationService = new UserRegistrationService(_userRepository);
        }
        public ActionResult SignIn()
        {
            return View("~/Views/UsefulInfo/SignInForm.cshtml");
        }

        [HttpPost]
        public ActionResult SignIn(SignInViewModel userModel)
        {
            if (_registrationService.SignIn(userModel))
            {
                Session["IsAdminMode"] = "true";
            }

            return RedirectToAction("Index", "Wiki");
        }

        public ActionResult SignOut()
        {
            Session["IsAdminMode"] = "false";
            return RedirectToAction("Index", "Wiki");
        }
    }
}