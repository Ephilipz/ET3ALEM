using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessEntities.Models;
using Server_Application.Data;
using DataServiceLayer.QuizDsl;

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

        // POST: api/Quiz
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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
    }
}
