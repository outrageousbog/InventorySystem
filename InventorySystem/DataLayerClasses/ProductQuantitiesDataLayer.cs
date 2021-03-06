﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using InventorySystem.Models;

namespace InventorySystem.DataLayerClasses
{
    public class ProductQuantitiesDataLayer
    {
        SqlConnection connection;
        SqlCommand command;
        String connectionString;

        public ProductQuantitiesDataLayer(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("localDB");
            connection = new SqlConnection(connectionString);
        }

        public IEnumerable<ProductQuantityModel> GetQuantities(int? ProductID)
        {
            List<ProductQuantityModel> productQuantites = new List<ProductQuantityModel>();

            if (ProductID == null)
            {
                command = new SqlCommand("SELECT * FROM [ViewProductQuantities]", connection);
            }
            else
            {
                command = new SqlCommand("SELECT * FROM [ViewProductQuantities] WHERE ProductID = '" + ProductID + "'", connection);
            }

            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ProductQuantityModel productQuantityModel = new ProductQuantityModel((int)Convert.ToInt64(reader["ProductID"]),
                        (int)Convert.ToInt64(reader["ProductQuantity"]));
                    productQuantites.Add(productQuantityModel);
                }
            }

            connection.Close();

            return productQuantites;
        }

        public ProductQuantitiesInsert InsertProductQuantitiy(ProductQuantitiesInsert productQuantitiesInsert)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("ProcedureInsertProductsQuantities", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ProductID_Input", productQuantitiesInsert.ProductID);
                    command.Parameters.AddWithValue("@ProductQuantity_Input", productQuantitiesInsert.ProductQuantity);
                    
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                
                return productQuantitiesInsert;
            }

        }

        public ProductQuantitiesUpdate UpdateProductQuantitiy(ProductQuantitiesUpdate productQuantitiesUpdate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("ProcedureUpdateProductsQuantity", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ProductIDUpdate", productQuantitiesUpdate.ProductID);
                    command.Parameters.AddWithValue("@QuantityUpdate", productQuantitiesUpdate.ProductQuantity);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return productQuantitiesUpdate;
            }

        }

        public ProductQuantitiesDelete DeleteProductQuantitiy(ProductQuantitiesDelete productQuantitiesDelete)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("ProcedureDeleteProductsQuantity", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ProductIDDelete", productQuantitiesDelete.ProductID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return productQuantitiesDelete;
            }

        }
    }
}