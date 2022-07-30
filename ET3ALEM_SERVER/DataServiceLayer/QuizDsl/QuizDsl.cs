using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessEntities.Models;
using BusinessEntities.ViewModels;
using DataAccessLayer;

namespace DataServiceLayer
{
    public class QuizDsl : IQuizDsl
    {
        private readonly IQuestionDsl _iQuestionDsl;
        private readonly IQuizDal _iQuizDal;
        private readonly IMapper _iMapper;

        public QuizDsl(IQuizDal quizDal, IQuestionDsl questionDsl, IMapper iMapper)
        {
            _iQuizDal = quizDal;
            _iQuestionDsl = questionDsl;
            _iMapper = iMapper;
        }

        public Task<Quiz> GetQuiz(int quizId)
        {
            return _iQuizDal.GetQuiz(quizId);
        }

        public async Task<string> GetQuizTitleFromCode(string code)
        {
            return await _iQuizDal.GetQuizTitleFromCode(code);
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
            var questions = quiz.QuizQuestions.Select(qQ => qQ.Question);
            await _iQuestionDsl.InsertQuestions(questions);
            return await _iQuizDal.InsertQuiz(quiz);
        }

        public async Task<Quiz> DeleteQuiz(int id)
        {
            var quiz = await _iQuizDal.DeleteQuiz(id);

            if (quiz == null) return null;

            var questionIds = quiz.QuizQuestions.Select(quizQuestion => quizQuestion.QuestionId);

            await _iQuestionDsl.DeleteQuestions(questionIds);

            return quiz;
        }

        public async Task PutQuiz(Quiz quiz)
        {
            var questions = quiz.QuizQuestions.Select(quizQuestion => quizQuestion.Question).ToList();
            await _iQuestionDsl.UpdateQuestionsBasedOnId(questions);
            await _iQuizDal.PutQuiz(quiz);
        }

        public async Task<List<UngradedQuizTableVM>> GetUngradedQuizzesForUser(string userId)
        {
            var ungradedAttempts = await _iQuizDal.GetUngradedQuizzesForUser(userId);

            return ungradedAttempts
                .GroupBy(attempt => attempt.Quiz)
                .Select(attemptGrouping => new UngradedQuizTableVM
                {
                    QuizId = attemptGrouping.Key.Id,
                    QuizTitle = attemptGrouping.Key.Name,
                    UngradedAttemptCount = attemptGrouping.Count(),
                    User = _iMapper.Map<SimpleUserVM>(attemptGrouping.FirstOrDefault()?.User)
                })
                .ToList();
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