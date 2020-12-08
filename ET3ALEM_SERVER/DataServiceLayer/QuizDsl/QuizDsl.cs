using BusinessEntities.Models;
using DataAccessLayer;
using DataServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer
{
    public class QuizDsl : IQuizDsl
    {
        private readonly IQuizDal _IQuizDal;
        private readonly IQuestionDsl _IQuestionDsl;
        public QuizDsl(IQuizDal IQuizDal, IQuestionDsl IQuestionDsl)
        {
            _IQuizDal = IQuizDal;
            _IQuestionDsl = IQuestionDsl;
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

        public Task<IEnumerable<Quiz>> GetQuizzes()
        {
            return _IQuizDal.GetQuizzes();
        }

        public Task<Quiz> InsertQuiz(Quiz quiz)
        {
            foreach (var quizQuestion in quiz.QuizQuestions)
            {
                if (quizQuestion.QuestionId <= 0)
                    quizQuestion.QuestionId = _IQuestionDsl.InsertQuestion(quizQuestion.Question).Id;
            }
            return _IQuizDal.InsertQuiz(quiz);
        }
        public async Task<Quiz> DeleteQuiz(int id)
        {
            //remove quiz
            Quiz quiz = await _IQuizDal.DeleteQuiz(id);

            if (quiz != null)
            {
                //remove questions
                List<int> relatedQuestions = quiz.QuizQuestions.Select(quizQuestion => quizQuestion.QuestionId).ToList();
                foreach (int questionId in relatedQuestions)
                {
                    await _IQuestionDsl.DeleteQuestion(questionId);
                }

            }

            return quiz;
        }

        public async Task PutQuiz(int id, Quiz quiz)
        {
            foreach(QuizQuestion Qquestion in quiz.QuizQuestions)
            {
                if (Qquestion.Question.Id < 0)
                {
                    await _IQuestionDsl.DeleteQuestion(Qquestion.Question.Id * -1);
                }
                else if (Qquestion.Question.Id == 0)
                {
                    Qquestion.QuestionId = (await _IQuestionDsl.InsertQuestion(Qquestion.Question)).Id;
                }
                else
                {
                    await _IQuestionDsl.PutQuestion(Qquestion.Question);
                }
            }
            await _IQuizDal.PutQuiz(id, quiz);
        }



    }
}
