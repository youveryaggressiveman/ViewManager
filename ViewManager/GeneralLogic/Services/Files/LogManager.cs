using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLogic.Services.Files
{
    public static class LogManager
    {
        public static readonly string s_path = @"C:\ViewManager\Logs";

        public static void CreateMainFolder()
        {
            string mainPath = @"C:\ViewManager";

            if (!Directory.Exists(mainPath))
            {
                Directory.CreateDirectory(mainPath);
            }
        }

        public async static void SaveLog(string name, DateTime dateTime, string answer)
        {
            string pathFile = s_path + @"\" + name + "_" + dateTime.Day + "-" + dateTime.Month + "-" + dateTime.Year + ".txt";

            if (!Directory.Exists(s_path))
            {
                Directory.CreateDirectory(s_path);
            }

            if (!File.Exists(pathFile))
            {
                File.Create(pathFile).Close();
            }

            //using (StreamWriter streamWriter = new StreamWriter(pathFile, true))
            //{
            //    await streamWriter.WriteLineAsync(answer + " - " + DateTime.Now + "\n");
            //}
        }
    }
}
