using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities.Models;
using Microsoft.EntityFrameworkCore;
using Server_Application.Data;

namespace DataAccessLayer
{
    public class QuizAttemptDal : IQuizAttemptDal
    {
        private readonly ApplicationContext _context;

        public QuizAttemptDal(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<QuizAttempt> GetQuizAttempt(int id)
        {
            return await _context.QuizAttempts.Where(qA => qA.Id == id)
                .Include(qA => qA.QuestionsAttempts)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstAsync();
        }

        public Task<QuizAttempt> GetQuizAttemptWithQuiz(int id)
        {
            return _context.QuizAttempts.Where(qA => qA.Id == id)
                .Include(qA => qA.Quiz)
                .Include(qA => qA.QuestionsAttempts)
                .ThenInclude(qAS => ((MCQAttmept) qAS).Choices)
                .Include(qA => qA.QuestionsAttempts)
                .ThenInclude(qAS => ((LongAnswerAttempt) qAS).LongAnswer)
                .Include(qA => qA.QuestionsAttempts.OrderBy(qAs => qAs.Sequence))
                .ThenInclude(qAS => qAS.QuizQuestion)
                .ThenInclude(quizQuestion => quizQuestion.Question)
                .ThenInclude(question => ((MultipleChoiceQuestion) question).Choices)
                .Include(qA => qA.QuestionsAttempts.OrderBy(qAs => qAs.Sequence))
                .ThenInclude(qAS => qAS.QuizQuestion)
                .ThenInclude(quizQuestion => quizQuestion.Question)
                .ThenInclude(question => ((OrderQuestion) question).OrderedElements)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstAsync();
        }

        public async Task<QuizAttempt> PutQuizAttempt(QuizAttempt quizAttempt)
        {
            quizAttempt.QuestionsAttempts.ForEach(qA => qA.QuizQuestion = null);
            quizAttempt.Quiz = null;
            quizAttempt.QuestionsAttempts.ForEach(qA =>
            {
                if (qA is not MCQAttmept)
                    _context.Entry(qA).State = EntityState.Modified;
            });

            UpdateChoicesForMCQ(quizAttempt);
            _context.Entry(quizAttempt).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return quizAttempt;
        }

        private void UpdateChoicesForMCQ(QuizAttempt quizAttempt)
        {
            var mcqIds = quizAttempt.QuestionsAttempts.OfType<MCQAttmept>()
                .Select(mcq => mcq.Id).ToHashSet();

            if (mcqIds.Count == 0)
            {
                return;
            }

            var matchingMCQFromDBList = _context.QuestionAttempts
                .OfType<MCQAttmept>().Where(mcq => mcqIds.Contains(mcq.Id))
                .Include(mcq => mcq.Choices)
                .ToList();

            foreach (var mcqAttempt in quizAttempt.QuestionsAttempts.OfType<MCQAttmept>())
            {
                //detaches the attempt so the context doesn't track two entities with the same Id which gives an error
                _context.Entry(mcqAttempt).State = EntityState.Detached;

                var matchingMCQFromDB = matchingMCQFromDBList.FirstOrDefault(mcq => mcq.Id == mcqAttempt.Id);

                matchingMCQFromDB.Choices.RemoveAll(choice => !mcqAttempt.Choices.Exists(c => c.Id == choice.Id));

                var addedChoices = mcqAttempt.Choices
                    .Where(c => !matchingMCQFromDB.Choices.Exists(choice => c.Id == choice.Id));
                matchingMCQFromDB.Choices.AddRange(addedChoices);

                UpdateMCQInContext(matchingMCQFromDB, mcqAttempt);
            }
        }

        private void UpdateMCQInContext(MCQAttmept mcqAttemptFromDB, MCQAttmept mcqAttempt)
        {
            mcqAttemptFromDB.Choices.ForEach(choice => _context.Entry(choice).State = EntityState.Unchanged);
            mcqAttempt.Choices = mcqAttemptFromDB.Choices;
            _context.Entry(mcqAttemptFromDB).CurrentValues.SetValues(mcqAttempt);
        }

        public async Task<QuizAttempt> PostQuizAttempt(QuizAttempt quizAttempt)
        {
            foreach (var qA in quizAttempt.QuestionsAttempts)
            {
                _context.Entry(qA.QuizQuestion).State = EntityState.Unchanged;
            }

            _context.QuizAttempts.Add(quizAttempt);
            await _context.SaveChangesAsync();
            return quizAttempt;
        }

        public async Task<QuizAttempt> UpdateQuizAttemptGrade(QuizAttempt quizAttempt)
        {
            //detach child entities that will not be tracked
            quizAttempt.QuestionsAttempts.ForEach(qA => qA.QuizQuestion = null);
            quizAttempt.QuestionsAttempts.OfType<MCQAttmept>().ToList()
                .ForEach(mcqA => mcqA.Choices = null);
            quizAttempt.Quiz = null;

            _context.Update(quizAttempt);
            await _context.SaveChangesAsync();
            return quizAttempt;
        }

        public async Task<List<QuizAttempt>> GetUserQuizAttemptsForQuiz(
            int quizId, string userId)
        {
            return await _context.QuizAttempts
                .Where(qA => qA.QuizId == quizId && qA.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<QuizAttempt>> GetAllQuizAttemptsForQuiz(int quizId)
        {
            var quizAttempts = await _context.QuizAttempts
                .Where(qA => qA.QuizId == quizId)
                .Include(qA => qA.Quiz)
                .Include(qA => qA.User)
                .OrderByDescending(qA => qA.Grade)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();

            return quizAttempts;
        }

        public async Task<List<QuizAttempt>> GetQuizAttempts(string userId)
        {
            return await _context.QuizAttempts
                .Where(qA => qA.UserId == userId)
                .Include(qA => qA.Quiz)
                .OrderByDescending(qA => qA.SubmitTime)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<QuizAttempt>> GetUngradedAttemptsForQuiz(int quizId)
        {
            return await _context.QuizAttempts
                .Where(attempt => !attempt.IsGraded && attempt.QuizId == quizId)
                .Include(attempt => attempt.User)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
        }
    }
}