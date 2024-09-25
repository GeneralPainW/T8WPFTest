using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static T8WPFTest.MainWindow;

namespace T8WPFTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<OpticalSegment> segments = new List<OpticalSegment>();

        public double GetAttenuation()
        {
            return double.Parse(textBoxAttenuation.Text);
        }

        public double GetGain()
        {
            return double.Parse(textBoxGain.Text);
        }

        public double GetNoise()
        {
            return double.Parse(textBoxNoise.Text);
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(textBoxN.Text, out int N);

            if (radioButtonNo.IsChecked == true)
            {
                radioButtonNo.IsEnabled = false;
                radioButtonYes.IsEnabled = false;

                if (N > 1)
                {
                    segments.Add(new OpticalSegment(GetAttenuation(), GetGain(), GetNoise()));
                    textBoxAttenuation.Text = string.Empty;
                    textBoxGain.Text = string.Empty;
                    textBoxNoise.Text = string.Empty;
                    textBlockN.Text = "Осталось пролетов:";
                    textBoxN.Text = $"{N - 1}";
                }
                else if (N == 1)
                {
                    segments.Add(new OpticalSegment(GetAttenuation(), GetGain(), GetNoise()));
                    textBlockN.Text = "Количество пролетов (N):";
                    buttonCalculate.IsEnabled = true;
                    buttonAdd.IsEnabled = false;
                }
            }
            else if (radioButtonYes.IsChecked == true)
            {
                radioButtonNo.IsEnabled = false;
                radioButtonYes.IsEnabled = false;

                while (N > 0)
                {
                    for (int i = 0; i < N; i++)
                    {
                        segments.Add(new OpticalSegment(GetAttenuation(), GetGain(), GetNoise()));
                    }
                    N--;
                }
                buttonCalculate.IsEnabled = true;
                buttonAdd.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Не выбрано, одинаковые ли параметры линии.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void buttonCalculate_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(textBoxN.Text, out int N);
            double powerInput = double.Parse(textBoxPower.Text);

            OpticalLine opticalLine = new OpticalLine(powerInput, segments);

            double powerOutput = opticalLine.CalculateSignalPower();
            double nPowerOutput = opticalLine.CalculateNoisePower();
            double osnr = opticalLine.CalculateOSNR();

            textBlockResult.Text = $"Мощность на выходе линии: {powerOutput:F2} дБм\n" +
                                   $"Мощность накопленного шума: {nPowerOutput:F2} дБм\n" +
                                   $"OSNR: {osnr:F2} дБ";

            buttonCalculate.IsEnabled = false;
            buttonClear.IsEnabled = true;
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxN.Text = string.Empty;
            textBlockResult.Text = string.Empty;
            textBoxAttenuation.Text = string.Empty;
            textBoxGain.Text = string.Empty;
            textBoxNoise.Text = string.Empty;
            textBoxPower.Text = string.Empty;
            buttonAdd.IsEnabled = true;
            radioButtonNo.IsEnabled = true;
            radioButtonNo.IsChecked = false;
            radioButtonYes.IsEnabled = true;
            radioButtonYes.IsChecked = false;
            buttonClear.IsEnabled = false;
        }
    }
}
