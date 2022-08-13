using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;

namespace DataServiceLayer
{
    public interface IQuestionDsl
    {
        Task<List<Question>> GetQuestions();
        Task<IEnumerable<Question>> InsertQuestions(IEnumerable<Question> questions);
        Task<IEnumerable<Question>> DeleteQuestions(IEnumerable<int> questionIds);
        Task UpdateQuestionsBasedOnId(IEnumerable<Question> questions);
    }
}