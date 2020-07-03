using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.ViewModels
{
    public class Tokens
    {
        public string JWT { get; set; }
        public string RefreshToken { get; set; }

        public Tokens(string jwt, string refresh)
        {
            JWT = jwt;
            RefreshToken = refresh;
        }
    }
}
