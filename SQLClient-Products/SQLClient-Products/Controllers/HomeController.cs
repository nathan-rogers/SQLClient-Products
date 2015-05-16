using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQLClient_Products.Models;

namespace SQLClient_Products.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //testing insert into SQL table
            //ProductRepository.InsertProduct("TV", "Plasma", 200.00m, "www.place.org/pic");

            //testing delete specific entry in SQL table
            //ProductRepository.DeleteProduct(1);

            //testing update entry in SQL table
            //ProductRepository.UpdateProduct(2, "Television", "Widescreen Plasma", 2000.47m, "www.iliketv.com/tvplasma");
            
            //testing return 1 entry by ID number
            //ProductRepository.GetProductById(3);
            ProductRepository.GetAllProducts();
            return View();
        }

        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
    }
}