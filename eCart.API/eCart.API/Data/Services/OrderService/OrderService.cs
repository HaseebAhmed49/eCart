
using System;
using eCart.API.Data.Models.OrderAggregate;
using eCart.API.Data.Services.Basket;
using eCart.API.Data.Services.UoW;
using eCart.API.Data.Specifications;
using eCart.API.Models;
using eCart.API.Services;

namespace eCart.API.Data.Services.OrderService
{
	public class OrderService: IOrderService
	{

        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            this._basketRepo = basketRepo;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // Get Basket from the Repo

            var basket = await _basketRepo.GetBasketAsync(basketId);

            // Get Items from Product Repo

            var items = new List<OrderItem>();
            foreach(var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);

                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            // Get DeliveryMethods from Repo

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // Calculate subtotal

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            // Check to see if order exists

            var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if(order != null)
            {
                order.ShipToAddress = shippingAddress;
                order.DeliveryMethod = deliveryMethod;
                order.Subtotal = subtotal;
                _unitOfWork.Repository<Order>().Update(order);
            }
            else
            {
                // Create Order

                order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal,
                    basket.PaymentIntentId);
                _unitOfWork.Repository<Order>().Add(order);
            }


            // Save to DB
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // Return Order

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}

