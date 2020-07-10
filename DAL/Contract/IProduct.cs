using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contract
{
   interface IProduct
    {
        IEnumerable<Product> getProducsListBySort(Func<Product, object> func);
        void PayForItems(int id);
        void PayForItems();
        double Payment();
        double Payment(int id);
    }
}
