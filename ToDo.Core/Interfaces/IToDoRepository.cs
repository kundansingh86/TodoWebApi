using ToDo.Core.Helpers;
using ToDo.Core.Specifications;

namespace ToDo.Core.Interfaces
{
    public interface IToDoRepository : IRepositoryBase<Entities.ToDo>
    {
        PageList<Entities.ToDo> GetToDos(ToDoSearchSpecs specs);
        PageList<Entities.ToDo> GetToDosByUser(int userId, ToDoSearchSpecs specs);
        List<Entities.ToDo> GetAllToDos();
        Entities.ToDo? GetToDoById(int id);
        void CreateToDo(Entities.ToDo todo);
        void UpdateToDo(Entities.ToDo todo);
        void DeleteToDo(Entities.ToDo todo);
    }
}
