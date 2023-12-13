using E_Commerce_BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce_Backend;

[Route("api/[controller]")]
[ApiController]
public class ProductTypeController : ControllerBase
{
    #region Field
    private readonly IProductTypeManager _typeManager;
    #endregion

    #region Ctor
    public ProductTypeController(IProductTypeManager typeManager)
    {
        _typeManager = typeManager;
    }
    #endregion

    #region Method
    // GET: api/GetAllType
    [HttpGet("GetAllType")]
    public async Task<ActionResult<List<ReadProductTypeDTO>>> GetAllType()
    {
        var result = await _typeManager.GetAll();
        return Ok(result);
    }

    // GET: api/GetTypeById/5
    [HttpGet("GetTypeById/{id}")]
    public async Task<ActionResult<ReadProductTypeDTO>> GetTypeById(int id)
    {
        return await _typeManager.GetById(id);
    }


    // POST: api/AddType
    [HttpPost("AddType")]
    public ActionResult<ReadProductTypeDTO> AddProduct(AddProductTypeDTO typeDTO)
    {
        var readTypeDTO = _typeManager.Add(typeDTO);

        return CreatedAtAction("GetTypeById", new { id = readTypeDTO.Id }, readTypeDTO);
    }

    // PUT: api/EditBrand
    [HttpPut("EditType")]
    public ActionResult EditType(UpdateProductTypeDTO typeDTO)
    {
        //if (id != transaction.TransactionId)
        //{
        //    return BadRequest();
        //}

        var dbType = _typeManager.Update(typeDTO);

        if (dbType)
            return Ok(dbType);

        return NotFound();
    }

    // DELETE: api/DeleteType/5
    [HttpDelete("DeleteType/{id}")]
    public ActionResult DeleteType(int id)
    {
        _typeManager.Delete(id);

        return NoContent();
    }

    #endregion
}
