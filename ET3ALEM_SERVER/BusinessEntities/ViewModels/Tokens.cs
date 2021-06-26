using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.ViewModels
{
    public class Tokens
    {
        public string JWT { get; set; }
        public string RefreshToken { get; set; }
        public string UserId { get; set; }

        public Tokens(string jwt, string refresh, string userId)
        {
            JWT = jwt;
            RefreshToken = refresh;
            UserId = userId;
        }
    }
}
