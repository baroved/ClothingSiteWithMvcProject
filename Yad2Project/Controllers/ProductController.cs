using DAL;
using DAL.Repository;
using Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Yad2Project.ViewModel;
using DAL.ProductMathods;
namespace Yad2Project.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult GetProductInCart()
        {
            string userName = CookieHelper.GetUserBycookie(GetCookie());
            var productList = new List<Product>();
            UserRepository repoUser = new UserRepository();
            ProductRepository repoProduct = new ProductRepository();
            ProductMethods productMathods = new ProductMethods();
            if (userName == null)
            {
                productList = repoProduct.GetProductsInCart();
                ViewBag.Sum = productMathods.Payment();
                ViewBag.SumMember = productMathods.Payment() * 0.9;
            }
            else
            {
                int idOfUser = repoUser.GetUser(userName).ID;
                productList = repoProduct.GetProductsInCart(idOfUser);
                ViewBag.SumMemberr = productMathods.Payment(idOfUser);
            }
            return View(productList);
        }

        public ActionResult AddProduct()
        {
            return PartialView("AddProduct");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            UserRepository repoUser = new UserRepository();
            ProductRepository repoProduct = new ProductRepository();
            product.AddedToCart = DateTime.Now;
            product.Date = DateTime.Now;
            product.State = State.Aviable;
            product.UserID = null;
            product.OwnerID = repoUser.GetUser(CookieHelper.GetUserBycookie(GetCookie())).ID;
            if (file1 != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file1.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    product.PictureOne = array;
                }
            }
            if (file2 != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file2.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    product.PictureTwo = array;
                }
            }
            if (file3 != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file3.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    product.PictureThree = array;
                }
            }
            if (ModelState.IsValid)
            {
                HttpCookie cookiemsg;
                if (HttpContext.Request.Cookies["message"] == null)
                {
                    cookiemsg = new HttpCookie("message");
                    cookiemsg.Value = "Item has been added";
                    Response.Cookies.Add(cookiemsg);
                    cookiemsg.Expires = DateTime.Now.AddSeconds(2);
                }
                repoProduct.AddProduct(product);
                return RedirectToAction("GetProductAviable", "Home");
            }
            else
                return RedirectToAction("AddProduct");
        }

        public ActionResult AddToCart(int id)
        {
            UserRepository UserRepo = new UserRepository();
            ProductRepository repoProduct = new ProductRepository();
            string userName = CookieHelper.GetUserBycookie(GetCookie());
            if (userName != null)
            {
                User user = UserRepo.GetUser(userName);
                repoProduct.MoveToCart(id, user.ID);
            }
            else
            {
                repoProduct.MoveToCart(id);
            }
            return RedirectToAction("GetProductAviable", "Home");
        }

        public ActionResult GetAllDetails(int id)
        {
            ProductRepository repoProduct = new ProductRepository();
            return View(repoProduct.GetAllDetails(id));
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            ProductRepository repoProduct = new ProductRepository();
            repoProduct.ProductsAviable(id);
            return RedirectToAction("GetProductInCart", "Product");
        }


        //[Authorize]
        public ActionResult PayProducts()
        {
            string name = CookieHelper.GetUserBycookie(GetCookie());
            UserRepository userRepo = new UserRepository();
            User user = userRepo.GetUser(name);
            ProductRepository repoProduct = new ProductRepository();
            ProductMethods productMathods = new ProductMethods();
            HttpCookie cookiemsg;
            if (HttpContext.Request.Cookies["payMessage"] == null)
            {
                cookiemsg = new HttpCookie("payMessage");
                Response.Cookies.Add(cookiemsg);
                cookiemsg.Expires = DateTime.Now.AddSeconds(2);

                if (user != null)
                {
                    cookiemsg.Value = "Items have been purchased for the amount of :" + productMathods.Payment(user.ID).ToString() +"$";
                    productMathods.PayForItems(user.ID);
                }
                else
                {
                    cookiemsg.Value = "Items have been purchased for the amount of :" + productMathods.Payment().ToString() + "$";
                    productMathods.PayForItems();
                }
            }
           
            return RedirectToAction("GetProductAviable", "Home");
        }

        public HttpCookie GetCookie()
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName];
            if (authCookie != null)
                return authCookie;
            return null;
        }
    }

}