﻿using GeneralLogic.Services.Files;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using ServerApp.Assets.Custom.MessageBox;
using ServerApp.Assets.Custom.StatBox;
using ServerApp.Controllers;
using ServerApp.Core.Singleton;
using ServerApp.Core.Statistics;
using ServerApp.Model;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace ServerApp.ViewModel
{
    public class StatisticsPageViewModel : BaseViewModel
    {
        private readonly IFileManager _fileManager;

        private readonly StatisticsSort _statisticsSort;

        private IEnumerable<ISeries> _series;

        private ObservableCollection<Statistics> _statList;
        private ObservableCollection<double> _countStat;

        private Statistics _selectedStat;

        private Visibility _visibilityBorder = Visibility.Collapsed;
        private Visibility _visibilityChart = Visibility.Collapsed;

        public Statistics SelectedStat
        {
            get => _selectedStat;
            set
            {
                _selectedStat = value;
                OnPropertyChanged(nameof(SelectedStat));

                SelectStat();
            }
        }

        public ObservableCollection<Statistics> StatList
        {
            get => _statList;
            set
            {
                _statList = value;
                OnPropertyChanged(nameof(StatList));
            }
        }

        public Visibility VisibilityChart
        {
            get => _visibilityChart;
            set
            {
                _visibilityChart = value;
                OnPropertyChanged(nameof(VisibilityChart));
            }
        }

        public Visibility VisibilityBorder
        {
            get => _visibilityBorder;
            set
            {
                _visibilityBorder = value;
                OnPropertyChanged(nameof(VisibilityBorder));
            }
        }

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

            _statisticsSort = new();

            _fileManager = new AppStatisticsFileManager();


            LoadStat();
        }

        private async void SelectStat()
        {
            var client = ConnectedClientSingleton.S_ListConnectedClient.FirstOrDefault(user => user.Name == SelectedStat.ClientName);

            if (client == null || SelectedStat == null)
            {
                return;
            }

            if (!CustomStatBox.Show(SelectedStat.ProcessName, SelectedStat.ClientName, SelectedStat.Title))
            {
                try
                {
                    await TcpController.SendMessage(client, $"5, App: {SelectedStat.ProcessName}");
                }
                catch
                {
                    CustomMessageBox.Show("An error occurred when trying to close the program on the client side.", Assets.Custom.MessageBox.Basic.Titles.Warning, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
                }
                
            }

            _statisticsSort.UpdateStat(SelectedStat);

            LoadStat();
        }

        private void SetBorderInfo()
        {
            double statCount = 0;

            foreach (var stat in CountStat)
            {
                statCount += stat;
            }

            if (statCount == 0)
            {
                VisibilityBorder = Visibility.Visible;
                VisibilityChart = Visibility.Collapsed;
            }
            else
            {
                VisibilityBorder = Visibility.Collapsed;
                VisibilityChart = Visibility.Visible;
            }
        }

        private async void LoadStat()
        {
            StatList = new ObservableCollection<Statistics>();
            CountStat = new ObservableCollection<double>() { 0 ,0 ,0 };

            try
            {
                if(AppStatSingleton.S_ListAppStat == null)
                {
                    return;
                }

                AppStatSingleton.S_ListAppStat.ToList().ForEach(StatList.Add);

                CountStat.Clear();

                _statisticsSort.CountNumber(StatList).ToList().ForEach(CountStat.Add);
                
            }
            catch
            {
                CustomMessageBox.Show("The data file could not be read.", Assets.Custom.MessageBox.Basic.Titles.Error, Assets.Custom.MessageBox.Basic.Buttons.Ok, Assets.Custom.MessageBox.Basic.Buttons.Nothing);
            }
            finally
            {
                LoadStatisticToChart();
                SetBorderInfo();
            }
        }

        private void LoadStatisticToChart()
        {
            Series = new ISeries[]
            {
                 new PieSeries<double> { Values = new double[] { CountStat[0] }, Name = "Verified", Fill= new SolidColorPaint(new SKColor(100, 149, 237))},
                 new PieSeries<double> { Values = new double[] { CountStat[1] }, Name = "Warning", Fill = new SolidColorPaint(new SKColor(238, 32, 77))},
                 new PieSeries<double> { Values = new double[] { CountStat[2] }, Name = "Approved", Fill = new SolidColorPaint(new SKColor(168, 228, 160))}
            };
        }
    }
}
