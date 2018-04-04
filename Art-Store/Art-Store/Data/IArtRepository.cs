using System.Collections.Generic;
using ArtStore.Data.Entities;

namespace ArtStore.Data
{
    public interface IArtRepository
    {
        IEnumerable<Product> GetAllProducts();
        bool SaveAll();
    }
}