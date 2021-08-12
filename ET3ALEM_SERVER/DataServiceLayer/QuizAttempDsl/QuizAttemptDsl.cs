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
            quizAttempt.Grade = quizAttempt.QuestionsAttempts.Sum(qA => qA.Grade);
            quizAttempt.IsGraded = true;
            return await _IQuizAttemptDal.UpdateQuizAttemptGrade(quizAttempt);
        }


        public async Task<QuizAttempt> InsertQuizAttempt(QuizAttempt quizAttempt)
        {
            var quiz = await _IQuizDsl.GetQuiz(quizAttempt.QuizId);
            List<QuizQuestion> assignedQuizQuestions = (!quiz.IncludeAllQuestions && quiz.IncludedQuestionsCount != null) ? quiz.QuizQuestions.Take((int)quiz.IncludedQuestionsCount).ToList() : quiz.QuizQuestions;
            if (quiz.ShuffleQuestions)
            {
                Random random = new Random();
                for (int i = assignedQuizQuestions.Count() - 1; i > 0; i--)
                {
                    int randomIndex = random.Next(0, i + 1);

                    int temp = assignedQuizQuestions[i].Sequence;
                    assignedQuizQuestions[i].Sequence = assignedQuizQuestions[randomIndex].Sequence;
                    assignedQuizQuestions[randomIndex].Sequence = temp;
                }
            }
            foreach (var quizQuestion in assignedQuizQuestions)
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
