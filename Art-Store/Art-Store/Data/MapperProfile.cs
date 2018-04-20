using ArtStore.Data.Entities;
using ArtStore.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtStore.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Order, OrderViewModel>().ForMember(o => o.orderId, ex => ex.MapFrom(o => o.Id)).ReverseMap();
            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
        }
    }
}
