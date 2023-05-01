using LiveChartsCore.Defaults;
using LiveChartsCore;
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
using GeneralLogic.Services.Files;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using GeneralLogic.Services.PcFeatures.ArrangeData;

namespace ClientApp.Assets.Custom.ComputerInfoBox
{
    /// <summary>
    /// Логика взаимодействия для CustomComputerInfoBox.xaml
    /// </summary>
    public partial class CustomComputerInfoBox : Window
    {
        private static IFileManager s_fileManger;

        private static CustomComputerInfoBox s_customComputerInfoBox;
        private static bool s_result = false;

        private static readonly ObservableCollection<ObservableValue> s_observableValues = new ObservableCollection<ObservableValue>();

        private static ObservableCollection<ISeries> s_series { get; set; }

        public CustomComputerInfoBox()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;

            s_fileManger = new PcFeaturesFileManager();

            s_series = new ObservableCollection<ISeries>
            {
                new LineSeries<ObservableValue>
                {
                    Values = s_observableValues,
                    Name = "CPU load:",
                    Stroke = new SolidColorPaint(new SKColor(255, 207, 0)),
                    GeometryFill = new SolidColorPaint(new SKColor(255, 207, 0)),
                    GeometryStroke= new SolidColorPaint(new SKColor(36, 36, 36)),
                    Fill = new SolidColorPaint(new SKColor(255, 229, 115))
                }
            };
        }

        public async static Task<bool> Show(string pcName)
        {
            s_customComputerInfoBox = new();

            s_customComputerInfoBox.pcNameRun.Text = pcName;
            s_customComputerInfoBox.descriptionTextBlock.Text = await s_fileManger.FileReader(Environment.MachineName);

            ArrangeHelper helper = new();

            System.Timers.Timer timer = new System.Timers.Timer(5000);
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
            Close();
        }

        private void pcInfoButton_Click(object sender, RoutedEventArgs e)
        {
            s_result = true;
            s_customComputerInfoBox.Close();
        }
    }
}
