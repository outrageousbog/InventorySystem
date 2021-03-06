﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using InventorySystem.DataLayerClasses;
using InventorySystem.Models;
using Microsoft.AspNet.OData;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventorySystem.Controllers
{
    [Route("api/product/[controller]")]
    public class PricesController : Controller
    {
        private readonly IConfiguration _configuration;

        public PricesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/prices?id
        [HttpGet("{id?}")]
        [EnableQuery()]
        public IEnumerable<ProductPrice> Get(int? id)
        {
            return new ProductPricesDataLayer(_configuration).GetPrice(id);
        }

        // POST api/prices
        [HttpPost]
        //[Consumes("application/json")]
        public IActionResult Post([FromBody] ProductsPriceInsert productsPriceInsert)
        {
            return Ok(new ProductPricesDataLayer(_configuration).InsertProductPrice(productsPriceInsert));
        }

        // PUT api/prices/1
        [HttpPut]
        public IActionResult Put([FromBody] ProductsPriceUpdate productsPriceUpdate)
        {
            return Ok(new ProductPricesDataLayer(_configuration).UpdateProductPrice(productsPriceUpdate));
        }

        // DELETE api/prices/1
        [HttpDelete]
        public IActionResult Delete([FromBody] ProductsPriceDelete productsPriceDelete)
        {
            return Ok(new ProductPricesDataLayer(_configuration).DeleteProductPrice(productsPriceDelete));
        }
    }
}
