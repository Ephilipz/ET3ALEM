using BusinessEntities.Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    public class QuizAttemptDsl : IQuizAttemptDsl
    {
        private IQuizAttemptDal _IQuizAttemptDal;
        private IQuizDsl _IQuizDsl;
        public QuizAttemptDsl(IQuizAttemptDal IQuizAttemptDal, IQuizDsl IQuizDsl)
        {
            _IQuizAttemptDal = IQuizAttemptDal;
            _IQuizDsl = IQuizDsl;
        }

        public Task<QuizAttempt> GetQuizAttempt(int id)
        {
            return _IQuizAttemptDal.GetQuizAttempt(id);
        }

        public async Task<QuizAttempt> PutQuizAttempt(int id,string userId, QuizAttempt quizAttempt)
        {
            quizAttempt.UserId = userId;
            if (quizAttempt.SubmitTime != null)
                quizAttempt.GradeQuiz();
            return await _IQuizAttemptDal.PutQuizAttempt(id, quizAttempt);
        }


        public async Task<QuizAttempt> InsertQuizAttempt(QuizAttempt quizAttempt)
        {
            var quiz = await _IQuizDsl.GetQuiz(quizAttempt.QuizId);
            foreach (var questionAttempt in quizAttempt.QuestionsAttempts)
            {
                questionAttempt.QuizQuestion = quiz.QuizQuestions.Find(quizQuestion => quizQuestion.Id == questionAttempt.QuizQuestionId);
                questionAttempt.Grade = questionAttempt.GradeQuestion();
            }
            return await _IQuizAttemptDal.InsertQuizAttempt(quizAttempt);
        }
        public Task<List<QuizAttempt>> GetQuizAttemptsForQuiz(int quizId, string userId)
        {
            return _IQuizAttemptDal.GetQuizAttemptsForQuiz(quizId, userId);
        }
    }
}
