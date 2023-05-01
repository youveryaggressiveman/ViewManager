using GeneralLogic.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLogic.Services.Translator
{
    public static class TranslatorController
    {
        public static async Task<TrsanslationModel> GetTranslation(string text, string inputLang, string outputLang)
        {
            try
            {
                using (HttpClient client = new())
                {
                    var url = $"https://api.mymemory.translated.net/get?q={text}&langpair={inputLang}|{outputLang}";

                    var result = await client.GetAsync(url);

                    var endResult = JsonConvert.DeserializeObject<ResponseData>(await result.Content.ReadAsStringAsync());

                    TrsanslationModel newModel = new()
                    {
                        Data = text,
                        Translation = endResult?.Matches?.FirstOrDefault()?.Translation!
                    };

                    return newModel;
                }
            }
            catch
            {
                throw new Exception();
            }
            

            
        }
    }
}
