﻿using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IQuizAttemptDal
    {
        Task<QuizAttempt> InsertQuizAttempt(QuizAttempt quizAttempt);
        Task<QuizAttempt> GetQuizAttempt(int id);
        Task<QuizAttempt> PostQuizAttempt(QuizAttempt quizAttempt);
        Task<QuizAttempt> UpdateQuizAttemptGrade(QuizAttempt quizAttempt);
        Task<List<QuizAttempt>> GetUserQuizAttemptsForQuiz(int quizId, string userId);
        Task<List<QuizAttempt>> GetAllQuizAttemptsForQuiz(int quizId);
        Task<QuizAttempt> GetQuizAttemptWithQuiz(int id);
        Task<List<QuizAttempt>> GetQuizAttempts(string userId);
        Task DeleteRelatedQuizAttempts(int quizId);
    }
}
