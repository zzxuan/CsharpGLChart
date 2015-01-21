using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GLFormsChart;
using GLFormsChart.ChartModels;

namespace CsharpGlChartSample
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            chart.Init(0, 0, 100, 100);

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            GLLineItem line = new GLLineItem(
                new double[] { 0, 1, 2, 3, 4, 5, 6, 90 },
                new double[] { 5, 1, 2, 55, 4, 5, 42, 35 },
                System.Drawing.Color.AliceBlue);
            chart.AddGLitems(line);
            chart.Refresh();
        }
    }
}
