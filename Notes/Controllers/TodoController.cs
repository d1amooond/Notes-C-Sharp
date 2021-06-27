using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.Data;
using Notes.Dtos;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Controllers
{
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodosRepo _repository;
        private readonly IMapper _mapper;

        public TodoController(ITodosRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("Notes/{noteId}/Todos/{id}", Name="GetTodoItemById")]
        public ActionResult<TodoReadDto> GetTodoItemById(int noteId, int id)
        {
            Todo todo = _repository.GetTodoItemById(id);

            if (todo != null)
            {
                return Ok(_mapper.Map<TodoReadDto>(todo));
            }
            return NotFound();
        }

        [HttpPost("Notes/{noteId}/Todos")]
        public ActionResult<TodoReadDto> CreateTodo([FromRoute] int noteId, TodoCreateDto todoCreateDto)
        {
            if (todoCreateDto == null)
            {
                return NotFound();
            }

            todoCreateDto.NoteId = noteId;

            var todoModel = _mapper.Map<Todo>(todoCreateDto);
            _repository.CreateTodo(noteId, todoModel);
            _repository.SaveChanges();

            var todoReadDto = _mapper.Map<TodoReadDto>(todoModel);

            return Ok(new { id = todoReadDto.Id } );
        }

        [HttpPut("Todos/{todoId}/Check")]
        public ActionResult<TodoReadDto> CheckTodo([FromRoute] int todoId, TodoCheckDto todoCheckDto)
        {
            if (todoCheckDto == null)
            {
                return NotFound();
            }

            Todo todo = _repository.GetTodoItemById(todoId);
            todo.Checked = todoCheckDto.Checked;

            _repository.UpdateTodo(todo);
            _repository.SaveChanges();

            var todoReadDto = _mapper.Map<TodoReadDto>(todo);

            return Ok(todoReadDto);
        }
    }
}
