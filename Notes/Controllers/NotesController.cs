using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Data;
using Notes.Dtos;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Notes.Controllers
{
    // /Notes
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesRepo _repository;
        private readonly IMapper _mapper;

        public NotesController(INotesRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // /Notes
        [Authorize]
        [HttpGet("Users/{userId}/Notes")]
        public ActionResult<IEnumerable<NoteReadDto>> GetNotes(Guid userId)
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (id != userId.ToString())
            {
                return StatusCode(403);
            };
            var notes = _repository.GetNotes(userId);
            return Ok(_mapper.Map<IEnumerable<NoteReadDto>>(notes));
        }

        [Authorize]
        // /Notes/{id}
        [HttpGet("Users/{userId}/Notes/{noteId}", Name="GetNoteById")]
        public ActionResult<NoteReadDto> GetNoteById(Guid userId, int noteId)
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (id != userId.ToString())
            {
                return StatusCode(403);
            };
            var note = _repository.GetNoteById(noteId);
            if (note != null)
            {
                return Ok(_mapper.Map<NoteReadDto>(note));
            }
            return NotFound();
        }

        // POST /Notes 
        [Authorize]
        [HttpPost("Users/{userId}/Notes")]
        public ActionResult<NoteReadDto> CreateNote(Guid userId, NoteCreateDto noteCreateDto)
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (id != userId.ToString())
            {
                return StatusCode(403);
            };
            noteCreateDto.OwnerId = userId;
            var noteModel = _mapper.Map<Note>(noteCreateDto);
            _repository.CreateNote(noteModel);
            _repository.SaveChanges();

            var noteReadDto = _mapper.Map<NoteReadDto>(noteModel);

            return Ok(noteReadDto);
        }

        [Authorize]
        [HttpPut("Users/Notes/{noteId}")]
        public ActionResult<NoteReadDto> UpdateNote(int noteId, NoteUpdateDto noteUpdateDto)
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var noteModelFromRepo = _repository.GetNoteById(noteId);
            if (noteModelFromRepo == null)
            {
                return NotFound();
            }

            if (id != noteModelFromRepo.OwnerId.ToString())
            {
                return StatusCode(403);
            };

            _mapper.Map(noteUpdateDto, noteModelFromRepo);

            _repository.UpdateNote(noteModelFromRepo);

            _repository.SaveChanges();

            var noteReadDto = _mapper.Map<NoteReadDto>(noteModelFromRepo);

            return Ok(noteReadDto);
        }

        [Authorize]
        [HttpDelete("Users/Notes/{noteId}")]
        public ActionResult<NoteReadDto> DeleteNote(int noteId)
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
           

            var noteModelFromRepo = _repository.GetNoteById(noteId);

            if (id != noteModelFromRepo.OwnerId.ToString())
            {
                return StatusCode(403);
            };


            if (noteModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteNote(noteModelFromRepo);

            _repository.SaveChanges();

            return NoContent();

        }
    }
}
