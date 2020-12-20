using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessEntities.Models;
using Server_Application.Data;
using DataServiceLayer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Server_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizDsl _IQuizDsl;

        public QuizController(IQuizDsl IQuizDsl)
        {
            _IQuizDsl = IQuizDsl;
        }

        // GET: api/Quiz/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            var quiz = await _IQuizDsl.GetQuiz(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return quiz;
        }

        [HttpGet("GetQuizTitleFromCode/{code}")]
        public async Task<ActionResult<string>> GetQuizTitleFromCode(string code)
        {
            string title = await _IQuizDsl.GetQuizTitleFromCode(code);

            if (string.IsNullOrEmpty(title))
            {
                return NotFound();
            }

            var returnedTitle = new
            {
               title
            };

            return Ok(returnedTitle);
        }

        //GET: api/Quiz
        [HttpGet]
        public async Task<IEnumerable<Quiz>> GetQuizzes(string userId)
        {
            return await _IQuizDsl.GetQuizzes(userId);
        }

        // POST: api/Quiz
        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(quiz);
            }
            await _IQuizDsl.InsertQuiz(quiz);
            return CreatedAtAction("GetQuiz", new { id = quiz.Id }, quiz);
        }

        [HttpDelete]
        public async Task<ActionResult<Quiz>> DeleteQuiz(int id)
        {
            var quiz = await _IQuizDsl.DeleteQuiz(id);
            if(quiz == null)
            {
                return NotFound();
            }
            return quiz;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, Quiz quiz)
        {
            if (id != quiz.Id)
                return BadRequest();

            await _IQuizDsl.PutQuiz(id, quiz);

            return NoContent();
        }
        
    }
}
