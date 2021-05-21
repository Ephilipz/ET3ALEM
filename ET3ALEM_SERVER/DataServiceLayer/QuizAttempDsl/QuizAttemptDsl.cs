using BusinessEntities.Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Enumerators;

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

        public async Task<QuizAttempt> PutQuizAttempt(int id, string userId, QuizAttempt quizAttempt)
        {
            quizAttempt.UserId = userId;
            if (quizAttempt.SubmitTime != null && quizAttempt.Quiz.AutoGrade)
                quizAttempt.GradeQuiz();
            return await _IQuizAttemptDal.PutQuizAttempt(id, quizAttempt);
        }

        public async Task<QuizAttempt> UpdateQuizAttemptGrade(QuizAttempt quizAttempt)
        {
            quizAttempt.QuestionsAttempts.ForEach(qA => qA.IsGraded = true);
            quizAttempt.Grade = Math.Round(quizAttempt.QuestionsAttempts.Sum(qA => qA.Grade));
            quizAttempt.IsGraded = true;
            return await _IQuizAttemptDal.UpdateQuizAttemptGrade(quizAttempt);
        }


        public async Task<QuizAttempt> InsertQuizAttempt(QuizAttempt quizAttempt)
        {
            var quiz = await _IQuizDsl.GetQuiz(quizAttempt.QuizId);
            foreach (var quizQuestion in quiz.QuizQuestions)
            {
                quizAttempt.QuestionsAttempts.Add(GetQuestionAttemptFromQuizQuestion(quizQuestion));
            }
            return await _IQuizAttemptDal.InsertQuizAttempt(quizAttempt);
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
                    MultipleChoiceQuestion MCQquestion = (MultipleChoiceQuestion)quizQuestion.Question;
                    return new MCQAttmept()
                    {
                        QuizQuestion = quizQuestion,
                        QuizQuestionId = quizQuestion.Id,
                        Id = 0
                    };
                case QuestionType.TrueFalse:
                    TrueFalseQuestion TFquestion = (TrueFalseQuestion)quizQuestion.Question;
                    return new TrueFalseAttempt()
                    {
                        QuizQuestion = quizQuestion,
                        QuizQuestionId = quizQuestion.Id,
                        Id = 0,
                    };
                default:
                    return new TrueFalseAttempt();
            }
        }
    }
}
