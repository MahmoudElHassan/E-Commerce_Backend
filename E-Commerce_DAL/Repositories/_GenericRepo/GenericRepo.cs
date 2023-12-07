namespace E_Commerce_DAL;

public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
{
    #region Field
    private readonly ApplicationDbContext _context;
    #endregion

    #region Ctor
    public GenericRepo(ApplicationDbContext context)
    {
        _context = context;
    }
    #endregion


    #region Method

    public List<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    public TEntity? GetById(int id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public void DeleteById(int id)
    {
        var entityToDelete = GetById(id);
        if (entityToDelete is not null)
        {
            _context.Set<TEntity>().Remove(entityToDelete);
        }
    }


    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    #endregion
}
