﻿using ArtStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtStore.Data
{
    public class ArtContext: DbContext
    {
        public ArtContext(DbContextOptions<ArtContext> options):base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<OrderItem> OrdersItems { get; set; }
    }
}
