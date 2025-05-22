using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для AddInfo.xaml
    /// </summary>
    public partial class AddInfo : Window
    {
        private readonly Accident _currentAccident = new Accident();

        private readonly DTPEntities _bd = new DTPEntities();
        public AddInfo()
        {
            InitializeComponent();
            DataContext = _currentAccident;

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var flag = true;
               
                foreach (var control in Items.Children)
                {
                    if (control is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        flag = false;
                    }
                    else if (control is ComboBox comboBox && string.IsNullOrWhiteSpace(comboBox.Text))
                    {
                        flag = false;
                    }
                }
                var selectedWeather = WeatherComboBox.SelectedItem as Weather_Conditions;
                if (selectedWeather != null)
                {
                    _currentAccident.id_weather_conditions = selectedWeather.Weather_Condition_ID;
                }
                else
                {
                    flag = false;
                }
                if (flag)
                {
                    _bd.Accident.Add(_currentAccident);
                    _bd.SaveChanges();
                    DialogResult = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");
                }
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException?.InnerException?.Message ?? ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Ошибка");
            }
        }
        private void AddWeather_Click(object sender, RoutedEventArgs e)
        {
            var addWeatherWindow = new AddWeatherWindow();
            if (addWeatherWindow.ShowDialog() == true)
            {
                var newWeather = addWeatherWindow.NewWeatherCondition;

                _bd.Weather_Conditions.Add(newWeather);
                _bd.SaveChanges();
            }
            WeatherComboBox.ItemsSource = null;
            WeatherComboBox.ItemsSource = _bd.Weather_Conditions.ToList();
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

