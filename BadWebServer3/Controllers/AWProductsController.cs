using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BadWebServer.Models;
namespace BadWebServer.Controllers
{
    public class AWProductsController : Controller
    {
        private readonly string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2016;User ID=BadWebServerUser;Password=BadPassword;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [HttpGet]
        public IActionResult Index()
        {
            List<Product> products = new List<Product>();
            //ADO.Net 
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand selectCommand = sqlConnection.CreateCommand())
                {
                    selectCommand.CommandText = "sp_GetProducts";
                    selectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        int idColumn = reader.GetOrdinal("ProductID");
                        int nameColumn = reader.GetOrdinal("Name");
                        int productNumberColumn = reader.GetOrdinal("ProductNumber");
                        int makeFlagColumn = reader.GetOrdinal("MakeFlag");
                        int finishedGoodsFlagColumn = reader.GetOrdinal("FinishedGoodsFlag");
                        int colorFlagColumn = reader.GetOrdinal("Color");

                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductID = reader.GetInt32(idColumn),
                                Name = reader.GetString(nameColumn),
                                ProductNumber = reader.GetString(productNumberColumn),
                                MakeFlag = reader.GetBoolean(makeFlagColumn),
                                FinishedGoodsFlag = reader.GetBoolean(finishedGoodsFlagColumn),
                                Color = reader.IsDBNull(5) ? null : reader.GetString(colorFlagColumn)
                                //I can keep going with this....
                            });
                        }
                    }
                }
                sqlConnection.Close();
            }
            return Json(products);
        }

        [HttpPut]
        public IActionResult Index([FromBody] Product product)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            if (ModelState.IsValid)
            {
                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO Production.Product (Name, ProductNumber, MakeFlag, FinishedGoodsFlag, Color, SafetyStockLevel, ReorderPoint, StandardCost, ListPrice, DaysToManufacture, SellStartDate) VALUES (@name, @productNumber, @makeFlag, @finishedGoodsFlag, @color, @safetyStockLevel, @reorderPoint, @standardCost, @listPrice, @daysToManufacture, @sellStartDate)";

                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@productNumber", product.ProductNumber);
                command.Parameters.AddWithValue("@makeFlag", product.MakeFlag.HasValue && product.MakeFlag.Value ? 1 : 0);
                command.Parameters.AddWithValue("@finishedGoodsFlag", product.FinishedGoodsFlag.HasValue && product.FinishedGoodsFlag.Value ? 1 : 0);
                command.Parameters.AddWithValue("@color", (object)product.Color ?? DBNull.Value);
                command.Parameters.AddWithValue("@safetyStockLevel", product.SafetyStockLevel);
                command.Parameters.AddWithValue("@reorderPoint", product.ReorderPoint);
                command.Parameters.AddWithValue("@standardCost", product.StandardCost);
                command.Parameters.AddWithValue("@listPrice", product.ListPrice);
                command.Parameters.AddWithValue("@daysToManufacture", product.DaysToManufacture);
                command.Parameters.AddWithValue("@sellStartDate", product.SellStartDate);

                int recordsAdded = command.ExecuteNonQuery();

                timer.Stop();
                Console.WriteLine("Insert took {0} milliseconds", timer.ElapsedMilliseconds);
                if (recordsAdded == 0)
                {
                    throw new Exception("Unable to add record");
                }
                connection.Close();

                return Created("/AWProducts", product);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        public IActionResult Index(int id, [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Production.Product SET " +
                    "Name = '" + product.Name + "' WHERE ProductID = " + id;

                int recordsUpdated = command.ExecuteNonQuery();
                if (recordsUpdated == 0)
                {
                    throw new Exception("Unable to update record");
                }
                connection.Close();

                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        public IActionResult Index(int id)
        {

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand deleteCommand = connection.CreateCommand();
            deleteCommand.CommandText = "sp_DeleteProduct";
            deleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@productId", id);

            int recordsDeleted = deleteCommand.ExecuteNonQuery();
            if (recordsDeleted == 0)
            {
                throw new Exception("Unable to delete record");
            }
            connection.Close();

            return Ok();

        }
    }
}
