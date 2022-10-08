using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GeneralLogic.Services.Files
{
    public class FileManager : IFileManager
    {
        private readonly string _path = @"C:\ViewManager\PcFeatures";

        public FileManager()
        {
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        public async Task<string> FileReader(string pcName)
        {
            string filePath = _path + pcName + ".txt";

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

        public async Task FileWriter(string text, string pcName)
        {
            string filePath = _path + pcName + ".txt";

            if (!File.Exists(filePath))
            {
                File.Create(_path).Close();
            }

            using (StreamWriter streamWriter = new StreamWriter(filePath, false))
            {
                await streamWriter.WriteLineAsync(text);
            }
        }
    }
}
