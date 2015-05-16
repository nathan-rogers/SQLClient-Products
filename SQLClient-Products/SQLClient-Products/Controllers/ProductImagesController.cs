using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQLClient_Products.Models;
using System.IO;
namespace SQLClient_Products.Controllers
{
    public class ProductImagesController : Controller
    {
        // GET: ProductImages
        [HttpGet]
        public ActionResult List(int id)
        {
            ViewBag.ProductID = id;
            return View(ProductRepository.GetImagesByProductID(id));
        }
        /// <summary>
        /// Select image for deletion. Direct to Delete View
        /// </summary>
        /// <param name="imageID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int imageID)
        {


            return View(ProductRepository.GetImageById(imageID));
        }


        /// <summary>
        /// This takes an additional arguement, productID = id.
        /// 
        /// The reason for this, it is impossible to return to the correct image view list
        /// without the product ID being passed in ADDITION to the image ID. 
        /// 
        /// Image ID deletes the correct image, and id is used to redirect to correct product ID image list. 
        /// </summary>
        /// <param name="imageID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteConfirmation(int imageID, int id)
        {
            if (ProductRepository.DeleteImage(imageID))
            {
                return RedirectToAction("List", new { id = id });
            }
            else
            {
                ViewBag.Error = "Failed to retrieve contact.";
                return RedirectToAction("List", new { id = id });
            }

        }
        /// <summary>
        /// create a new image for a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create(int id)
        {
            ViewBag.ProductID = id;
            return View(new ProductImage());
        }
        /// <summary>
        /// Uploads and save the image
        /// </summary>
        /// <param name="image"></param>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(ProductImage image, int id, HttpPostedFileBase file)
        {
            try
            {
                //upload file, generate unique identifier for image
                var filename = Guid.NewGuid().ToString().Substring(0, 5) + "_" + Path.GetFileName(file.FileName);
                var filenamePath = Path.Combine(Server.MapPath("~/Content/Uploads"), filename);
                file.SaveAs(filenamePath);//saves file
                image.ImageURL = filename;
                image.ProductID = id;
                if (ProductRepository.InsertImage(image.ImageURL, image.ProductID))
                {
                    ViewBag.Reward = "Creation Successful!";
                    return RedirectToAction("List", new { id = image.ProductID });
                }

                else
                {
                    ViewBag.Error = "Failed to create new product.";

                    return View(new ProductImage());
                }
            }
            catch (Exception e)
            {

                ViewBag.Error = "No file was uploaded" + e;
                return View(new ProductImage());
            }


        }
        /// <summary>
        /// update the image, keeping the same image id associated
        /// </summary>
        /// <param name="imageID"></param>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateImage(int imageID, int id, HttpPostedFileBase file)
        {
            try
            {
                var filename = Guid.NewGuid().ToString().Substring(0, 5) + "_" + Path.GetFileName(file.FileName);
                var filenamePath = Path.Combine(Server.MapPath("~/Content/Uploads"), filename);
                file.SaveAs(filenamePath);//saves file

                if (ProductRepository.UpdateImage(imageID, filename, id))
                {
                    ViewBag.Message = "Change successful";
                    return RedirectToAction("List", new { id = id });
                }
                else
                {
                    ViewBag.Error = "Failed to update. Check code.";
                    return View();
                }
            }
            catch (Exception)
            {

                ViewBag.Error = "No file was uploaded";
                return View(new Product());
            }


        }

    }

}