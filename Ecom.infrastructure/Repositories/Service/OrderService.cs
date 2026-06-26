using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Order;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories.Service
{
    internal class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(AppDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Orders> CreateOrdersAsync(OrderDTO orderDTO, string BuyerEmail)   
        {
            var basket = await _unitOfWork.CustomerBasket.GetBasketAsync(orderDTO.basketId);
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var item in basket.basketItems)
            {
                var Product = await _unitOfWork.productRepositry.GetByIdAsync(item.Id);
                var orderItrem = new OrderItem
                    (Product.Id, item.Image, Product.Name, item.Price, item.Qunatity);
                orderItems.Add(orderItrem);
            }
            var deliveryMethod = await _context.DeliveryMethod.FirstOrDefaultAsync(m=>m.Id == orderDTO.deliveryMethodId);

            var  subTotal = orderItems.Sum(  m => m.Price * m.Quntity);

            var  ship = _mapper.Map<ShippingAddress>(source: orderDTO.shipAddress);

            var  order = new Orders( BuyerEmail, subTotal,  ship,  deliveryMethod, orderItems);

            await _context.Orders.AddAsync(entity: order);
            await _context.SaveChangesAsync();
            await _unitOfWork.CustomerBasket.DeleteBasketAsync(orderDTO.basketId);
            return order;

        }

        public async Task<IReadOnlyList<OrderToReturnDTO>> GetAllOrdersForUserAsync(string BuyerEmail)
        {
            var orders =await _context.Orders.Where(m=>m.BuyerEmail == BuyerEmail)
                .Include(inc=>inc.orderItems).Include(inc=>inc.deliveryMethod)
                .ToListAsync();
            var result= _mapper.Map<IReadOnlyList<OrderToReturnDTO>>( orders);

            return result;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        => await _context.DeliveryMethod.AsNoTracking().ToListAsync();
        public async Task<Orders> GetOrderByIdAsync(int Id, string BuyerEmail)
        {
            var order = await _context.Orders.Where(m => m.Id == Id && m.BuyerEmail == BuyerEmail)
                .Include(inc => inc.orderItems)
                .Include(inc => inc.deliveryMethod)
                .FirstOrDefaultAsync();

            var result = _mapper.Map<OrderToReturnDTO>(order);
            return result;
                
        }
    }
}
