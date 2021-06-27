using AutoMapper;
using Notes.Dtos;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Profiles
{
    public class TodosProfile : Profile
    {
        public TodosProfile()
        {
            CreateMap<Todo, TodoReadDto>();
            CreateMap<TodoCreateDto, Todo>();
        }
    }
}
