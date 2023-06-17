﻿using GeneralLogic.Services.Files;
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
        private static readonly string s_connectionApiString = "http://45.138.157.71:5194/";

        public static string GetConnectionApiString() =>
            s_connectionApiString;

        public static async Task<bool> CheckConnection()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    await client.GetAsync(s_connectionApiString);

                    return true;
                }


            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Server", DateTime.Today, $"Api: Check connection: {ex.Message}.");

                return false;
            }
        }
    }
}
