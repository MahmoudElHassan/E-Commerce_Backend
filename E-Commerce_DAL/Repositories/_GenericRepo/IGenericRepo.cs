namespace E_Commerce_DAL;

public interface IGenericRepo<TEntity> where TEntity : BaseEntity
{
    Task<IReadOnlyList<TEntity>> GetAll();
    Task<TEntity>? GetById(int id);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void DeleteById(int id);
    void SaveChanges();

    Task<IReadOnlyList<TEntity>> ListAllAsync();
    Task<TEntity> GetEntityWithSpec(ISpecification<TEntity> spec);
    Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec);
    Task<int> CountAsync(ISpecification<TEntity> spec);
}
