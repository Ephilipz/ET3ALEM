using HashidsNet;

namespace Helpers
{
    public class QuizHelper : IQuizHelper
    {
        private static readonly string salt = "r8455qRJMx";
        private static readonly Hashids hashIds = new(salt, 5, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
        public string GetCode(int id)
        {
            return hashIds.Encode(id);
        }
    }
}