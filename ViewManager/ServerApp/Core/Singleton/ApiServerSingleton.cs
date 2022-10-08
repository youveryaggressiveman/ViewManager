using GeneralLogic.Services.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Core.Singleton
{
    public static class ApiServerSingleton
    {
        private static readonly string s_connectionApiString = "http://localhost:5194/";

        public static string GetConnectionApiString() =>
            s_connectionApiString;

        public static async Task<bool> CheckConnection()
        {
                var request = WebRequest.Create(s_connectionApiString);
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)await request.GetResponseAsync();
                    response.Close();

                    LogManager.SaveLog("Server", DateTime.Today, "Check connection: the server is successfully connected");

                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.SaveLog("Server", DateTime.Today, "Check connection: " + ex.Message);

                    return false;
                }             
        }
    }
}
