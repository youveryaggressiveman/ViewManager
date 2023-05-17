using GeneralLogic.Services.Files;
using ServerApp.Core.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Core.Statistics
{
    public static class StatForm
    {
        private static IFileManager s_fileManager = new AppStatisticsFileManager();

        public static async Task SaveStat()
        {
            var allAppStat = string.Empty;

            foreach (var stat in AppStatSingleton.S_ListAppStat)
            {
                if (stat.Title == "Verified")
                {
                    allAppStat += "Verified: ";
                }

                allAppStat += $"{stat.ProcessName}, Client: {stat.ClientName}\n";
            }

            await s_fileManager.FileWriter("Statistics", allAppStat);
        }

        public static async Task SaveClient()
        {
            string clientList = string.Empty;
            string key = "1652456";

            foreach (var client in ConnectedClientSingleton.S_ListConnectedClient)
            {
                clientList += client.Name + key;
                clientList += client.Ip + key;
                clientList += client.Port + key;
                clientList += "Disconnected" + "\n";
            }

            await s_fileManager.FileWriter("Clients", clientList);
        }
    }
}
