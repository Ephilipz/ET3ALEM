using BusinessEntities.Models;
using DataAccessLayer.QuizDal;
using DataServiceLayer.QuestionDataService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer.QuizDsl
{
    public class QuizDsl : IQuizDsl
    {
        private readonly IQuizDal _IQuizDal;
        private readonly IQuestionDsl _IQuestionDsl;
        public QuizDsl(IQuizDal IQuizDal, IQuestionDsl IQuestionDsl)
        {
            _IQuizDal = IQuizDal;
            _IQuestionDsl = IQuestionDsl;
        }
        public Task<Quiz> GetQuiz(int quizId)
        {
            return _IQuizDal.GetQuiz(quizId);
        }

        public Task<Quiz> InsertQuiz(Quiz quiz)
        {
            foreach (var quizQuestion in quiz.QuizQuestions)
            {
                if (quizQuestion.QuestionId == 0)
                    _IQuestionDsl.InsertQuestion(quizQuestion.Question);
            }
            return _IQuizDal.InsertQuiz(quiz);
        }
    }
}
