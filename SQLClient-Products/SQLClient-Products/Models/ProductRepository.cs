using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;

namespace SQLClient_Products.Models
{
    public class ProductRepository
    {
        //TODO: Fill in product data access methods....
        // InsertProduct - inserts a product into the database

        public static bool InsertProduct(string name, string description, decimal price)
        {
            //Insert Product in the database
            //declares variable for scope of code block
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open database connection
                con.Open();
                try
                {

                    //sql call
                    SqlCommand command = new SqlCommand("INSERT INTO Products (Name, ProductDescription, Price) Values(@name, @description, @price)", con);
                    // using parameters avoids sql injections, keep your job
                    command.Parameters.Add(new SqlParameter("name", name));
                    command.Parameters.Add(new SqlParameter("description", description));
                    command.Parameters.Add(new SqlParameter("price", price));
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
        }
        // DeleteProduct - deletes a product in the database 
        public static bool DeleteProduct(int productID)
        {
            //Insert Product in the database
            //declares variable for scope of code block
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open database connection
                con.Open();
                try
                {
                    //sql call
                    SqlCommand command = new SqlCommand("DELETE FROM Products WHERE ProductID = @productID", con);
                    // using parameters avoids sql injections, keep your job
                    command.Parameters.Add(new SqlParameter("productID", productID));
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
        }
        // UpdateProduct - updates a product in the database
        public static bool UpdateProduct(int productID, string name, string description, decimal price)
        {
            //Insert Product in the database
            //declares variable for scope of code block
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open database connection
                con.Open();
                try
                {
                    //sql call
                    SqlCommand command = new SqlCommand("UPDATE Products SET Name = @name, ProductDescription = @description, Price = @price WHERE ProductID = @productID", con);
                    // using parameters avoids sql injections, keep your job

                    command.Parameters.Add(new SqlParameter("productID", productID));
                    command.Parameters.Add(new SqlParameter("name", name));
                    command.Parameters.Add(new SqlParameter("description", description));
                    command.Parameters.Add(new SqlParameter("price", price));
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
        }
        // GetProductById - gets a single product from the database by it's Id

        public static Product GetProductById(int productID)
        {
            Product product = new Product();
            //TODO: SELECT a contact from the database by Id
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open database connection
                con.Open();
                try
                {
                    //sql call
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE ProductID = @productID", con))
                    {

                        command.Parameters.Add(new SqlParameter("productID", productID));
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            decimal price = reader.GetDecimal(3);
                            product = new Product(productID, name, description, price);
                        }
                    }
                    return product;

                }
                catch (Exception)
                {

                    return product;
                }
            }
        }

        // GetAllProducts - returns all products from the database

        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            //TODO: SELECT all contacts from the database
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {

                //open database connection
                con.Open();
                try
                {
                    //sql call
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Products", con))
                    {

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int productID = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            decimal price = reader.GetDecimal(3);
                            products.Add(new Product(productID, name, description, price));
                        }
                    }
                    return products;

                }
                catch (Exception)
                {

                    return products;
                }
            }

        }

        /// <summary>
        /// List of all images in productimages table in database
        /// </summary>
        /// <returns></returns>
        public static List<ProductImage> GetImagesByProductID(int productID)
        {
            List<ProductImage> images = new List<ProductImage>();



            //TODO: SELECT all contacts from the database
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {

                //open database connection
                con.Open();
                try
                {
                    //sql call
                    using (SqlCommand command = new SqlCommand("SELECT * FROM ProductImages WHERE ProductID = @productID", con))
                    {

                        command.Parameters.Add(new SqlParameter("productID", productID));
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int imageID = reader.GetInt32(0);
                            string url = reader.GetString(1);
                            images.Add(new ProductImage(imageID, url, productID));
                        }
                    }
                    return images;

                }
                catch (Exception)
                {

                    return images;
                }
            }

        }
        public static ProductImage GetImageById(int imageID)
        {
            ProductImage image = new ProductImage();
            //TODO: SELECT a contact from the database by Id
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open database connection
                con.Open();
                try
                {
                    //sql call
                    using (SqlCommand command = new SqlCommand("SELECT * FROM ProductImages WHERE ImageID = @imageID", con))
                    {

                        command.Parameters.Add(new SqlParameter("imageID", imageID));
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string imageURL = reader.GetString(1);
                            int productID = reader.GetInt32(2);
                            image = new ProductImage(imageID, imageURL, productID);
                        }
                    }
                    return image;

                }
                catch (Exception)
                {

                    return image;
                }
            }
        }

        public static bool DeleteImage(int imageID)
        {
            //Insert Product in the database
            //declares variable for scope of code block
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open database connection
                con.Open();
                try
                {
                    //sql call
                    SqlCommand command = new SqlCommand("DELETE FROM ProductImages WHERE ImageID = @imageID", con);
                    // using parameters avoids sql injections, keep your job
                    command.Parameters.Add(new SqlParameter("imageID", imageID));
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }

            }
        }

        public static bool InsertImage(string url, int id)
        {
            //Insert Product in the database
            //declares variable for scope of code block
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open database connection
                con.Open();
                try
                {

                    //sql call
                    SqlCommand command = new SqlCommand("INSERT INTO ProductImages (ImageURL, ProductID) Values(@imageURL, @productID)", con);
                    // using parameters avoids sql injections, keep your job
                    command.Parameters.Add(new SqlParameter("imageURL", url));
                    command.Parameters.Add(new SqlParameter("productID", id));
                
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }

            }

        }


        public static bool UpdateImage(int imageID, string URL, int productID)
        {
            //Insert Product in the database
            //declares variable for scope of code block
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open database connection
                con.Open();
                try
                {
                    //sql call
                    SqlCommand command = new SqlCommand("UPDATE ProductImages SET ImageURL = @imageURL, ProductID = @productID WHERE ImageID = @imageID", con);
                    // using parameters avoids sql injections, keep your job

                    command.Parameters.Add(new SqlParameter("imageID", imageID));
                    command.Parameters.Add(new SqlParameter("imageURL", URL));
                    command.Parameters.Add(new SqlParameter("productID", productID));
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
        }
    }
}