﻿using AutoStore.Models;
using System.Linq;
using System.Web.Mvc;

namespace AutoStore.Controllers.ClientService
{
    public class LoginAdminController : Controller
    {
        // GET: LoginAdmin
        private DBConnection db = new DBConnection();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult check()
        {
            string username = Request.Form["username"];
            string pass = Request.Form["pass"];
            var obj = db.NHANVIENs.Where(a => a.USERNAME.Equals(username) && a.PASSWORD.Equals(pass) && a.PHANQUYEN.Equals("AMDIR")).FirstOrDefault();
            if (obj != null)
            {
                Session["UserID"] = obj.MANV.ToString();
                Session["UserName"] = obj.TENNV.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("UserID");
            return RedirectToAction("Index", "Welcome");
        }
    }
}