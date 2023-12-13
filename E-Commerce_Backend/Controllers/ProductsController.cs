﻿using E_Commerce_BL;
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
    public async Task<ActionResult<List<ReadProductDTO>>> GetAllProduct()
    {
        var result = await _productManager.GetAll();
        return Ok(result);
    }

    // GET: api/GetProductById/5
    [HttpGet("GetProductById/{id}")]
    public async Task<ActionResult<ReadProductDTO>> GetProductById(int id)
    {
        return await _productManager.GetById(id);

        //if (productDTO == null)
        //    return NotFound();

         //productDTO;
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
    public ActionResult EditProduct(UpdateProductDTO productDTO)
    {
        //if (id != transaction.TransactionId)
        //{
        //    return BadRequest();
        //}

        var dbProduct = _productManager.Update(productDTO);

        if (dbProduct)
            return Ok(dbProduct);

        return NotFound();
    }

    // DELETE: api/DeleteProduct/5
    [HttpDelete("DeleteProduct/{id}")]
    public ActionResult DeleteProduct(int id)
    {
        _productManager.Delete(id);

        return NoContent();
    }

    #endregion
}