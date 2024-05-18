using CodeFood.Data;

namespace CodeFood.Repository.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    IEnumerable<T> GetAllEntities();
    T GetEntity(Guid id);
    void CreateEntity(T entity);
    void DeleteEntity(T entity);
    void UpdateEntity(T entity);
    void SaveChanges();
}
