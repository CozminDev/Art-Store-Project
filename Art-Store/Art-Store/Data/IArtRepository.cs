using System.Collections.Generic;
using ArtStore.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ArtStore.Data
{
    public interface IArtRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Order> GetAllOrders();
        bool SaveAll();
        
    }
}