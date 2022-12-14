using GeneralLogic.Services.Files;
using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
            ServerApp.Model.Statistics oneStat;

            _statList = new ObservableCollection<Model.Statistics>();
            _appList = new ObservableCollection<string>();

            var nameCountList = new List<(string, int)>();
            var lastList = new List<Model.Statistics>();

            var statList = allStat.Replace("\r", string.Empty).Split("\n");

            var allApp = await _fileManager.FileReader("AllowedApplications");
            var appList = allApp.Replace("\r", string.Empty).Split("\n");

            foreach (var app in appList)
            {
                if (app != string.Empty)
                {
                    _appList.Add(app);
                }
            }

            var uniqueList = statList.Distinct();

            foreach (var app in uniqueList)
            {
                nameCountList.Add((app, 0));
            }

            foreach (var nameCount in uniqueList)
            {
                var item = (string.Empty, 0);
                foreach (var stat in statList)
                {
                    if (nameCount == stat)
                    {
                        var ge = nameCountList.FirstOrDefault(re => re.Item1 == nameCount);
                        item = (ge.Item1, item.Item2 + 1);
                    }
                }

                var splitItem = item.Item1.Split(", Client: ");
                if (lastList.Count(e => splitItem[0] == e.Title && splitItem[1] == e.ClientName) < 1)
                {
                    string title = string.Empty;
                    string imageName = string.Empty;

                    if (splitItem[0].Contains("Error: "))
                    {
                        title = "Error";
                        imageName = title;
                    }
                    else if (_appList.FirstOrDefault(e => e == splitItem[0]) != null)
                    {
                        title = "Approved";
                        imageName = "Confirm";
                    }
                    else
                    {
                        title = "Warning";
                        imageName = title;
                    }

                    lastList.Add(new Model.Statistics
                    {
                        Title = title,
                        ClientName = splitItem[1],
                        ProcessName = splitItem[0].Replace("Error: ", string.Empty),
                        Count = item.Item2,
                        Image = GetImage(imageName),
                    });

                    item = (string.Empty, 0);
                }
            }

            foreach (var item in lastList)
            {
                _statList.Add(item);
            }

            return _statList;
        }

        private BitmapImage GetImage(string value)
        {
            DirectoryInfo directoryInfo = new(@"../../../Assets/Custom/MessageBox/Icons/");

            foreach (var image in directoryInfo.GetFiles())
            {
                if (image.Name == value + ".png")
                {
                    return new BitmapImage(new Uri(image.FullName));
                }
            }

            return null;
        }

        public IEnumerable<double> CountNumber(IEnumerable<ServerApp.Model.Statistics> listToCount)
        {
            var countList = new List<double>();

            double countWarning = 0;
            double countError = 0;
            double countApproved = 0;

            foreach (var stat in listToCount)
            {
                switch (stat.Title)
                {
                    case "Warning":
                        countWarning += stat.Count;
                        break;
                    case "Error":
                        countError += stat.Count;
                        break;
                    case "Approved":
                        countApproved += stat.Count;
                        break;
                    default:
                        break;
                }
            }

            countList.Add(countWarning);
            countList.Add(countError);
            countList.Add(countApproved);

            return countList;
        }
    }
}
