using DAL.Repository;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Yad2Project.ViewModel;
using DAL.ProductMathods;
namespace Yad2Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult GetProductByDate()
        {
            ProductMethods productMathods = new ProductMethods();
            var productList = productMathods.getProducsListBySort(p => p.Date);
            return PartialView("_ProductPartial", productList);
        }
        public ActionResult GetProductByTitle()
        {
            ProductMethods productMathods = new ProductMethods();
            var productList = productMathods.getProducsListBySort(p => p.Title);
            return PartialView("_ProductPartial", productList);
        }
        public ActionResult GetProductByPrice()
        {
            ProductMethods productMathods = new ProductMethods();
            var productList = productMathods.getProducsListBySort(p => p.Price);
            return PartialView("_ProductPartial", productList);
        }

        public ActionResult GetProductAviable()
        {
            ProductRepository repoProduct = new ProductRepository();
            CheckCart();
            var productList = repoProduct.GetProductsAviable();
            return PartialView("_ProductPartial", productList);
        }
        public void CheckCart()
        {
            string name = CookieHelper.GetUserBycookie(GetCookie());
            ProductRepository productrepo = new ProductRepository();
            UserRepository userRepo = new UserRepository();
            User user = userRepo.GetUser(name);
            List<Product> productlist;
            if (user != null)
                productlist = productrepo.GetProductsInCart(user.ID);
            else
                productlist = productrepo.GetProductsInCart();
            foreach (var item in productlist)
            {
                if (DateTime.Now >= item.AddedToCart.AddMinutes(5))
                    productrepo.ProductsAviable(item.ID);
            }
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