using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessEntities.Models;
using BusinessEntities.ViewModels;
using DataAccessLayer;
using Helpers;

namespace DataServiceLayer
{
    public class QuizAttemptDsl : IQuizAttemptDsl
    {
        private readonly IQuizAttemptDal _IQuizAttemptDal;
        private readonly IQuizDsl _IQuizDsl;
        private readonly IMapper _IMapper;

        public QuizAttemptDsl(IQuizAttemptDal quizAttemptDal, IQuizDsl quizDsl,
            IMapper iMapper)
        {
            _IQuizAttemptDal = quizAttemptDal;
            _IQuizDsl = quizDsl;
            _IMapper = iMapper;
        }

        public Task<QuizAttempt> GetQuizAttempt(int id)
        {
            return _IQuizAttemptDal.GetQuizAttempt(id);
        }

        public async Task<QuizAttempt> GetQuizAttemptWithQuiz(int id)
        {
            var attempt = await _IQuizAttemptDal.GetQuizAttemptWithQuiz(id);
            attempt.QuestionsAttempts = attempt.QuestionsAttempts
                .OrderBy(qA => qA.QuizQuestion.Sequence).ToList();
            return attempt;
        }

        public async Task<QuizAttempt> PutQuizAttempt(string userId,
            QuizAttempt quizAttempt)
        {
            var matchingQuizAttempt =
                await _IQuizAttemptDal.GetQuizAttempt(quizAttempt.Id);
            if (matchingQuizAttempt.UserId != userId)
            {
                return null;
            }

            quizAttempt.UserId = userId;
            if (quizAttempt.Quiz.AutoGrade)
                quizAttempt.GradeQuiz();
            
            return await _IQuizAttemptDal.PutQuizAttempt(quizAttempt);
        }

        public async Task<QuizAttempt> UpdateQuizAttemptGrade(
            QuizAttempt quizAttempt)
        {
            quizAttempt.QuestionsAttempts.ForEach(qA => qA.IsGraded = true);
            quizAttempt.Grade =
                quizAttempt.QuestionsAttempts.Sum(qA => qA.Grade);
            quizAttempt.IsGraded = true;

            return await _IQuizAttemptDal.UpdateQuizAttemptGrade(quizAttempt);
        }


        public async Task<QuizAttempt> PostQuizAttempt(
            QuizAttempt quizAttempt)
        {
            var quiz = await _IQuizDsl.GetQuiz(quizAttempt.QuizId);
            var assignedQuizQuestions = GetAssignedQuestionsForQuiz(quiz);
            foreach (var quizQuestion in assignedQuizQuestions)
            {
                quizAttempt.QuestionsAttempts.Add(
                    QuestionAttemptFactory.GetQuestionAttemptFromQuizQuestion(
                        quizQuestion,
                        qa =>
                        {
                            qa.QuizQuestion = quizQuestion;
                            qa.QuizQuestionId = quizQuestion.Id;
                            qa.Id = 0;
                        }));
            }

            return await _IQuizAttemptDal.PostQuizAttempt(quizAttempt);
        }

        public Task<List<QuizAttempt>> GetUserQuizAttemptsForQuiz(int quizId,
            string userId)
        {
            return _IQuizAttemptDal.GetUserQuizAttemptsForQuiz(quizId, userId);
        }

        public Task<List<QuizAttempt>> GetAllQuizAttemptsForQuiz(int quizId)
        {
            return _IQuizAttemptDal.GetAllQuizAttemptsForQuiz(quizId);
        }

        public Task<List<QuizAttempt>> GetQuizAttempts(string userId)
        {
            return _IQuizAttemptDal.GetQuizAttempts(userId);
        }

        private List<QuizQuestion> GetAssignedQuestionsForQuiz(Quiz quiz)
        {
            if (!quiz.ShuffleQuestions)
                return quiz.QuizQuestions;
            var quizQuestions = GetShuffledQuizQuestions(quiz);
            int? questionCount = quiz.IncludeAllQuestions
                ? quiz.QuizQuestions.Count
                : quiz.IncludedQuestionsCount;
            return quizQuestions
                .OrderBy(qQ => qQ.Sequence)
                .Take(questionCount ?? quiz.QuizQuestions.Count)
                .ToList();
        }

        private List<QuizQuestion> GetShuffledQuizQuestions(Quiz quiz)
        {
            List<QuizQuestion> quizQuestions = quiz.QuizQuestions;
            Random rnd = new Random();
            int[] indexes = Enumerable.Range(0, quiz.QuizQuestions.Count)
                .ToArray();
            rnd.Shuffle(indexes);
            for (int i = 0; i < indexes.Length; i++)
            {
                quizQuestions[i].Sequence = indexes[i];
            }

            return quizQuestions;
        }

        public async Task<QuizAttemptVM> GetQuizAttemptWithQuizLight(int id)
        {
            var attempt = await _IQuizAttemptDal.GetQuizAttemptWithQuiz(id);
            attempt.QuestionsAttempts = attempt.QuestionsAttempts
                .OrderBy(qA => qA.QuizQuestion.Sequence).ToList();
            return _IMapper.Map<QuizAttemptVM>(attempt);
        }

        public async Task<List<QuizAttempt>> GetUngradedAttemptsForQuiz(
            int quizId)
        {
            return await _IQuizAttemptDal.GetUngradedAttemptsForQuiz(quizId);
        }
    }
}