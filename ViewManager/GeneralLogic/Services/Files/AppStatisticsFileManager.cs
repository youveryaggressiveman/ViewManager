using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLogic.Services.Files
{
    public class AppStatisticsFileManager : IFileManager
    {
        private readonly string _path = @"C:\ViewManager\AppStatistics\";

        public AppStatisticsFileManager()
        {
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        public async Task<string> FileReader(string name)
        {
            string filePath = _path + name + ".txt";

            if (!File.Exists(filePath))
            {
                throw new Exception("The file does not exist");
            }

            using (StreamReader streamReader = new StreamReader(filePath))
            {
                var text = await streamReader.ReadToEndAsync();

                return text;
            }
        }

        public async Task FileWriter(string text, string name)
        {
            string filePath = _path + name + ".txt";

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            switch (name)
            {
                case "AllowedApplications":

                    using (StreamWriter streamWriter = new StreamWriter(filePath, false))
                    {
                        await streamWriter.WriteAsync(text);
                    }

                    break;
                case "Statistics":

                    using (StreamWriter streamWriter = new StreamWriter(filePath, true))
                    {
                        await streamWriter.WriteLineAsync(text);
                    }

                    break;
                default:
                    break;
            }
            
        }
    }
}
