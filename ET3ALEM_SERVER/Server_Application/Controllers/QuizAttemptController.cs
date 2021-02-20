using BusinessEntities.Models;
using DataServiceLayer;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuizAttemptController : ControllerBase
    {

        private IQuizAttemptDsl _IQuizAttemptDsl;
        public QuizAttemptController(IQuizAttemptDsl IQuizAttemptDsl)
        {
            _IQuizAttemptDsl = IQuizAttemptDsl;
        }
        // POST: api/QuizAttempt
        [HttpPost]
        public async Task<ActionResult<QuizAttempt>> PostQuizAttempt(QuizAttempt quizAttempt)
        {
            string userId = AccountHelper.getUserId(HttpContext, User);
            if (!ModelState.IsValid || string.IsNullOrEmpty(userId))
                return BadRequest(quizAttempt);
            quizAttempt.UserId = userId;
            await _IQuizAttemptDsl.InsertQuizAttempt(quizAttempt);
            return CreatedAtAction("GetQuizAttempt", new { id = quizAttempt.Id }, quizAttempt);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizAttempt>> GetQuizAttempt(int id)
        {
            return await _IQuizAttemptDsl.GetQuizAttempt(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<QuizAttempt>> PutQuizAttempt(int id, QuizAttempt quizAttempt)
        {
            await _IQuizAttemptDsl.PutQuizAttempt(id, quizAttempt);
            return NoContent();
        }

        [HttpGet("GetQuizAttemptsForQuiz/{quizId}")]
        public async Task<ActionResult<List<QuizAttempt>>> GetQuizAttemptsForQuiz(int quizId)
        {
            string userId = AccountHelper.getUserId(HttpContext, User);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(quizId);
            return await _IQuizAttemptDsl.GetQuizAttemptsForQuiz(quizId, userId);
        }
    }
}
