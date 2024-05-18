using CodeFood.Data;
using CodeFood.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeFood.Repository.EntityFramework
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _entities;

        public EFRepository(AppDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAllEntities()
        {
            return _entities.AsEnumerable();
        }

        public T GetEntity(Guid id)
        {
            return _entities.FirstOrDefault(e => e.Id == id)!;
        }

        public void CreateEntity(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateEntity(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity.Id != default)
                _context.Entry(entity).State = EntityState.Modified;

            SaveChanges();
        }

        public void DeleteEntity(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
