namespace ToDo.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository User { get; }
        public IToDoRepository ToDo { get; }
        void Save();
    }
}
