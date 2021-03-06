﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using InventorySystem.Models;

namespace InventorySystem.DataLayerClasses
{
    public class ProductMaterialsOrderDataLayer
    {
        SqlConnection connection;
        SqlCommand command;
        String connectionString;

        public ProductMaterialsOrderDataLayer(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("localDB");
            connection = new SqlConnection(connectionString);
        }

        public IEnumerable<ProductMaterialsOrder> GetProductsFromMaterials(int? ProductMaterialsOrderID)
        {
            List<ProductMaterialsOrder> products = new List<ProductMaterialsOrder>();

            if (ProductMaterialsOrderID == null)
            {
                command = new SqlCommand("SELECT * FROM [ViewProductMaterials]", connection);
            }
            else
            {
                command = new SqlCommand("SELECT * FROM [ViewProductMaterials] WHERE MaterialID = '" + ProductMaterialsOrderID + "'", connection);
            }

            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ProductMaterialsOrder productMaterialsOrder = new ProductMaterialsOrder((int)Convert.ToInt64(reader["ProductID"]),
                       (int)Convert.ToInt64(reader["MaterialID"]));
                    products.Add(productMaterialsOrder);
                }
            }

            connection.Close();

            return products;
        }

        public IEnumerable<ProductMaterialsOrderName> GetMaterialsFromProducts(int? ProductMaterialsOrderID)
        {
            List<ProductMaterialsOrderName> materials = new List<ProductMaterialsOrderName>();

            if (ProductMaterialsOrderID == null)
            {
                command = new SqlCommand("SELECT * FROM [ViewProductMaterialsV2]", connection);
            }
            else
            {
                command = new SqlCommand("SELECT * FROM [ViewProductMaterialsV2] WHERE ProductID = '" + ProductMaterialsOrderID + "'", connection);
            }

            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ProductMaterialsOrderName productMaterialsOrderName = new ProductMaterialsOrderName((String)reader["MaterialName"]);
                    materials.Add(productMaterialsOrderName);
                }
            }

            connection.Close();

            return materials;
        }

        public ProductMaterialsOrderInsert InsertMaterialsOrder(ProductMaterialsOrderInsert productMaterialsOrderInsert)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("ProcedureInsertProductsMaterialsOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ProductMaterialOrderID", productMaterialsOrderInsert.MaterialID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return productMaterialsOrderInsert;
            }

        }

        public ProductMaterialsOrderUpdate UpdateMaterialsOrder(ProductMaterialsOrderUpdate productMaterialsOrderUpdate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("ProcedureUpdateProductsMaterialsOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ProductMaterialOrderIDUpdate", productMaterialsOrderUpdate.MaterialsOrderID);
                    command.Parameters.AddWithValue("@ProductMaterialIDValueUpdate", productMaterialsOrderUpdate.MaterialID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return productMaterialsOrderUpdate;
            }

        }

        public ProductMaterialsOrderDelete DeleteMaterialsOrder(ProductMaterialsOrderDelete productMaterialsOrderDelete)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("ProcedureDeleteProductsMaterialsOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MaterialsOrderIDDelete", productMaterialsOrderDelete.MaterialsOrderID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return productMaterialsOrderDelete;
            }

        }

    }
}
