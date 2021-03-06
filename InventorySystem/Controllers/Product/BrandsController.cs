﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using InventorySystem.DataLayerClasses;
using InventorySystem.Models;
using System.IO;
using Microsoft.AspNet.OData;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventorySystem.Controllers
{
    [Route("api/product/[controller]")]
    public class BrandsController : Controller
    {
        private readonly IConfiguration _configuration;

        public BrandsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/brands?id
        [HttpGet("{id?}")]
        [EnableQuery()]
        public IEnumerable<ProductBrand> Get(int? id)
        {
            return new ProductBrandsDataLayer(_configuration).GetBrand(id);
        }

        // POST api/brand
        [HttpPost]
        //[Consumes("application/json")]
        public IActionResult Post([FromBody] ProductBrandInsert productBrandInsert)
        {
            return Ok(new ProductBrandsDataLayer(_configuration).InsertProductBrand(productBrandInsert));
        }

        // PUT api/brand/1
        [HttpPut]
        public IActionResult Put([FromBody] ProductBrandsUpdate productBrandsUpdate)
        {
            return Ok(new ProductBrandsDataLayer(_configuration).UpdateProductBrand(productBrandsUpdate));
        }

        // DELETE api/brand/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ProductBrandsDelete productBrandsDelete = new ProductBrandsDelete(id);
            
            return Ok(new ProductBrandsDataLayer(_configuration).DeleteProductBrand(productBrandsDelete));
        }
    }
}
