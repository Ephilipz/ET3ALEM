using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities.Models;
using DataAccessLayer;

namespace DataServiceLayer
{
    public class QuizDsl : IQuizDsl
    {
        private readonly IQuestionDsl _IQuestionDsl;
        private readonly IQuizAttemptDal _IQuizAttemptDal;
        private readonly IQuizDal _IQuizDal;

        public QuizDsl(IQuizDal IQuizDal, IQuestionDsl IQuestionDsl, IQuizAttemptDal IQuizAttemptDal)
        {
            _IQuizDal = IQuizDal;
            _IQuestionDsl = IQuestionDsl;
            _IQuizAttemptDal = IQuizAttemptDal;
        }

        public Task<Quiz> GetQuiz(int quizId)
        {
            return _IQuizDal.GetQuiz(quizId);
        }

        public Task<string> GetQuizTitleFromCode(string code)
        {
            return _IQuizDal.GetQuizTitleFromCode(code);
        }

        public Task<Quiz> GetSimpleQuiz(int quizId)
        {
            return _IQuizDal.GetSimpleQuiz(quizId);
        }

        public Task<IEnumerable<Quiz>> GetQuizzes(string userId)
        {
            return _IQuizDal.GetQuizzes(userId);
        }

        public Task<Quiz> InsertQuiz(Quiz quiz)
        {
            foreach (var quizQuestion in quiz.QuizQuestions)
                if (quizQuestion.QuestionId <= 0)
                    quizQuestion.QuestionId = _IQuestionDsl.InsertQuestion(quizQuestion.Question).Id;
            return _IQuizDal.InsertQuiz(quiz);
        }

        public async Task<Quiz> DeleteQuiz(int id)
        {
            //remove quiz
            var quiz = await _IQuizDal.DeleteQuiz(id);

            if (quiz != null)
                //remove questions
                foreach (var questionId in quiz.QuizQuestions.Select(quizQuestion => quizQuestion.QuestionId))
                    await _IQuestionDsl.DeleteQuestion(questionId);

            return quiz;
        }

        public async Task PutQuiz(int id, Quiz quiz)
        {
            foreach (var Qquestion in quiz.QuizQuestions)
                if (Qquestion.Question.Id == 0)
                    Qquestion.QuestionId = _IQuestionDsl.InsertQuestion(Qquestion.Question).Id;
                else if (Qquestion.Question.Id < 0)
                    await _IQuestionDsl.DeleteQuestion(Qquestion.Question.Id);
                else
                    await _IQuestionDsl.PutQuestion(Qquestion.Question);
            await _IQuizDal.PutQuiz(id, quiz);
        }

        public Task<Quiz> GetBasicQuizByCode(string code)
        {
            return _IQuizDal.GetBasicQuizByCode(code);
        }

        public Task<Quiz> GetFullQuizByCode(string code)
        {
            return _IQuizDal.GetFullQuizByCode(code);
        }
    }
}