using HashidsNet;

namespace Helpers
{
    public class QuizHelper : IQuizHelper
    {
        private readonly string salt = "r8455qRJMx";

        public string GetCode(int id)
        {
            var hashids = new Hashids(salt, 5, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
            return hashids.Encode(id);
        }
    }
}