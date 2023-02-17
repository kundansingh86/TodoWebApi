using ToDo.Core.Interfaces;
using ToDo.Infra;

namespace ToDo.Infra.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToDoDbContext _toDoDbContext;

        private IUserRepository? _userRepo;
        private IToDoRepository? _toDoRepo;

        public UnitOfWork(ToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public IUserRepository User 
        {
            get
            {
                if(_userRepo == null)
                {
                    _userRepo = new UserRepository(_toDoDbContext);
                }

                return _userRepo;
            }
        }

        public IToDoRepository ToDo
        {
            get
            {
                if (_toDoRepo == null)
                {
                    _toDoRepo = new ToDoRepository(_toDoDbContext);
                }

                return _toDoRepo;
            }
        }

        public void Save()
        {
            _toDoDbContext.SaveChanges();
        }
    }
}
