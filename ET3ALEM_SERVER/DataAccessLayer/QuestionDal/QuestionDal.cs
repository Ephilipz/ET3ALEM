using Microsoft.EntityFrameworkCore;
using BusinessEntities.Models;
using Server_Application.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Enumerators;
using System.Linq;

namespace DataAccessLayer
{
    public class QuestionDal : IQuestionDal
    {
        private readonly ApplicationContext _context;
        public QuestionDal(ApplicationContext context)
        {
            _context = context;
        }
        public Task<List<Question>> GetQuestions()
        {
            return _context.Questions.ToListAsync();
        }
        public async Task<Question> InsertQuestion(Question question)
        {
            await _context.Questions.AddAsync(question);
            return question;
        }

        public async Task<Question> DeleteQuestion(int questionId)
        {
            Question question = await _context.Questions.FindAsync(questionId);
            if (question != null)
            {
                _context.Questions.Remove(question);
                switch (question.QuestionType)
                {
                    case QuestionType.MCQ:
                        MultipleChoiceQuestion mcq = question as MultipleChoiceQuestion;
                        _context.Choices.RemoveRange(mcq.Choices);
                        break;
                }
                await _context.SaveChangesAsync();
            }
            return question;
        }

        public async Task PutQuestion(Question question)
        {
            switch (question.QuestionType)
            {
                case QuestionType.MCQ:
                    MultipleChoiceQuestion mcq = question as MultipleChoiceQuestion;

                    //track changes in choices
                    foreach (Choice c in mcq.Choices)
                    {
                        //new choice
                        if (c.Id == 0)
                            _context.Choices.Add(c);
                        //deleted choice
                        else if (c.Id < 0)
                        {
                            c.Id *= -1;
                            _context.Choices.Remove(c);
                        }
                        //modified choice
                        else
                            _context.Entry(c).State = EntityState.Modified;
                    }
                    break;
                case QuestionType.TrueFalse:
                    //if question is updated from MCQ to TF, delete the choices
                    Question oldQuestion = _context.Questions.First(q => q.Id == question.Id);
                    if (oldQuestion.QuestionType == QuestionType.MCQ)
                    {
                        _context.Choices.RemoveRange(((MultipleChoiceQuestion)oldQuestion).Choices);
                    }
                    break;
            }
            _context.Entry(question).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
