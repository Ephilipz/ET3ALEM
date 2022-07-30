using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities.Models;

namespace DataAccessLayer
{
    public interface IQuestionDal
    {
        Task<List<Question>> GetQuestions();
        Task<Question> InsertQuestion(Question question);
        Task<Question> DeleteQuestion(int questionId);
        Task PutQuestion(Question question);
        Task<IEnumerable<Question>> InsertQuestions(IEnumerable<Question> questions);
        Task<IEnumerable<Question>> DeleteQuestions(IEnumerable<int> questionIds);
        Task <IEnumerable<Question>> PutQuestions(IEnumerable<Question> questions);
    }
}