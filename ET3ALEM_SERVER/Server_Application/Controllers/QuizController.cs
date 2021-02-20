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
using System.Security.Claims;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Helpers;

namespace Server_Application.Controllers
{
    [Authorize]
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
        [HttpGet("GetBasicQuizByCode/{code}")]
        public async Task<ActionResult<Quiz>> GetBasicQuizByCode(string code)
        {
            return await _IQuizDsl.GetBasicQuizByCode(code);
        }

        [HttpGet("GetFullQuizByCode/{code}")]
        public async Task<ActionResult<Quiz>> GetFullQuizByCode(string code)
        {
            return await _IQuizDsl.GetFullQuizByCode(code);
        }

        [HttpGet]
        public async Task<IEnumerable<Quiz>> GetQuizzes()
        {
            string userId = AccountHelper.getUserId(HttpContext, User);
            return await _IQuizDsl.GetQuizzes(userId);
        }

        // POST: api/Quiz
        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid || string.IsNullOrEmpty(userId))
            {
                return BadRequest(quiz);
            }
            quiz.UserId = userId;
            await _IQuizDsl.InsertQuiz(quiz);
            return CreatedAtAction("GetQuiz", new { id = quiz.Id }, quiz);
        }

        [HttpDelete]
        public async Task<ActionResult<Quiz>> DeleteQuiz(int id)
        {
            var quiz = await _IQuizDsl.DeleteQuiz(id);
            if (quiz == null)
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
