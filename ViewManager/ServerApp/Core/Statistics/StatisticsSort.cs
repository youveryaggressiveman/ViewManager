using GeneralLogic.Services.Files;
using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ServerApp.Core.Statistics
{
    public class StatisticsSort
    {
        private ObservableCollection<string> _appList;
        private ObservableCollection<ServerApp.Model.Statistics> _statList;

        private readonly IFileManager _fileManager;

        public StatisticsSort()
        {
            _fileManager = new AppStatisticsFileManager();

        }

        public async Task<IEnumerable<ServerApp.Model.Statistics>> Sort(string allStat)
        {
            _appList = new ObservableCollection<string>();

            var statList = allStat.Split('\n');

            var allApp = await _fileManager.FileReader("AllowedApplications");
            var appList = allApp.Split('\n');

            foreach (var app in appList)
            {
                if (app != string.Empty)
                {
                    _appList.Add(app);
                }
            }

            foreach (var app in _appList)
            {
                foreach (var stat in statList)
                {
                    if (stat.Contains(app))
                    {
                        _statList.Add(new Model.Statistics
                        {
                            Title = "Approved",
                            Description = stat.Replace("Approved: ", ""),
                            Image = new BitmapImage(new Uri("../../../Assets/Custom/MessageBox/Icons/Confirm.png"))
                        });
                    }
                }
            }

            return _statList;
        }

        public IEnumerable<double> CountNumber(IEnumerable<ServerApp.Model.Statistics> listToCount)
        {
            var countList = new List<double>();

            double countWarning = 0;
            double countError = 0;
            double countApprowed = 0;



            foreach (var stat in listToCount)
            {
                switch (stat.Title)
                {
                    case "Warning":
                        countWarning++;
                        break;
                    case "Error":
                        countError++; 
                        break;
                    case "Approwed":
                        countApprowed++;
                        break;
                    default:
                        break;
                }
            }

            countList.Add(countWarning);
            countList.Add(countError);
            countList.Add(countApprowed);

            return countList;
        }
    }
}
