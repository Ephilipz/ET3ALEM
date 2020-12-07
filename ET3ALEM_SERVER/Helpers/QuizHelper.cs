using System;
using System.Collections.Generic;
using System.Text;
using HashidsNet;

namespace Helpers
{
    public class QuizHelper
    {
        private static readonly string salt = "r8455qRJMx";
        public static string getCode(int id)
        {
            var hashids = new Hashids(salt, 5, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
            return hashids.Encode(id);
        }
    }
}
