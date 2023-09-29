using E_Commerce_BL;
using E_Commerce_BL.ManagerDTOs;
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
    public ActionResult<IEnumerable<ReadProductBrandDTO>> GetAllBrand()
    {
        return _brandManager.GetAll();
    }

    // GET: api/GetBrandById/5
    [HttpGet("GetBrandById/{id}")]
    public ActionResult<ReadProductBrandDTO> GetBrandById(Guid id)
    {
        var brandDTO = _brandManager.GetById(id);

        if (brandDTO == null)
            return NotFound();

        return brandDTO;
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
    public IActionResult EditBrand(UpdateProductBrandDTO brandDTO)
    {
        //if (id != transaction.TransactionId)
        //{
        //    return BadRequest();
        //}

        var dbBrand = _brandManager.Update(brandDTO);

        if (dbBrand)
            return NoContent();

        return NotFound();
    }

    // DELETE: api/DeleteBrand/5
    [HttpDelete("DeleteBrand/{id}")]
    public async Task<IActionResult> DeleteBrand(Guid id)
    {
        _brandManager.Delete(id);

        return NoContent();
    }

    #endregion
}
