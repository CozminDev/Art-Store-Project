using ArtStore.Data;
using ArtStore.Data.Entities;
using ArtStore.ViewModels;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public OrdersController(IArtRepository repo, ILogger<OrdersController> logger,IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Order>,IEnumerable<OrderViewModel>>(_repo.GetAllOrders()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Orders:{ex}");
                return BadRequest("Failed to get Orders");

            }
        }
        [HttpGet("{id}")]
        public IActionResult GetAllOrdersById(int id)
        {
            try
            {
                if (_repo.GetOrderById(id) != null)
                {
                    return Ok(_mapper.Map<Order,OrderViewModel>(_repo.GetOrderById(id)));
                }
                else return NotFound();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Orders:{ex}");
                return BadRequest("Failed to get Orders");

            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);
                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }
                    _repo.AddEntity(newOrder);
                    if (_repo.SaveAll())
                    {
                        var vm = _mapper.Map<Order, OrderViewModel>(newOrder);
                        return Created($"api/orders/{vm.orderId}", vm);
                    }
                }
                else return BadRequest(ModelState);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to post Order:{ex}");
                
            }
            return BadRequest("Failed to post Order");
        }
    }
}
