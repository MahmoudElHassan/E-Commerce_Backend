using E_Commerce_BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Backend;

[Route("api/[controller]")]
[ApiController]
public class ProductBrandController : Controller
{
    #region Field
    private readonly IProductBrandManager _brandManager;
    #endregion

    #region Ctor
    public ProductBrandController(IProductBrandManager brandManager)
    {
        _brandManager = brandManager;
    }
    #endregion

    #region Method
    // GET: api/GetAllBrand
    [HttpGet("GetAllBrand")]
    public async Task<ActionResult<List<ReadProductBrandDTO>>> GetAllBrand()
    {
        var result =  await _brandManager.GetAll();
        return Ok(result);
    }

    // GET: api/GetBrandById/5
    [HttpGet("GetBrandById/{id}")]
    public async Task<ActionResult<ReadProductBrandDTO>> GetBrandById(int id)
    {
        return await _brandManager.GetById(id);
    }


    // POST: api/AddBrand
    [HttpPost("AddBrand")]
    public ActionResult<ReadProductBrandDTO> AddProduct(AddProductBrandDTO brandDTO)
    {
        var readBrandDTO = _brandManager.Add(brandDTO);

        return CreatedAtAction("GetBrandById", new { id = readBrandDTO.Id }, readBrandDTO);
    }

    // PUT: api/EditBrand
    [HttpPut("EditBrand")]
    public ActionResult EditBrand(UpdateProductBrandDTO brandDTO)
    {
        //if (id != transaction.TransactionId)
        //{
        //    return BadRequest();
        //}

        var dbBrand = _brandManager.Update(brandDTO);

        if (dbBrand)
            return Ok(dbBrand);

        return NotFound();
    }

    // DELETE: api/DeleteBrand/5
    [HttpDelete("DeleteBrand/{id}")]
    public ActionResult DeleteBrand(int id)
    {
        _brandManager.Delete(id);

        return NoContent();
    }

    #endregion
}
