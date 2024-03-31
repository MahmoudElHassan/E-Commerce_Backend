using E_Commerce_DAL;
using E_Commerce_DAL.OrderAggregate;

namespace E_Commerce_BL;

public class OrderService : IOrderService
{
    private readonly IBasketManager _basketManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentService _paymentService;
    public OrderService(IBasketManager basketRepo, IUnitOfWork unitOfWork, IPaymentService paymentService)
    {
        _unitOfWork = unitOfWork;
        _basketManager = basketRepo;
        _paymentService = paymentService;
    }

    public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
    {
        // get basket from repo
        var basket = await _basketManager.GetBasketAsync(basketId);

        // get items from the product repo
        var items = new List<OrderItem>();
        foreach (var item in basket.Items)
        {
            var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
            var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureURL);
            var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
            items.Add(orderItem);
        }

        // get delivery method from repo
        var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

        // calc subtotal
        var subtotal = items.Sum(item => item.Price * item.Quantity);

        // check to see if order exists
        var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
        var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

        
        if (order != null)
        {
            _unitOfWork.Repository<Order>().Delete(order);
            await _paymentService.CreateOrUpdatePaymentIntent(basket.PaymentIntentId);
        }

        // create order
        order = new Order(items, buyerEmail, shippingAddress, deliveryMethod,
            subtotal, basket.PaymentIntentId);
        _unitOfWork.Repository<Order>().Add(order);

        //if (order != null)
        //{
        //    //order.ShipToAddress = shippingAddress;
        //    //order.DeliveryMethod = deliveryMethod;
        //    //order.Subtotal = subtotal;
        //}
        //else
        //{
        //    // create order
        //    order = new Order(items, buyerEmail, shippingAddress, deliveryMethod,
        //        subtotal, basket.PaymentIntentId);
        //    _unitOfWork.Repository<Order>().Add(order);
        //}

        // save to db
        var result = await _unitOfWork.Complete();

        if (result <= 0) return null;

        // return order
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