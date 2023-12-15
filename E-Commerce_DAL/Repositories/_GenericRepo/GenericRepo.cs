using Microsoft.EntityFrameworkCore;

namespace E_Commerce_DAL;

public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : BaseEntity
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

    public virtual async Task<IReadOnlyList<TEntity>> GetAll()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity> GetById(int id)
    {
        var result = await _context.Set<TEntity>().FindAsync(id);
        if (result is null)
        {
            return null;
        }
        return result;
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
            _context.Set<TEntity>().Remove(entityToDelete.Result);
        }
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }


    #endregion
}
