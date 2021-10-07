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
        private readonly IMapper _IMapper;

        public QuizDsl(IQuizDal quizDal, IQuestionDsl questionDsl, IQuizAttemptDal quizAttemptDal, IMapper iMapper)
        {
            _iQuizDal = quizDal;
            _iQuestionDsl = questionDsl;
            _IMapper = iMapper;
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

        public async Task<List<UngradedQuizTableVM>> GetUngradedQuizzesForUser(string userId)
        {
            List<QuizAttempt> ungradedAttempts = await _iQuizDal.GetUngradedQuizzesForUser(userId);
            return ungradedAttempts
                .GroupBy(attempt => attempt.Quiz)
                .Select(attemptGrouping => new UngradedQuizTableVM
                {
                    QuizId = attemptGrouping.Key.Id,
                    QuizTitle = attemptGrouping.Key.Name,
                    UngradedAttemptCount = attemptGrouping.Count(),
                    User = _IMapper.Map<SimpleUserVM>(attemptGrouping.FirstOrDefault()?.User)
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