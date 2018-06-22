﻿using AutoStore.Models;
using System.Linq;
using System.Web.Mvc;

namespace AutoStore.Controllers.ClientService
{
    public class GioHangController : Controller
    {
        private DBConnection db = new DBConnection();

        public ActionResult Index()
        {
            string clientuserid = (string)(Session["ClientUserID"]);
            return View(db.GIOHANGs.Include("SANPHAM").Where(a => a.MAKH == clientuserid).ToList());
        }

        public void AddToCart(string idsp)
        {
            string clientuserid = (string)(Session["ClientUserID"]);
            var cartItem = db.GIOHANGs.SingleOrDefault(a => a.MASP == idsp && a.MAKH == clientuserid);
            if (cartItem == null)
            {
                GIOHANG temp = new GIOHANG { CODE = FuncClass.genNextCode(), MAKH = clientuserid, MASP = idsp, SOLUONG = 1 };
                db.GIOHANGs.Add(temp);
            }
            else
            {
                cartItem.SOLUONG = cartItem.SOLUONG + 1;
            }
            // Save changes
            db.SaveChanges();
        }

        public ActionResult cart()
        {
            string clientuserid = (string)(Session["ClientUserID"]);
            return PartialView(db.GIOHANGs.Include("SANPHAM").Where(a => a.MAKH == clientuserid).ToList());
        }

        public void delitem(string idkh, string idsp)
        {
            string clientuserid = (string)(Session["ClientUserID"]);
            var temp = db.GIOHANGs.SingleOrDefault(a => a.MASP == idsp && a.MAKH == clientuserid);
            if (temp != null)
            {
                if (temp.SOLUONG == 1)
                {
                    db.GIOHANGs.Remove(temp);
                }
                if (temp.SOLUONG > 1)
                {
                    temp.SOLUONG = temp.SOLUONG - 1;
                }
            }
            db.SaveChanges();
        }

        public void decrease(int i, string idsp)
        {
            string clientuserid = (string)(Session["ClientUserID"]);
            var cartItem = db.GIOHANGs.SingleOrDefault(a => a.MASP == idsp && a.MAKH == clientuserid);
            if (cartItem != null)
            {
                cartItem.SOLUONG = i;
                db.SaveChanges();
            }
        }
    }
}