using GeneralLogic.Services.Files;
using GeneralLogic.Services.PcFeatures.ArrangeData;
using HidSharp.Utility;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using ServerApp.Controllers;
using ServerApp.Properties;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ServerApp.Assets.Custom.ComputerInfoBox
{
    /// <summary>
    /// Логика взаимодействия для CustomComputerInfoBox.xaml
    /// </summary>
    public partial class CustomComputerInfoBox : Window
    {
        private static IFileManager s_fileManger;

        private static CustomComputerInfoBox s_customComputerInfoBox;
        private static bool s_result = false;
        private static DispatcherTimer s_dispatcherTimer;

        private static readonly ObservableCollection<ObservableValue> s_observableValues = new ObservableCollection<ObservableValue>();

        private static ObservableCollection<ISeries> s_series { get; set; }

        public CustomComputerInfoBox()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;

            s_fileManger = new PcFeaturesFileManager();

            scrollPcInfo.Visibility = Visibility.Collapsed;
            descriptionTextBlock.Visibility = Visibility.Collapsed;
            chart.Visibility = Visibility.Collapsed;
            titleCPUTextBlock.Visibility = Visibility.Collapsed;
            descriptionAnotherPcTextBlock.Visibility = Visibility.Collapsed;
            gridForChart.Visibility = Visibility.Collapsed;

            s_series = new ObservableCollection<ISeries>
            {
                new LineSeries<ObservableValue>
                {
                    Values = s_observableValues,
                    Name = Settings.Default.LanguageName == "en-US" ? "CPU load:" : "Загрузка ЦПУ:",
                    Stroke = new SolidColorPaint(new SKColor(255, 207, 0)),
                    GeometryFill = new SolidColorPaint(new SKColor(255, 207, 0)),
                    GeometryStroke= new SolidColorPaint(new SKColor(36, 36, 36)),
                    Fill = new SolidColorPaint(new SKColor(255, 229, 115))
                }
            };
        }

        public async static Task<bool> Show(bool availible, string pcName)
        {
            s_customComputerInfoBox = new();
            s_customComputerInfoBox.pcNameRun.Text = pcName;
            s_customComputerInfoBox.descriptionTextBlock.Text = await s_fileManger.FileReader("Server");

            System.Timers.Timer timer = new System.Timers.Timer(5000);
            ArrangeHelper helper;

            if (availible)
            {
                s_customComputerInfoBox.descriptionTextBlock.Visibility = Visibility.Visible;
                s_customComputerInfoBox.scrollPcInfo.Visibility = Visibility.Visible;
                s_customComputerInfoBox.chart.Visibility = Visibility.Visible;
                s_customComputerInfoBox.gridForChart.Visibility = Visibility.Visible;
                s_customComputerInfoBox.titleCPUTextBlock.Visibility = Visibility.Visible;

                helper = new();

                timer.Elapsed += (sender, e) =>
                {
                    var data = helper.GetArrangeData();

                    s_observableValues.Add(new(data.Load));

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        s_customComputerInfoBox.chart.Series = s_series;
                    }); 
                };
                timer.Start();

            }
            else
            {
                s_customComputerInfoBox.descriptionAnotherPcTextBlock.Visibility = Visibility.Visible;

                s_dispatcherTimer = new DispatcherTimer();
                s_dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                s_dispatcherTimer.Tick += (object? sender, EventArgs e) =>
                {
                    s_customComputerInfoBox.descriptionAnotherPcTextBlock.Text = TcpController.S_Answer;
                };
                s_dispatcherTimer.Start();
            }

            s_customComputerInfoBox.ShowDialog();

            timer.Stop();

            return s_result;
        }

        private void DragWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {
                throw;
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            if (s_dispatcherTimer != null)
            {
                s_dispatcherTimer.Stop();
            }

            Close();
        }

        private void pcInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (s_dispatcherTimer != null)
            {
                s_dispatcherTimer.Stop();
            }

            s_result = true;
            s_customComputerInfoBox.Close();
        }
    }
}
