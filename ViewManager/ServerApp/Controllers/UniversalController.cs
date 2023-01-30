using GeneralLogic.Services.Files;
using HidSharp.Utility;
using Newtonsoft.Json;
using ServerApp.Core.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Controllers
{
    public class UniversalController<T>
    {
        protected readonly string _connectionString;

        private readonly string _nameT;

        public UniversalController(string connectionString)
        {
            _connectionString = connectionString;

            var type = typeof(T);
            _nameT = type.Name;
        }
            

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

                    var result = await client.GetAsync(_connectionString + $"api/{_nameT}/Get{_nameT}List");

                    if (result.IsSuccessStatusCode)
                    {
                        var endResult = JsonConvert.DeserializeObject<IEnumerable<T>>(await result.Content.ReadAsStringAsync());

                        LogManager.SaveLog("Server", DateTime.Today, "Api: The response was received successfully.");

                        return endResult;
                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Api: There is no user with such data.");

                        return null;
                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Api: The user is not logged in to the system.");

                        throw new Exception("The user is not logged in");
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {

                LogManager.SaveLog("Server", DateTime.Today, $"Api: {ex.Message}.");

                throw new OperationCanceledException(ex.Message);
            }
        }

        public async Task<T> Get(string id)
        {
            try
            {
                using (HttpClient client = new())
                {
                    var header = client.DefaultRequestHeaders;
                    header.Accept.Clear();
                    header.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    header.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TokenForApi.GetTokenApi());

                    var url = _connectionString + $"api/{_nameT}/Get{_nameT}ById?id={id}";

                    var result = await client.GetAsync(url);

                    if (result.IsSuccessStatusCode)
                    {
                        var endResult = JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());

                        LogManager.SaveLog("Server", DateTime.Today, "Api: The response was received successfully.");

                        return endResult;
                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Api: There is no user with such data.");

                        return default;
                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Api: The user is not logged in to the system.");

                        throw new Exception("The user is not logged in");
                    }

                    return default;
                }
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Server", DateTime.Today, $"Api: {ex.Message}.");

                throw new OperationCanceledException(ex.Message);
            }
        }

        public async Task<T?> Create(T obj)
        {
            try
            {
                using (HttpClient client = new())
                {
                    var header = client.DefaultRequestHeaders;
                    header.Accept.Clear();
                    header.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    header.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TokenForApi.GetTokenApi());

                    var url = _connectionString + $"api/{_nameT}/Create{_nameT}";
                    var jsonObject = JsonConvert.SerializeObject(obj);

                    var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    var result = await client.PostAsync(url, content);

                    if (result.IsSuccessStatusCode)
                    {
                        var endResult = JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());

                        LogManager.SaveLog("Server", DateTime.Today, "Api: The response was received successfully.");

                        return endResult;
                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Api: There is no user with such data.");

                        return default;
                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Api: The user is not logged in to the system.");

                        throw new Exception("The user is not logged in");
                    }

                    return default;
                }
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Server", DateTime.Today, $"Api: {ex.Message}.");

                throw new OperationCanceledException(ex.Message);
            }
        }

        public async Task<bool> Put(T obj)
        {
            try
            {
                using (HttpClient client = new())
                {
                    var header = client.DefaultRequestHeaders;
                    header.Accept.Clear();
                    header.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    header.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TokenForApi.GetTokenApi());

                    var url = _connectionString + $"api/{_nameT}/Update{_nameT}";
                    var jsonObject = JsonConvert.SerializeObject(obj);

                    var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    var result = await client.PutAsync(url, content);

                    if (result.IsSuccessStatusCode)
                    {
                        var endResult = JsonConvert.DeserializeObject<bool>(await result.Content.ReadAsStringAsync());

                        LogManager.SaveLog("Server", DateTime.Today, "Api: The response was received successfully.");

                        return endResult;
                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Api: There is no user with such data.");

                        return false;
                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Api: The user is not logged in to the system.");

                        throw new Exception("The user is not logged in");
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Server", DateTime.Today, $"Api: {ex.Message}.");

                throw new OperationCanceledException(ex.Message);
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                using (HttpClient client = new())
                {
                    var header = client.DefaultRequestHeaders;
                    header.Accept.Clear();
                    header.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    header.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TokenForApi.GetTokenApi());

                    var url = _connectionString + $"api/{_nameT}/Remove{_nameT}";

                    var result = await client.DeleteAsync(url);

                    if (result.IsSuccessStatusCode)
                    {
                        var endResult = JsonConvert.DeserializeObject<bool>(await result.Content.ReadAsStringAsync());

                        LogManager.SaveLog("Server", DateTime.Today, "Api: The response was received successfully.");

                        return endResult;
                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Api: There is no user with such data.");

                        return false;
                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        LogManager.SaveLog("Server", DateTime.Today, "Api: The user is not logged in to the system.");

                        throw new Exception("The user is not logged in");
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Server", DateTime.Today, $"Api: {ex.Message}.");

                throw new OperationCanceledException(ex.Message);
            }
        }
    }
}
