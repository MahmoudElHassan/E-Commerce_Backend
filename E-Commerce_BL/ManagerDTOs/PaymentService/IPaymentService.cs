using E_Commerce_DAL;

namespace E_Commerce_BL;

public interface IPaymentService
{
    Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
    Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);
    Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
}