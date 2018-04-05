using ArtStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtStore.Controllers
{
    [Route("api/orders")]
    public class OrdersController:Controller
    {
        private readonly IArtRepository _repo;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IArtRepository repo, ILogger<OrdersController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                return Ok(_repo.GetAllOrders());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Orders:{ex}");
                return BadRequest("Failed to get Orders");

            }
        }
    }
}
