using GeneralLogic.Services.Files;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using ServerApp.Assets.Custom.MessageBox;
using ServerApp.Model;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ServerApp.ViewModel
{
    public class StatisticsPageViewModel : BaseViewModel
    {
        private IEnumerable<ISeries> _series;

        private ObservableCollection<double> _countStat;

        public ObservableCollection<double> CountStat
        {
            get => _countStat;
            set
            {
                _countStat= value;
                OnPropertyChanged(nameof(CountStat));
            }
        }

        public IEnumerable<ISeries> Series
        {
            get => _series;
            set
            {
                _series = value;
                OnPropertyChanged(nameof(Series));
            }
        }

        public StatisticsPageViewModel()
        {
            Series= new ObservableCollection<ISeries>();

            CountStat = new ObservableCollection<double>()
            {
                0,
                0,
                0,
            };

            LoadStatisticToChart();
            LoadStat();
        }

        private async void LoadStat()
        {
            try
            {
                var allLogs = await LogManager.ReadLog("Server", DateTime.Today);
            }
            catch
            {
                CustomMessageBox.Show("The data file could not be read.", Assets.Custom.MessageBox.Basic.Titles.Error, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
            }
        }

        private void LoadStatisticToChart()
        {
            Series = new ISeries[]
            {
                 new PieSeries<double> { Values = new double[] { CountStat[0] }, Name = "Warning" },
                 new PieSeries<double> { Values = new double[] { CountStat[1] }, Name = "Danger" },
                 new PieSeries<double> { Values = new double[] { CountStat[2] }, Name = "Approved" }
            };
        }
    }
}
