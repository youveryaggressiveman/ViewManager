using GeneralLogic.Services.Files;
using Newtonsoft.Json;
using ServerApp.Core.Singleton;
using ServerApp.Core.Tokens;
using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Controllers
{
    public class MainPageViewModelController
    {
        private readonly string _connectionString;

        public MainPageViewModelController(string connectionString) =>
            _connectionString = connectionString;

        public async Task<User> GetUserById()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var header = client.DefaultRequestHeaders;
                    header.Accept.Clear();
                    header.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    header.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TokenForApi.GetTokenApi());

                    var result = await client.GetAsync(_connectionString + "api/User/GetUserById?Id=" + AuthUserSingleton.AuthUser.Id);

                    if (result.IsSuccessStatusCode)
                    {
                        var endResult = JsonConvert.DeserializeObject<User>(await result.Content.ReadAsStringAsync());                     

                        LogManager.SaveLog("Server", DateTime.Today, "Api: The response was received successfully");

                        return endResult;
                    }
                    else if(result.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Api: There is no user with such data");

                        return null;
                    }
                    else if(result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Api: The user is not logged in to the system");

                        return null;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {

                LogManager.SaveLog("Server", DateTime.Today, "Api: " + ex.Message);

                throw new OperationCanceledException(ex.Message);
            }
        }
    }
}
