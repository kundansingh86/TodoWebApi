using ToDo.Core.Helpers;
using ToDo.Core.Interfaces;
using ToDo.Core.Specifications;
using ToDo.Infra;
using ToDo.Infra.Helpers;

namespace ToDo.Infra.Repos
{
    public class ToDoRepository : RepositoryBase<Core.Entities.ToDo>, IToDoRepository
    {
        public ToDoRepository(ToDoDbContext toDoDbContext) : base(toDoDbContext)
        {
        }

        public void CreateToDo(Core.Entities.ToDo todo)
        {
            todo.Status = 1;
            todo.CreatedAt = DateTime.Now;
            todo.ModifiedAt = DateTime.Now;

            Create(todo);
        }

        public void DeleteToDo(Core.Entities.ToDo todo)
        {
            Delete(todo);
        }

        public List<Core.Entities.ToDo> GetAllToDos()
        {
            return FindAll().ToList();
        }

        public Core.Entities.ToDo? GetToDoById(int id)
        {
            return FindByCondition(t => t.Id == id).SingleOrDefault();
        }

        public PageList<Core.Entities.ToDo> GetToDos(ToDoSearchSpecs specs)
        {
            var todos = FindAll();

            if(specs.Status > 0)
            {
                todos = FindByCondition(t => t.Status == specs.Status);
            }

            if(!string.IsNullOrWhiteSpace(specs.Title))
            {
                todos = todos.Where(t => t.Title.ToLower().Contains(specs.Title.ToLower()));
            }

            todos = SortHelper<Core.Entities.ToDo>.ApplySort(todos, specs.OrderBy);

            return PageList<Core.Entities.ToDo>.ToPageList(todos, specs.PageNumber, specs.PageSize);
        }

        public PageList<Core.Entities.ToDo> GetToDosByUser(int userId, ToDoSearchSpecs specs)
        {
            var todos = FindByCondition(t => t.UserId == userId);

            if (specs.Status > 0)
            {
                todos = todos.Where(t => t.Status == specs.Status);
            }

            if (!string.IsNullOrWhiteSpace(specs.Title))
            {
                todos = todos.Where(t => t.Title.ToLower().Contains(specs.Title.ToLower()));
            }

            todos = SortHelper<Core.Entities.ToDo>.ApplySort(todos, specs.OrderBy);

            return PageList<Core.Entities.ToDo>.ToPageList(todos, specs.PageNumber, specs.PageSize);
        }

        public void UpdateToDo(Core.Entities.ToDo todo)
        {
            todo.ModifiedAt = DateTime.Now;

            Update(todo);
        }
    }
}
