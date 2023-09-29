namespace E_Commerce_DAL;

public class ProductRepo : GenericRepo<Product>, IProductRepo
{
    #region Field
    private readonly ApplicationDbContext _context;
    #endregion

    #region Ctor
    public ProductRepo(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    #endregion
}
