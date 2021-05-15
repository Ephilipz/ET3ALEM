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

        [HttpGet]
        public async Task<ActionResult<List<QuizAttempt>>> GetQuizAttempts()
        {
            string userId = AccountHelper.getUserId(HttpContext, User);
            if (string.IsNullOrEmpty(userId))
                return BadRequest();
            return await _IQuizAttemptDsl.GetQuizAttempts(userId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizAttempt>> GetQuizAttempt(int id)
        {
            return await _IQuizAttemptDsl.GetQuizAttempt(id);
        }

        [HttpGet("GetQuizAttemptWithQuiz/{id}")]
        public async Task<ActionResult<QuizAttempt>> GetQuizAttemptWithQuiz(int id)
        {
            return await _IQuizAttemptDsl.GetQuizAttemptWithQuiz(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<QuizAttempt>> PutQuizAttempt(int id, QuizAttempt quizAttempt)
        {
            await _IQuizAttemptDsl.PutQuizAttempt(id, AccountHelper.getUserId(HttpContext, User), quizAttempt);
            return NoContent();
        }

        [HttpPut("UpdateQuizAttemptGrade")]
        public async Task<ActionResult<QuizAttempt>> UpdateQuizAttemptGrade(QuizAttempt quizAttempt)
        {
            string userId = AccountHelper.getUserId(HttpContext, User);
            if(string.IsNullOrEmpty(userId) || userId != quizAttempt.Quiz.UserId)
            {
                return BadRequest();
            }
            await _IQuizAttemptDsl.UpdateQuizAttemptGrade(quizAttempt);
            return NoContent();
        }

        [HttpGet("GetUserQuizAttemptsForQuiz/{quizId}")]
        public async Task<ActionResult<List<QuizAttempt>>> GetUserQuizAttemptsForQuiz(int quizId)
        {
            string userId = AccountHelper.getUserId(HttpContext, User);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(quizId);
            return await _IQuizAttemptDsl.GetUserQuizAttemptsForQuiz(quizId, userId);
        } 
        [HttpGet("GetAllQuizAttemptsForQuiz/{quizId}")]
        public async Task<ActionResult<List<QuizAttempt>>> GetAllQuizAttemptsForQuiz(int quizId)
        {
            return await _IQuizAttemptDsl.GetAllQuizAttemptsForQuiz(quizId);
        }
    }
}
