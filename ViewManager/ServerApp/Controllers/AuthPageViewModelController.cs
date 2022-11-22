using Newtonsoft.Json;
using ServerApp.Core.Singleton;
using ServerApp.Core.Tokens;
using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ServerApp.Controllers
{
    public class AuthPageViewModelController
    {
        private readonly string _connectionString;

        public AuthPageViewModelController(string connectionString) =>
            _connectionString = connectionString;

        public async Task<bool> AuthHelper(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = _connectionString + "token?login=" + user.Login + 
                    "&password=" + user.Password;

                var result = await client.PostAsync(url, null);
                
                if (result.IsSuccessStatusCode)
                {
                    var endResult =  JsonConvert.DeserializeObject<AuthUser>(await result.Content.ReadAsStringAsync());

                    TokenForApi.SetTokenApi(endResult.Token.ToString());

                    AuthUserSingleton.AuthUser.Id = endResult.Id.ToString();
                    AuthUserSingleton.AuthUser.RoleValue = endResult.RoleValue.ToString();

                    return true;
                }
                else if(result.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return false;
                }

                throw new Exception(result.RequestMessage?.ToString());
            }
        }
    }
}
