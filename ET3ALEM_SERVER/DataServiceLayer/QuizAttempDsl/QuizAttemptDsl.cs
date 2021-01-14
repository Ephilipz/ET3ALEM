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
    }
}
