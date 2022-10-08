using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Core.Tokens
{
    public static class TokenForApi
    {
        private static string s_tokenApi = "";

        public static string GetTokenApi() =>
            s_tokenApi;

        public static void SetTokenApi(string token) =>
            s_tokenApi = token;
    }
}
