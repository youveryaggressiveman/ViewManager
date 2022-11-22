﻿using GeneralLogic.Services.Files;
using Newtonsoft.Json;
using ServerApp.Core.Singleton;
using ServerApp.Core.Tokens;
using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Controllers
{
    public class UpdateUserListPageViewModelController<T> where T : class
    {
        private readonly string _connectionString;

        public UpdateUserListPageViewModelController(string connectionString) =>
            _connectionString = connectionString;

        public async Task<IEnumerable<T>> GetList()
        {
            try
            {
                using (HttpClient client = new())
                {
                    var header = client.DefaultRequestHeaders;
                    header.Accept.Clear();
                    header.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    header.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TokenForApi.GetTokenApi());

                    HttpResponseMessage? result;

                    if (typeof(T).IsAssignableFrom(typeof(Role)))
                    {
                        result = await client.GetAsync(_connectionString + "api/Role/GetRoleList");
                    }
                    else if (typeof(T).IsAssignableFrom(typeof(Office)))
                    {
                        result = await client.GetAsync(_connectionString + "api/Office/GetOfficeList");
                    }
                    else
                    {
                        result = await client.GetAsync(_connectionString + "api/User/GetUserList");
                    }

                    if (result.IsSuccessStatusCode)
                    {
                        var endResult = JsonConvert.DeserializeObject<IEnumerable<T>>(await result.Content.ReadAsStringAsync());

                        LogManager.SaveLog("Server", DateTime.Today, "Api: The response was received successfully");

                        return endResult;
                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Api: There is no user with such data");

                        return null;
                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
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


        public async Task<bool> PutUserInfo(T user)
        {
            try
            {
                using (HttpClient client = new())
                {
                    var header = client.DefaultRequestHeaders;
                    header.Accept.Clear();
                    header.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    header.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TokenForApi.GetTokenApi());

                    var url = _connectionString + "api/User/UpdateUser";
                    var jsonObject = JsonConvert.SerializeObject(user);

                    var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    var result = await client.PutAsync(url, content);

                    if (result.IsSuccessStatusCode)
                    {
                        var endResult = JsonConvert.DeserializeObject<bool>(await result.Content.ReadAsStringAsync());

                        LogManager.SaveLog("Server", DateTime.Today, "Api: The response was received successfully");

                        return endResult;
                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Api: There is no user with such data");

                        return false;
                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Api: The user is not logged in to the system");

                        return false;
                    }

                    return false;
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
