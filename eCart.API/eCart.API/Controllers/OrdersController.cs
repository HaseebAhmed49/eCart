using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using eCart.API.Data.DTOs.Identity;
using eCart.API.Data.DTOs.Order;
using eCart.API.Data.Errors;
using eCart.API.Data.Extensions;
using eCart.API.Data.Models.OrderAggregate;
using eCart.API.Data.Services.OrderService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCart.API.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;

        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this._orderService = orderService;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var address = _mapper.Map<AddressDTO, Address>(orderDTO.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDTO.DeliveryMethodId, orderDTO.BasketId, address);

            if (order == null) return BadRequest(new ApiResponse(400, "Problem Creating Order"));

            return Ok(order);
        }

    }
}

