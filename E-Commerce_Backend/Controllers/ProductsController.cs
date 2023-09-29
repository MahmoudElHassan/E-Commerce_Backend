using E_Commerce_BL;
using E_Commerce_DAL;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Backend;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    #region Field
    private readonly IProductManager _productManager;
    #endregion

    #region Ctor
    public ProductsController(IProductManager productManager)
    {
        _productManager = productManager;
    }
    #endregion

    #region Method
    // GET: api/GetAllProduct
    [HttpGet("GetAllProduct")]
    public ActionResult<IEnumerable<ReadProductDTO>> GetAllProduct()
    {
        return _productManager.GetAll();
    }

    // GET: api/GetProductById/5
    [HttpGet("GetProductById/{id}")]
    public ActionResult<ReadProductDTO> GetProductById(Guid id)
    {
        var productDTO = _productManager.GetById(id);

        if (productDTO == null)
            return NotFound();

        return productDTO;
    }


    // POST: api/AddProduct
    [HttpPost("AddProduct")]
    public ActionResult<ReadProductDTO> AddProduct(AddProductDTO productDTO)
    {
        var readProductDTO = _productManager.Add(productDTO);

        return CreatedAtAction("GetProductById", new { id = readProductDTO.Id }, readProductDTO);
    }

    // PUT: api/EditProduct
    [HttpPut("EditProduct")]
    public IActionResult EditProduct(UpdateProductDTO productDTO)
    {
        //if (id != transaction.TransactionId)
        //{
        //    return BadRequest();
        //}

        var dbProduct = _productManager.Update(productDTO);

        if (dbProduct)
            return NoContent();

        return NotFound();
    }

    // DELETE: api/DeleteProduct/5
    [HttpDelete("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        _productManager.Delete(id);

        return NoContent();
    }

    #endregion
}
