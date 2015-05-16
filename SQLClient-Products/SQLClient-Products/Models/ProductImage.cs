using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLClient_Products.Models
{

    public class ProductImage
    {
        public int ImageID { get; set; }
        public string ImageURL { get; set; }
        public int ProductID { get; set; }

        public ProductImage() { }

        public ProductImage(int imageID, string url, int productid)
        {
            this.ImageID = imageID;
            this.ImageURL = url;
            this.ProductID = productid;

        }
    }
}