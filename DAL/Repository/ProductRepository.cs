using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.ContrectRepo;
using DAL.DB;
using Models.Models;
namespace DAL.Repository
{
    public class ProductRepository : IRepoProduct
    {
        public void AddProduct(Product product)
        {
            using (var context = new DBContext())
            {
                if (product != null)
                {
                    context.ProductList.Add(product);
                    context.SaveChanges();
                }
            }
        }
        //non user
        public List<Product> GetProductsInCart()
        {
            List<Product> listOfProduct;
            using (var context = new DBContext())
            {
                listOfProduct = context.ProductList.Include("User").Where(a => a.State == State.InCart && a.UserID == null).ToList();
                return listOfProduct;
            }
        }
        //With a user
        public List<Product> GetProductsInCart(int idUser)
        {
            List<Product> listOfProduct;
            using (var context = new DBContext())
            {
                listOfProduct = context.ProductList.Include("User").
                    Where(a => a.State == State.InCart && a.UserID == idUser).ToList();
            }
            return listOfProduct;
        }
        public List<Product> GetProductsAviable()
        {
            List<Product> listOfProduct;
            using (var context = new DBContext())
            {
                listOfProduct = context.ProductList.Include("User").Where(a => a.State == State.Aviable).ToList();
                return listOfProduct;
            }
        }
        //non user
        public void MoveToCart(int id)
        {
            Product product;
            using (var context = new DBContext())
            {
                product = context.ProductList.Where(a => a.ID == id).FirstOrDefault();
                if (product != null)
                {
                    product.State = State.InCart;
                    product.AddedToCart = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        //With a user
        public void MoveToCart(int id, int idUser)
        {
            Product product;
            using (var context = new DBContext())
            {
                product = context.ProductList.Where(a => a.ID == id).FirstOrDefault();
                if (product != null)
                {
                    product.UserID = idUser;
                    product.State = State.InCart;
                    product.AddedToCart = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }
        public bool ProductsSold(int id)
        {
            Product product;
            using (var context = new DBContext())
            {
                product = context.ProductList.Where(a => a.ID == id).FirstOrDefault();
                if (product != null)
                {
                    product.State = State.Sold;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public bool ProductsAviable(int id)
        {
            Product product;
            using (var context = new DBContext())
            {
                product = context.ProductList.Where(a => a.ID == id).FirstOrDefault();
                if (product != null)
                {
                    product.State = State.Aviable;
                    product.UserID = null;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public Product GetAllDetails(int id)
        {
            Product product;
            using (var context = new DBContext())
            {
                product = context.ProductList.Include("User").Where(a => a.ID == id).FirstOrDefault();
                return product;
            }
        }
    
    }
}
