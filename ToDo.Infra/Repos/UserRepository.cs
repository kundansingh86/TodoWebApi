using ToDo.Core.Entities;
using ToDo.Core.Interfaces;

namespace ToDo.Infra.Repos
{
    public class UserRepository : RepositoryBase<Core.Entities.User>, IUserRepository
    {
        public UserRepository(ToDoDbContext toDoDbContext) : base(toDoDbContext)
        {
        }

        public void CreateUser(User user)
        {
            user.CreatedAt = DateTime.UtcNow;
            user.ModifiedAt = DateTime.UtcNow;

            Create(user);
        }

        public User? GetUserByEmailAndPassword(string email, string password)
        {
            return FindByCondition(u => u.Email == email && u.Password == password).SingleOrDefault();
        }
    }
}
