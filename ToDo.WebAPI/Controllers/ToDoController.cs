using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ToDo.Core.Interfaces;
using ToDo.Core.Specifications;
using ToDo.WebAPI.DTOs;
using ToDo.WebAPI.Models;

namespace ToDo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ToDoController(ILogger<ToDoController> logger
            , IUnitOfWork unitOfWork
            , IMapper mapper
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllToDos([FromQuery] ToDoSearchSpecs specs)
        {
            try
            {
                var authUser = new AuthUser(User);

                var todos = _unitOfWork.ToDo.GetToDosByUser(authUser.Id, specs);

                _logger.LogInformation($"Returned todos for the user: {authUser.Name} from database");

                var pageMetaData = new
                {
                    todos.CurrentPage,
                    todos.TotalPages,
                    todos.TotalCount,
                    todos.HasNext,
                    todos.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pageMetaData));

                var todosDto = _mapper.Map<List<ToDoDto>>(todos);

                return Ok(todosDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllToDos action: {ex.GetBaseException().Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}", Name = "GetToDoById")]
        public IActionResult GetToDoById(int id)
        {
            try
            {
                var todo = _unitOfWork.ToDo.GetToDoById(id);

                _logger.LogInformation("Returned todo from database");

                if (todo == null)
                {
                    return NotFound();
                }

                var todoDto = _mapper.Map<ToDoDto>(todo);

                return Ok(todoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetToDoById action: {ex.GetBaseException().Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPost]
        public IActionResult CreateToDo(ToDoForCreateUpdateDto model)
        {
            try
            {
              
                var authUser = new AuthUser(User);

                var toDoEntity = _mapper.Map<Core.Entities.ToDo>(model);

                toDoEntity.UserId = authUser.Id;

                _unitOfWork.ToDo.CreateToDo(toDoEntity);
                _unitOfWork.Save();

                var todoDto = _mapper.Map<ToDoDto>(toDoEntity);

                return CreatedAtRoute("GetToDoById", new { id = toDoEntity.Id }, todoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateToDo action: {ex.GetBaseException().Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateToDo(int id, ToDoForCreateUpdateDto model)
        {
            try
            {
                var todoEntity = _unitOfWork.ToDo.GetToDoById(id);

                if (todoEntity == null)
                {
                    _logger.LogError($"To Do with id: {id} hasn't been found in db.");
                    return NotFound();
                }

                var authUser = new AuthUser(User);

                if(todoEntity.UserId != authUser.Id)
                {
                    _logger.LogError($"Todo with id: {todoEntity.Id}, isn't belong to the this user {authUser.Id}");
                    return Unauthorized();
                }

                _mapper.Map(model, todoEntity);

                _unitOfWork.ToDo.UpdateToDo(todoEntity);
                _unitOfWork.Save();

                var updatedToDo = _mapper.Map<ToDoDto>(todoEntity);

                return Ok(updatedToDo);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateToDo action: {ex.GetBaseException().Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult CompleteToDo(int id)
        {
            try
            {
                var todoEntity = _unitOfWork.ToDo.GetToDoById(id);

                if (todoEntity == null)
                {
                    _logger.LogError($"To Do with id: {id} hasn't been found in db.");
                    return NotFound();
                }

                var authUser = new AuthUser(User);

                if (todoEntity.UserId != authUser.Id)
                {
                    _logger.LogError($"Todo with id: {todoEntity.Id}, isn't belong to the this user {authUser.Id}");
                    return Unauthorized();
                }

                todoEntity.Status = 2;

                _unitOfWork.ToDo.UpdateToDo(todoEntity);
                _unitOfWork.Save();

                var updatedToDo = _mapper.Map<ToDoDto>(todoEntity);

                return Ok(updatedToDo);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CompleteToDo action: {ex.GetBaseException().Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(int id)
        {
            try
            {
               
                var todoEntity = _unitOfWork.ToDo.GetToDoById(id);

                if (todoEntity == null)
                {
                    _logger.LogError($"To Do with id: {id} hasn't been found in db.");
                    return NotFound();
                }

                var authUser = new AuthUser(User);

                if (todoEntity.UserId != authUser.Id)
                {
                    _logger.LogError($"Todo with id: {todoEntity.Id}, isn't belong to the this user {authUser.Id}");
                    return Unauthorized();
                }

                _unitOfWork.ToDo.DeleteToDo(todoEntity);
                _unitOfWork.Save();

                return Ok("Todo has been deleted successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteToDo action: {ex.GetBaseException().Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}



