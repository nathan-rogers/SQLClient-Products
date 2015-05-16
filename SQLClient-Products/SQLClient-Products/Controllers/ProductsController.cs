using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQLClient_Products.Models;
using System.IO;

namespace SQLClient_Products.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            //return a list of products to the view.  The view will display a table of all products, with links to edit or delete the product.
            return View();
        }

        public ActionResult List()
        {
            return View(ProductRepository.GetAllProducts());
        }

        
        //The GET action will take no arguments and pass an empty Product object to the View and display the Create form to the user.  
        //The POST action will accept a Product object as an argument, handle the uploading of an image file, then add it to the database.
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Product());
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
             
                if (ProductRepository.InsertProduct(product.Name, product.ProductDescription, product.Price))
                {
                    ViewBag.Message = "Creation Successful!";
                    return RedirectToAction("List");
                }

                else
                {
                    ViewBag.Error = "Failed to create new product.";

                    return View(new Product());
                }
            }
            catch (Exception)
            {

                ViewBag.Error = "No file was uploaded";
                return View(new Product());
            }
            
            
        }


        
        //The GET action will accept an integer Id as an arguement.  The action will retrieve the product from the database, and pass it to the view to display the Edit form to the user, with the field values populated from the database.
        //The POST action will accept an integer Id and a product object as arguements.  The action will then upload a new file if one was selected, then update the record in the database.
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product product = ProductRepository.GetProductById(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            try
            {               
                if (ProductRepository.UpdateProduct(id, product.Name, product.ProductDescription, product.Price))
                {
                    ViewBag.Message = "Change successful";
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.Error = "Failed to update. Check code.";
                    return View(product);
                }
            }
            catch (Exception)
            {

                ViewBag.Error = "No file was uploaded";
                return View(new Product());
            }
            
        }


        //TODO: Create Delete actin
        //The GET action will accept an integer Id as an arguement and retrieve the product from the database.  The product object will be passed to the view to display to the user a confirmation screen with a button to confirm that links to the DeleteConfirmation action.
        [HttpGet]
        public ActionResult Delete(int id)
        {

            Product product = ProductRepository.GetProductById(id);
            return View(product);
        }

        //TODO: Create DeleteConfirmation action
        //The GET action will accept an integer Id as an arguement and delete the product from the database.  After the deletion is complete, redirect the user to the Index (listing) action.
        [HttpGet]
        public ActionResult DeleteConfirmation(int id)
        {
            if (ProductRepository.DeleteProduct(id))
            {
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Error = "Failed to retrieve contact.";
                return RedirectToAction("List");
            }
            
        }



    }
}