namespace E_Commerce_DAL;

public interface IGenericRepo<TEntity> where TEntity : class
{
    Task<IReadOnlyList<TEntity>> GetAll();
    Task<TEntity>? GetById(int id);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void DeleteById(int id);
    void SaveChanges();
}
