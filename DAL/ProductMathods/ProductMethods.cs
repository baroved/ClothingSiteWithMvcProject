using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using DAL.ContrectRepo;
using Models.Models;
using DAL.Contract;

namespace DAL.ProductMathods
{
   public class ProductMethods: IProduct
    {
        ProductRepository productRepo = new ProductRepository();
        public IEnumerable<Product> getProducsListBySort(Func<Product, object> func)
        {
            return productRepo.GetProductsAviable().OrderBy(func).ToList();
        }
        public void PayForItems()
        {
            foreach (var item in productRepo.GetProductsInCart())
                productRepo.ProductsSold(item.ID);
        }

        public void PayForItems(int id)
        {
            foreach (var item in productRepo.GetProductsInCart(id))
                productRepo.ProductsSold(item.ID);

        }
        //for visitor
        public double Payment()
        {
            double sum = 0;
            foreach (var item in productRepo.GetProductsInCart())
            {
                sum += item.Price;
            }
            return sum;
        }
        //for member
        public double Payment(int id)
        {
            double sum = 0;
            foreach (var item in productRepo.GetProductsInCart(id))
            {
                sum += item.Price;
            }
            return sum * 0.9;
        }
    }
}
