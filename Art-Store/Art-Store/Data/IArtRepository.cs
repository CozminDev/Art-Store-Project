using System.Collections.Generic;
using ArtStore.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ArtStore.Data
{
    public interface IArtRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Order> GetAllOrders(bool includeItems);
        bool SaveAll();
        Order GetOrderById(string username,int id);
        void AddEntity(object obj);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
        void AddOrder(Order newOrder);
    }
}