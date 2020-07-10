using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
namespace DAL.ContrectRepo
{
    interface IRepoProduct
    {
        List<Product> GetProductsInCart();
        List<Product> GetProductsAviable();
        List<Product> GetProductsInCart(int idUser);
        void AddProduct(Product product);
        void MoveToCart(int id, int idUser);
        void MoveToCart(int id);
        bool ProductsSold(int id);
        bool ProductsAviable(int id);
        Product GetAllDetails(int id);

    }
}
