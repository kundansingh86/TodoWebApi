using ToDo.Core.Entities;

namespace ToDo.Core.Interfaces
{
    public interface IUserRepository : IRepositoryBase<Entities.User>
    {
        void CreateUser(User user);

        User? GetUserByEmailAndPassword(string email, string password);
    }
}
