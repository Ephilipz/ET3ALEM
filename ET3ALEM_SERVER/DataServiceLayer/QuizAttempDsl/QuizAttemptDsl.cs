using BusinessEntities.Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Enumerators;
using Helpers;

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

        public async Task<QuizAttempt> GetQuizAttemptWithQuiz(int id)
        {
            QuizAttempt attempt = await _IQuizAttemptDal.GetQuizAttemptWithQuiz(id);
            attempt.QuestionsAttempts = attempt.QuestionsAttempts.OrderBy(qA => qA.QuizQuestion.Sequence).ToList();
            return attempt;
        }

        public async Task<QuizAttempt> PostQuizAttempt(string userId, QuizAttempt quizAttempt)
        {
            //check that userId matches
            QuizAttempt matchingQuizAttempt = await _IQuizAttemptDal.GetQuizAttempt(quizAttempt.Id);
            if(matchingQuizAttempt.UserId != userId)
            {
                return null;
            }
            quizAttempt.UserId = userId;
            if (quizAttempt.SubmitTime != null && quizAttempt.Quiz.AutoGrade)
                quizAttempt.GradeQuiz();
            return await _IQuizAttemptDal.PostQuizAttempt(quizAttempt);
        }

        public async Task<QuizAttempt> UpdateQuizAttemptGrade(QuizAttempt quizAttempt)
        {
            quizAttempt.QuestionsAttempts.ForEach(qA => qA.IsGraded = true);
            quizAttempt.Grade = quizAttempt.QuestionsAttempts.Sum(qA => qA.Grade);
            quizAttempt.IsGraded = true;
            return await _IQuizAttemptDal.UpdateQuizAttemptGrade(quizAttempt);
        }


        public async Task<QuizAttempt> InsertQuizAttempt(QuizAttempt quizAttempt)
        {
            Quiz quiz = await _IQuizDsl.GetQuiz(quizAttempt.QuizId);
            List<QuizQuestion> assignedQuizQuestions = GetAssignedQuestionsForQuiz(quiz);
            foreach (var quizQuestion in assignedQuizQuestions)
            {
                quizAttempt.QuestionsAttempts.Add(GetQuestionAttemptFromQuizQuestion(quizQuestion));
            }
            return await _IQuizAttemptDal.InsertQuizAttempt(quizAttempt);
        }

        private List<QuizQuestion> GetAssignedQuestionsForQuiz(Quiz quiz)
        {
            List<QuizQuestion> quizQuestions = quiz.QuizQuestions;
            if (!quiz.ShuffleQuestions)
                return quiz.QuizQuestions;
            Random rnd = new Random();
            int[] indexes = Enumerable.Range(0, quiz.QuizQuestions.Count())
                .ToArray();
            rnd.Shuffle(indexes);
            for (int i = 0; i < indexes.Count(); i++)
            {
                quizQuestions[i].Sequence = indexes[i];
            }

            return quizQuestions
                .OrderBy(qQ => qQ.Sequence)
                .Take(quiz.IncludedQuestionsCount ?? quiz.QuizQuestions.Count())
                .ToList();
        }

        public Task<List<QuizAttempt>> GetUserQuizAttemptsForQuiz(int quizId, string userId)
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

        private QuestionAttempt GetQuestionAttemptFromQuizQuestion(QuizQuestion quizQuestion)
        {
            switch (quizQuestion.Question.QuestionType)
            {
                case QuestionType.MCQ:
                    return new MCQAttmept()
                    {
                        QuizQuestion = quizQuestion,
                        QuizQuestionId = quizQuestion.Id,
                        Id = 0
                    };
                case QuestionType.TrueFalse:
                    return new TrueFalseAttempt()
                    {
                        QuizQuestion = quizQuestion,
                        QuizQuestionId = quizQuestion.Id,
                        Id = 0,
                    };
                case QuestionType.ShortAnswer:
                    return new ShortAnswerAttempt()
                    {
                        QuizQuestion = quizQuestion,
                        QuizQuestionId = quizQuestion.Id,
                        Id = 0
                    };
                case QuestionType.LongAnswer:
                    return new LongAnswerAttempt()
                    {
                        QuizQuestion = quizQuestion,
                        QuizQuestionId = quizQuestion.Id,
                        Id = 0
                    };
                default:
                    return new TrueFalseAttempt();
            }
        }
    }
}
