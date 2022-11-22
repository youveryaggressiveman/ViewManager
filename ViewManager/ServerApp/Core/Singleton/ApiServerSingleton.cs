using GeneralLogic.Services.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    await client.GetAsync(s_connectionApiString);

                    LogManager.SaveLog("Server", DateTime.Today, " Api: Check connection: the server is successfully connected");

                    return true;
                }


            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Server", DateTime.Today, "Api: Check connection: " + ex.Message);

                return false;
            }
        }
    }
}
