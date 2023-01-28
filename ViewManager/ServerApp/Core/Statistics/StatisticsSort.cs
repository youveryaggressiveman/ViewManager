using GeneralLogic.Services.Files;
using ServerApp.Core.Singleton;
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

        public void UpdateStat(Model.Statistics value)
        {
            try
            {
                foreach (var stat in AppStatSingleton.S_ListAppStat)
                {
                    if(stat == value)
                    {
                        stat.Title = "Verified";
                        stat.Image = GetImage("Verified");
                    }
                }
            }
            catch
            {
            }
        }

        public static IEnumerable<ServerApp.Model.Statistics> Distinct(IEnumerable<ServerApp.Model.Statistics> newStatList, IEnumerable<ServerApp.Model.Statistics> oldStatList)
        {
            List<ServerApp.Model.Statistics> statList = newStatList.ToList();

            foreach (var oldStat in oldStatList)
            {
                foreach (var newStat in newStatList)
                {
                    if (oldStat.ProcessName == newStat.ProcessName && oldStat.ClientName == newStat.ClientName
                            && oldStat.Title == newStat.Title)
                    {
                        statList.Remove(newStat);
                    }
                }
            }

            statList.AddRange(oldStatList);

            return statList;
        }

        public async Task<IEnumerable<ServerApp.Model.Statistics>> Sort(string allStat)
        {
            try
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
                    if (string.IsNullOrEmpty(nameCount))
                    {
                        continue;
                    }

                    var item = (string.Empty, 0);
                    foreach (var stat in statList)
                    {
                        if (nameCount == stat)
                        {
                            var name = nameCountList.FirstOrDefault(re => re.Item1 == nameCount);
                            item = (name.Item1, item.Item2 + 1);
                        }
                    }

                    var splitItem = item.Item1.Split(", Client: ");
                    if (lastList.Count(e => splitItem[0] == e.Title && splitItem[1] == e.ClientName) < 1)
                    {
                        string title = string.Empty;
                        string imageName = string.Empty;

                        if (splitItem[0].Contains("Verified: "))
                        {
                            title = "Verified";
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
                            ProcessName = splitItem[0].Replace("Verified: ", string.Empty),
                            Count = item.Item2,
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
            catch 
            {
                return _statList;
            }       
        }

        public BitmapImage GetImage(string value)
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
                    case "Verified":
                        countWarning += stat.Count;
                        break;
                    case "Warning":
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
