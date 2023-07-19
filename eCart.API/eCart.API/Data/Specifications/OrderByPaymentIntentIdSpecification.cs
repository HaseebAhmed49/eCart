using System.Linq.Expressions;
using eCart.API.Data.Models.OrderAggregate;

namespace eCart.API.Data.Specifications
{
    public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
    {
        public OrderByPaymentIntentIdSpecification(string paymentIntentId)
            : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}

