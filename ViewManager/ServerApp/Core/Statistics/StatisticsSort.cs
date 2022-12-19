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

            var gg = new List<(string, int)>();
            var gh = new List<Model.Statistics>();

            var statList = allStat.Split("\r\n");

            var allApp = await _fileManager.FileReader("AllowedApplications");
            var appList = allApp.Split("\r\n");

            foreach (var app in appList)
            {
                if (app != string.Empty)
                {
                    _appList.Add(app);
                }
            }

            var temp = statList.Distinct();

            foreach (var app in temp)
            {
                gg.Add((app, 0));
            }

            foreach (var itrm2 in temp)
            {
                var t = ("", 0);
                foreach (var item in statList)
                {
                    if (itrm2 == item)
                    {
                        var ge = gg.FirstOrDefault(re => re.Item1 == itrm2);
                        t = (ge.Item1, t.Item2 + 1);
                    }
                }

                var tt = t.Item1.Split(',');
                if (gh.Count(e => tt[0] == e.Title && tt[1] == e.ClientName) < 1)
                {
                    string title = "";
                    string imageName = "";

                    if (tt[0].Contains("Error: "))
                    {
                        title = "Error";
                        imageName = title;
                    }
                    else if (_appList.FirstOrDefault(e => e == tt[0]) != null)
                    {
                        title = "Approved";
                        imageName = "Confirm";
                    }
                    else
                    {
                        title = "Warning";
                        imageName = title;
                    }

                    gh.Add(new Model.Statistics
                    {
                        Title = title,
                        ClientName = tt[1],
                        ProcessName = tt[0].Replace("Error: ", ""),
                        Count = t.Item2,
                        Image = GetImage(imageName),
                    });

                    t = ("", 0);
                }
            }

            foreach (var item in gh)
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
