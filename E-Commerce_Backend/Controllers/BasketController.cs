using AutoMapper;
using E_Commerce_BL;
using E_Commerce_DAL;
using Microsoft.AspNetCore.Mvc;

namespace E;

public class BasketController : BaseApiController
{
    private readonly IBasketManager _basketManager;
    private readonly IMapper _mapper;
    public BasketController(IBasketManager basketManager, IMapper mapper)
    {
        _mapper = mapper;
        _basketManager = basketManager;
    }

    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
    {
        var basket = await _basketManager.GetBasketAsync(id);

        return Ok(basket ?? new CustomerBasket(id));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
    {
        //var customerBasket = _mapper.Map<CustomerBasket>(basket);

        var updatedBasket = await _basketManager.UpdateBasketAsync(basket);

        return Ok(updatedBasket);
    }

    [HttpDelete]
    public async Task DeleteBasketAsync(string id)
    {
        await _basketManager.DeleteBasketAsync(id);
    }
}