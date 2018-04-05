using ArtStore.Data;
using ArtStore.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtStore.Controllers
{
    [Route("api/products")]
    public class ProductsController:Controller
    {
        private readonly IArtRepository _repo;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IArtRepository repo,ILogger<ProductsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                return Ok(_repo.GetAllProducts());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Products:{ex}");
                return BadRequest("Failed to get Products");

            }
        }

    }
}
