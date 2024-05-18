using CodeFood.Data;

namespace CodeFood.Service.Interfaces;

public interface IUserService
{
    IEnumerable<User> GetUsers();
    User GetById(Guid id);
    User GetByName(string name);
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(Guid id);
}
