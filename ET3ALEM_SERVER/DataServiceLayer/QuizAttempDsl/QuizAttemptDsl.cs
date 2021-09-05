using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities.Enumerators;
using BusinessEntities.Models;
using BusinessEntities.ViewModels;
using DataAccessLayer;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DataServiceLayer
{
    public class QuizAttemptDsl : IQuizAttemptDsl
    {
        private readonly IQuizAttemptDal _IQuizAttemptDal;
        private readonly IQuizDsl _IQuizDsl;

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
            var attempt = await _IQuizAttemptDal.GetQuizAttemptWithQuiz(id);
            attempt.QuestionsAttempts = attempt.QuestionsAttempts.OrderBy(qA => qA.QuizQuestion.Sequence).ToList();
            return attempt;
        }

        public async Task<QuizAttempt> PostQuizAttempt(string userId, QuizAttempt quizAttempt)
        {
            //check that userId matches
            var matchingQuizAttempt = await _IQuizAttemptDal.GetQuizAttempt(quizAttempt.Id);
            if (matchingQuizAttempt.UserId != userId) return null;
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
            var quiz = await _IQuizDsl.GetQuiz(quizAttempt.QuizId);
            var assignedQuizQuestions = GetAssignedQuestionsForQuiz(quiz);
            foreach (var quizQuestion in assignedQuizQuestions)
                quizAttempt.QuestionsAttempts.Add(GetQuestionAttemptFromQuizQuestion(quizQuestion));
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

        private List<QuizQuestion> GetAssignedQuestionsForQuiz(Quiz quiz)
        {
            var quizQuestions = quiz.QuizQuestions;
            if (!quiz.ShuffleQuestions)
                return quiz.QuizQuestions;
            var rnd = new Random();
            var indexes = Enumerable.Range(0, quiz.QuizQuestions.Count())
                .ToArray();
            rnd.Shuffle(indexes);
            for (var i = 0; i < indexes.Count(); i++) quizQuestions[i].Sequence = indexes[i];

            return quizQuestions
                .OrderBy(qQ => qQ.Sequence)
                .Take(quiz.IncludedQuestionsCount ?? quiz.QuizQuestions.Count())
                .ToList();
        }

        private QuestionAttempt GetQuestionAttemptFromQuizQuestion(QuizQuestion quizQuestion)
        {
            switch (quizQuestion.Question.QuestionType)
            {
                case QuestionType.MCQ:
                    return new MCQAttmept
                    {
                        QuizQuestion = quizQuestion,
                        QuizQuestionId = quizQuestion.Id,
                        Id = 0
                    };
                case QuestionType.TrueFalse:
                    return new TrueFalseAttempt
                    {
                        QuizQuestion = quizQuestion,
                        QuizQuestionId = quizQuestion.Id,
                        Id = 0
                    };
                case QuestionType.ShortAnswer:
                    return new ShortAnswerAttempt
                    {
                        QuizQuestion = quizQuestion,
                        QuizQuestionId = quizQuestion.Id,
                        Id = 0
                    };
                case QuestionType.LongAnswer:
                    return new LongAnswerAttempt
                    {
                        QuizQuestion = quizQuestion,
                        QuizQuestionId = quizQuestion.Id,
                        Id = 0
                    };
                default:
                    throw new InvalidCastException("No valid question type was provided");
            }
        }

        public async Task<ActionResult<QuizAttemptVM>> GetQuizAttemptWithQuizLight(int id)
        {
            var attempt = await _IQuizAttemptDal.GetQuizAttemptWithQuiz(id);
            attempt.QuestionsAttempts = attempt.QuestionsAttempts.OrderBy(qA => qA.QuizQuestion.Sequence).ToList();
            return GetQuizAttemptVM(attempt);
        }
        private QuizAttemptVM GetQuizAttemptVM(QuizAttempt attempt)
        {
            return new QuizAttemptVM
            {
                Id = attempt.Id,
                UserId = attempt.UserId,
                StartTime = attempt.StartTime,
                Quiz = new QuizVM
                {
                    Id = attempt.Quiz.Id,
                    Code = attempt.Quiz.Code,
                    Name = attempt.Quiz.Name,
                    Instructions = attempt.Quiz.Instructions,
                    StartDate = attempt.Quiz.StartDate,
                    EndDate = attempt.Quiz.EndDate,
                    NoDueDate = attempt.Quiz.NoDueDate,
                    DurationSeconds = attempt.Quiz.DurationSeconds,
                    UnlimitedTime = attempt.Quiz.UnlimitedTime,
                    AllowedAttempts = attempt.Quiz.AllowedAttempts,
                    UnlimitedAttempts = attempt.Quiz.UnlimitedAttempts,
                    ShowGrade = attempt.Quiz.ShowGrade,
                    AutoGrade = attempt.Quiz.AutoGrade,
                    ShuffleQuestions = attempt.Quiz.ShuffleQuestions,
                    IncludeAllQuestions = attempt.Quiz.IncludeAllQuestions,
                    IncludedQuestionsCount = attempt.Quiz.IncludedQuestionsCount,
                },
                QuestionsAttempts = attempt.QuestionsAttempts.ConvertAll(questionAttempt => new QuestionAttemptVM
                {
                    Id = questionAttempt.Id,
                    LongAnswer = string.Empty,
                    QuizQuestion = new QuizQuestionVM
                    {
                        Id = questionAttempt.QuizQuestion.Id,
                        Grade = questionAttempt.QuizQuestion.Grade,
                        Sequence = questionAttempt.QuizQuestion.Sequence,
                        Question = GetQuestionVM(questionAttempt.QuizQuestion.Question)
                    }
                }).ToList()

            };
        }

        private QuestionVM GetQuestionVM(Question question)
        {
            return question switch
            {
                TrueFalseQuestion tfQuestion => new TrueFalseQuestionVM
                {
                    QuestionType = question.QuestionType,
                    Body = question.Body,
                    Id = question.Id
                },
                MultipleChoiceQuestion mcq => new MultipleChoiceQuestionVM
                {
                    QuestionType = question.QuestionType,
                    Body = question.Body,
                    Id = question.Id,
                    McqAnswerType = mcq.McqAnswerType,
                    Choices = mcq.Choices.ConvertAll(choice => new ChoiceVM { Id = choice.Id, Body = choice.Body })
                },
                ShortAnswerQuestion shortAnswerQuestion => new ShortAnswerQuestionVM
                {
                    QuestionType = question.QuestionType,
                    Body = question.Body,
                    Id = question.Id
                },
                LongAnswerQuestion longAnswerQuestion => new LongAnswerQuestionVM
                {
                    QuestionType = question.QuestionType,
                    Body = question.Body,
                    Id = question.Id
                },
                _ => throw new ArgumentException("invalid question type"),
            };
        }
    }
}