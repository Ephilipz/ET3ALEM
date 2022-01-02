using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities.Enumerators;
using BusinessEntities.Models;
using Microsoft.EntityFrameworkCore;
using Server_Application.Data;

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
            var question = await _context.Questions.FindAsync(questionId);
            if (question != null)
            {
                _context.Questions.Remove(question);
                if (question.QuestionType == QuestionType.MCQ)
                {
                    var mcq = question as MultipleChoiceQuestion;
                    _context.Choices.RemoveRange(mcq.Choices);
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
                    TrackChangesInMCQ(question);
                    break;
                case QuestionType.TrueFalse:
                    TrackChangesInTFQuestion(question);
                    break;
            }

            _context.Entry(question).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private void TrackChangesInTFQuestion(Question question)
        {
            var wasMCQ = _context.Questions.Any(q => question.Id == q.Id && q.QuestionType == QuestionType.MCQ);
            if (wasMCQ)
            {
                IQueryable<Choice> choices = _context.Choices.Where(c => c.MCQId == question.Id);
                _context.Choices.RemoveRange(choices);
            }
        }

        private void TrackChangesInMCQ(Question question)
        {
            var mcq = question as MultipleChoiceQuestion;

            foreach (Choice c in mcq.Choices)
            {
                bool isAdded = c.Id == 0;
                bool isDeleted = c.Id < 0;
                bool isEdited = c.Id > 0;
                
                if (isAdded)
                {
                    _context.Choices.Add(c);
                }
                else if (isDeleted)
                {
                    c.Id *= -1;
                    _context.Choices.Remove(c);
                }
                else if(isEdited)
                {
                    _context.Entry(c).State = EntityState.Modified;
                }
            }
        }
    }
}