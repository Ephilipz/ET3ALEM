using BusinessEntities.Models;
using DataServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_Application.Controllers
{
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
            if (!ModelState.IsValid)
                return BadRequest(quizAttempt);
            await _IQuizAttemptDsl.InsertQuizAttempt(quizAttempt);
            return CreatedAtAction("GetQuiz", new { id = quizAttempt.Id }, quizAttempt);
        }
    }
}
