using System;
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
using System.Windows.Shapes;

namespace DTP
{
    /// <summary>
    /// Логика взаимодействия для AddWeatherWindow.xaml
    /// </summary>
    public partial class AddWeatherWindow : Window
    {
        public Weather_Conditions NewWeatherCondition { get; private set; }
        public AddWeatherWindow()
        {
            InitializeComponent();
        }
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            decimal tempValue;
            if (!decimal.TryParse(tbTemperature.Text, out tempValue))
            {
                MessageBox.Show("Введите корректное число в поле Температура.");
                return;
            }

            NewWeatherCondition = new Weather_Conditions
            {
                Temperature = tempValue,
                Precipitation = tbPrecipitation.Text,
                Light_Conditions = tbLight.Text
            };
            DialogResult = true;
            Close();
        }
        private void TextBoxCourse_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c) && c != '.' && c != '/' && c != '-' && c != ',')
                {
                    e.Handled = true;
                }
            }
        }

        private void TextBoxName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    e.Handled = true;
                    break;
                }
            }
        }
    }
}
