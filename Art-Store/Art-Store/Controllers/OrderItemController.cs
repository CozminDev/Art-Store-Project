using ArtStore.Data;
using ArtStore.Data.Entities;
using ArtStore.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtStore.Controllers
{
    [Route("/api/orders/{orderId}/items")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemController : Controller
    {
        private readonly IArtRepository _repository;
        private readonly ILogger<OrderItemController> _logger;
        private readonly IMapper _mapper;

        public OrderItemController(IArtRepository repository, ILogger<OrderItemController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetOrderItem(int orderId)
        {
            var order = _repository.GetOrderById(User.Identity.Name,orderId);
            if (order != null) return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderItemById(int orderId, int id)
        {
            var order = _repository.GetOrderById(User.Identity.Name,orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                if (item != null)
                {
                    return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
                }
            }
            return NotFound();

        }

    }
}
