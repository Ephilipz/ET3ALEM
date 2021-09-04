using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities.Models;
using DataAccessLayer;

namespace DataServiceLayer
{
    public class QuizDsl : IQuizDsl
    {
        private readonly IQuestionDsl _iQuestionDsl;
        private readonly IQuizDal _iQuizDal;

        public QuizDsl(IQuizDal quizDal, IQuestionDsl questionDsl, IQuizAttemptDal quizAttemptDal)
        {
            _iQuizDal = quizDal;
            _iQuestionDsl = questionDsl;
        }

        public Task<Quiz> GetQuiz(int quizId)
        {
            return _iQuizDal.GetQuiz(quizId);
        }

        public Task<string> GetQuizTitleFromCode(string code)
        {
            return _iQuizDal.GetQuizTitleFromCode(code);
        }

        public Task<Quiz> GetSimpleQuiz(int quizId)
        {
            return _iQuizDal.GetSimpleQuiz(quizId);
        }

        public async Task<List<Quiz>> GetQuizzes(string userId)
        {
            return await _iQuizDal.GetQuizzes(userId);
        }

        public async Task<Quiz> InsertQuiz(Quiz quiz)
        {
            foreach (var quizQuestion in quiz.QuizQuestions)
                if (quizQuestion.QuestionId <= 0)
                {
                    Question insertedQuestion = await _iQuestionDsl.InsertQuestion(quizQuestion.Question);
                    quizQuestion.QuestionId = insertedQuestion.Id;
                }

            return await _iQuizDal.InsertQuiz(quiz);
        }

        public async Task<Quiz> DeleteQuiz(int id)
        {
            //remove quiz
            var quiz = await _iQuizDal.DeleteQuiz(id);

            if (quiz != null)
                //remove questions
                foreach (var questionId in quiz.QuizQuestions.Select(quizQuestion => quizQuestion.QuestionId))
                    await _iQuestionDsl.DeleteQuestion(questionId);

            return quiz;
        }

        public async Task PutQuiz(Quiz quiz)
        {
            foreach (var Qquestion in quiz.QuizQuestions)
                if (Qquestion.Question.Id == 0)
                    Qquestion.QuestionId = _iQuestionDsl.InsertQuestion(Qquestion.Question).Id;
                else if (Qquestion.Question.Id < 0)
                    await _iQuestionDsl.DeleteQuestion(Qquestion.Question.Id);
                else
                    await _iQuestionDsl.PutQuestion(Qquestion.Question);
            await _iQuizDal.PutQuiz(quiz);
        }

        public async Task<List<Quiz>> GetUngradedQuizzesForUser(string userId)
        {
            return await _iQuizDal.GetUngradedQuizzesForUser(userId);
        }

        public async Task<Quiz> GetBasicQuizByCode(string code)
        {
            return await _iQuizDal.GetBasicQuizByCode(code);
        }

        public async Task<Quiz> GetFullQuizByCode(string code)
        {
            return await _iQuizDal.GetFullQuizByCode(code);
        }
    }
}