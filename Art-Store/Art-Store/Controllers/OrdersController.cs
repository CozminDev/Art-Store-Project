using ArtStore.Data;
using ArtStore.Data.Entities;
using ArtStore.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController:Controller
    {
        private readonly IArtRepository _repo;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(IArtRepository repo, ILogger<OrdersController> logger,IMapper mapper,UserManager<StoreUser> userManager)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetAllOrders(bool includeItems=true)
        {
            try
            {
                var username = User.Identity.Name;

                var results = _repo.GetAllOrdersByUser(username, includeItems);

                return Ok(_mapper.Map<IEnumerable<Order>,IEnumerable<OrderViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Orders:{ex}");
                return BadRequest("Failed to get Orders");

            }
        }


        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            try
            {
                if (_repo.GetOrderById(User.Identity.Name,id) != null)
                {
                    return Ok(_mapper.Map<Order,OrderViewModel>(_repo.GetOrderById(User.Identity.Name,id)));
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
        public async Task<IActionResult> Post([FromBody] OrderViewModel model)
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

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    newOrder.User = currentUser;

                    _repo.AddOrder(newOrder);


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
