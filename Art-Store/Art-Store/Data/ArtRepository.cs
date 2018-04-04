using ArtStore.Data.Entities;
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
	}		
}
