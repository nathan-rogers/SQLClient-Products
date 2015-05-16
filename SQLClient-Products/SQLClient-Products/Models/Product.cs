using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLClient_Products.Models
{
    public class Product
    {
        //product ID fields in database
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }

        //not used
        public Product() { }
        public Product(int productID, string name, string description, decimal price)
        {
            this.ProductID = productID;
            this.Name = name;
            this.ProductDescription = description;
            this.Price = price;
        }
    }
}