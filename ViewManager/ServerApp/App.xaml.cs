using GeneralLogic.Model;
using GeneralLogic.Services.Files;
using GeneralLogic.Services.Translator;
using Microsoft.Win32;
using ServerApp.Controllers;
using ServerApp.Core.Singleton;
using ServerApp.Model;
using ServerApp.Properties;
using ServerApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ServerApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IFileManager _fileManager;

        private UniversalController<Role> _roleController;
        private UniversalController<Office> _officeCintroller;
        private UniversalController<Specialization> _specializtionController;

        private List<string> _listData;

        protected async override void OnStartup(StartupEventArgs e)
        {
            var lang = Settings.Default.LanguageName;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);

            GeneralLogic.Services.PcFeatures.TaskManager.Dispatcher.SetAutoRunValue(true, Assembly.GetExecutingAssembly().Location.Replace("dll", "exe"), "ServerApp");

            _fileManager = new AppStatisticsFileManager();

            _listData = new List<string>();
            List<TrsanslationModel> listSentences = new List<TrsanslationModel>();

            var fileTranslation = string.Empty;

            _roleController = new UniversalController<Role>(ApiServerSingleton.GetConnectionApiString());
            _officeCintroller = new UniversalController<Office>(ApiServerSingleton.GetConnectionApiString());
            _specializtionController = new UniversalController<Specialization>(ApiServerSingleton.GetConnectionApiString());

            foreach (var role in await _roleController.GetList())
            {
                _listData.Add(role.Value);
            }
            foreach (var office in await _officeCintroller.GetList())
            {
                _listData.Add(office.Value);
            }
            foreach (var spec in await _specializtionController.GetList())
            {
                _listData.Add(spec.Value);
            }

            try
            {
               await _fileManager.FileWriter("Translation", null);

                fileTranslation = await _fileManager.FileReader("Translation");

                foreach (var data in _listData)
                {
                    if (!fileTranslation.Contains(data))
                    {
                        listSentences.Add(await TranslatorController.GetTranslation(data, "en", "ru"));
                    }
                }

                foreach (var sentence in listSentences)
                {
                    fileTranslation += $"ru: {sentence.Translation} || en: {sentence.Data}\n";
                }

                await _fileManager.FileWriter("Translation", fileTranslation);
            }
            catch (Exception ex)
            {
                LogManager.SaveLog("Server", DateTime.Today, $"Translation: {ex.Message}.");
            }
            finally
            {
                var arrayTranslation = fileTranslation.Split("\n");

                foreach (var translation in arrayTranslation) 
                {
                    TrsanslationModel translationModel = new();

                    var endTranslation = translation.Split(" || ");

                    foreach (var endData in endTranslation)
                    {
                        if (endData.Contains("ru"))
                        {
                            translationModel.Translation = endData.Replace("ru: ", "");
                        }
                        else
                        {
                            translationModel.Data = endData.Replace("en: ", "");
                        }
                    }

                    TranslationSingleton.S_TranslationList.Add(translationModel);
                }

                base.OnStartup(e);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _fileManager = new AppStatisticsFileManager();

            SaveClient(_fileManager);
            SaveStat(_fileManager);

            base.OnExit(e);
        }

        private async void SaveStat(IFileManager fileManager)
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

            await fileManager.FileWriter("Statistics", allAppStat);
        }

        private async void SaveClient(IFileManager fileManager)
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

            await fileManager.FileWriter("Clients", clientList);
        }
    }
}
