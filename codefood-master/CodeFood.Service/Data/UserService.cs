using CodeFood.Data;
using CodeFood.Repository.Interfaces;
using CodeFood.Service.Interfaces;

namespace CodeFood.Service.Data;

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;

    public UserService(IRepository<User> repository)
    {
        _repository = repository;
    }

    public IEnumerable<User> GetUsers()
    {
        return _repository.GetAllEntities();
    }

    public User GetById(Guid id)
    {
        return _repository.GetEntity(id);
    }

    public User GetByName(string name)
    {
        return GetUsers().FirstOrDefault(u => u.UserName == name)!;
    }

    public void CreateUser(User user)
    {
        _repository.CreateEntity(user);
    }

    public void UpdateUser(User user)
    {
        _repository.UpdateEntity(user);
    }

    public void DeleteUser(Guid id)
    {
        var user = _repository.GetEntity(id);
        _repository.DeleteEntity(user);
    }
}
