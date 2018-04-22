using ArtStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtStore.Data
{
	public class ArtRepository : IArtRepository
	{
		private readonly ArtContext _ctx;
		private readonly ILogger<ArtRepository> _logger;

		public ArtRepository(ArtContext ctx,ILogger<ArtRepository> logger)
		{
			_ctx = ctx;
			_logger = logger;
		}

		public void AddEntity(object obj)
		{
			_ctx.Add(obj);
		}

		public IEnumerable<Order> GetAllOrders(bool includeItems)
		{
			try
			{
				if (includeItems)
				{
					return _ctx.Orders.Include(o => o.Items).ThenInclude(i => i.Product).ToList();
				}
				else return _ctx.Orders.ToList();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all Orders: {ex}");
				return null;

			}
		}

		public Order GetOrderById(string username,int id)
		{
			return _ctx.Orders
				.Include(o => o.Items)
				.ThenInclude(i => i.Product)
				.Where(p=>p.Id==id && p.User.UserName==username)
				.FirstOrDefault();
		}

		public IEnumerable<Product> GetAllProducts()
		{
			try
			{
				return _ctx.Products.ToList();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all products: {ex}");
				return null;
			   
			}
		}
		public bool SaveAll()
		{
			return _ctx.SaveChanges() > 0;
		}

		public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
		{
			if (includeItems)
			{
				return _ctx.Orders
					.Where(o=>o.User.UserName==username)
					.Include(o => o.Items)
					.ThenInclude(i => i.Product)
					.ToList();
			}
			else return _ctx.Orders
					.Where(o => o.User.UserName == username)
					.ToList();
		}

		public void AddOrder(Order newOrder)
		{
			foreach(var item in newOrder.Items)
			{
				item.Product = _ctx.Products.Find(item.Product.Id);
			}

			AddEntity(newOrder);
		}
	}		
}
