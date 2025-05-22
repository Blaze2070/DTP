using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DTP
{
    public partial class EditInfo : Window
    {
        private Accident _currentAccident;
        public Driver SelectedDriver { get; set; }
        public ObservableCollection<Driver> FilteredDrivers {  get; set; }
        public Passenger SelectedPassenger { get; set; }
        public ObservableCollection<Passenger> FilteredPassengers {  get; set; }
        public Police_Officer SelectedPolice { get; set; }
        public ObservableCollection<Police_Officer> FilteredPolice { get; set; }
        public Vehicle SelectedVehicle { get; set; }
        public ObservableCollection<Vehicle> FilteredVehicle { get; set; }
        public Material_Damage SelectedDamage { get; set; }
        public ObservableCollection<Material_Damage> FilteredDamage {  get; set; }

        public Accident Accident => _currentAccident;
        public EditInfo(Accident selectedAccident)
        {
            InitializeComponent();
            if (selectedAccident == null) return;
            _currentAccident = selectedAccident;
            DataContext = this;
            WeatherComboBox.SelectedIndex = _currentAccident.id_weather_conditions;
            FilteredDrivers = new ObservableCollection<Driver>(
                    DTPEntities.GetAllDriver().Where(d => d.Accident.Any(a => a.Accident_id == _currentAccident.Accident_id))
                );
            FilteredPassengers = new ObservableCollection<Passenger>(
                    DTPEntities.GetAllPassengers().Where(d => d.Accident.Any(a => a.Accident_id == _currentAccident.Accident_id))
                );
            FilteredPolice = new ObservableCollection<Police_Officer>(
                    DTPEntities.GetAllPolice().Where(d => d.Accident.Any(a => a.Accident_id == _currentAccident.Accident_id))
                );
            FilteredVehicle = new ObservableCollection<Vehicle>(
                    DTPEntities.GetAllVehicle().Where(d => d.Accident.Any(a => a.Accident_id == _currentAccident.Accident_id))
                );
            FilteredDamage = new ObservableCollection<Material_Damage>(
                    DTPEntities.GetAllDamage().Where(d => d.id_Accident == _currentAccident.Accident_id)
                );
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var accident = DTPEntities.GetContext().Accident.FirstOrDefault(a => a.Accident_id == _currentAccident.Accident_id);
                if (!string.IsNullOrWhiteSpace(Dfirstname.Text) &&
            !string.IsNullOrWhiteSpace(Dlastname.Text) &&
            Dbirth.SelectedDate != null &&
            !string.IsNullOrWhiteSpace(Dexp.Text) &&
            !string.IsNullOrWhiteSpace(Dlicense.Text))
                {
                    if (int.TryParse(Dexp.Text, out int exp))
                    {
                        var driver = new Driver()
                        {
                            First_Name = Dfirstname.Text,
                            Last_Name = Dlastname.Text,
                            Date_of_Birth = (DateTime)Dbirth.SelectedDate,
                            Driving_Experience = exp,
                            Drivers_License = Dlicense.Text
                        };
                        driver.Accident.Add(accident);
                        DTPEntities.GetContext().Driver.Add(driver);
                        FilteredDrivers.Add(driver);
                        Dfirstname.Clear();
                        Dlastname.Clear();
                        Dbirth.SelectedDate = null;
                        Dexp.Clear();
                        Dlicense.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Введите корректные значения");
                    }
                    
                }
                if (!string.IsNullOrWhiteSpace(Pfirstname.Text) &&
            !string.IsNullOrWhiteSpace(Plastname.Text) &&
            Pbirth.SelectedDate != null &&
            !string.IsNullOrWhiteSpace(Pinjury.Text))
                {
                    var passenger = new Passenger()
                    {
                        First_Name = Pfirstname.Text,
                        Last_Name = Plastname.Text,
                        Date_of_Birth = (DateTime)Pbirth.SelectedDate,
                        Injury_Status = Pinjury.Text
                    };
                    passenger.Accident.Add(accident);
                    DTPEntities.GetContext().Passenger.Add(passenger);
                    FilteredPassengers.Add(passenger);
                    Pfirstname.Clear();
                    Plastname.Clear();
                    Pbirth.SelectedDate = null;
                    Pinjury.Clear();
                }
                if (!string.IsNullOrWhiteSpace(PoliceFirstName.Text) &&
            !string.IsNullOrWhiteSpace(PoliceLastName.Text) &&
            !string.IsNullOrWhiteSpace(PoliceRank.Text) &&
            !string.IsNullOrWhiteSpace(PoliceBadgeNumber.Text))
                {
                    
                    var police = new Police_Officer()
                    {
                        First_Name = PoliceFirstName.Text,
                        Last_Name = PoliceLastName.Text,
                        Rank = PoliceRank.Text,
                        Badge_Number = PoliceBadgeNumber.Text,
                    };
                    police.Accident.Add(accident);
                    DTPEntities.GetContext().Police_Officer.Add(police);
                    FilteredPolice.Add(police);
                    PoliceFirstName.Clear();
                    PoliceLastName.Clear();
                    PoliceRank.Clear();
                    PoliceBadgeNumber.Clear();
                }
                if (!string.IsNullOrWhiteSpace(VehicleLicensePlateNumber.Text) &&
            !string.IsNullOrWhiteSpace(VehicleMake.Text) &&
            !string.IsNullOrWhiteSpace(VehicleModel.Text) &&
            int.TryParse(VehicleYearOfManufacture.Text, out int Year))
                {

                    var vehicle = new Vehicle()
                    {
                        License_Plate_Number = VehicleLicensePlateNumber.Text,
                        Make = VehicleMake.Text,
                        Model = VehicleModel.Text,
                        Year_of_Manufacture = Year
                    };
                    vehicle.Accident.Add(accident);
                    DTPEntities.GetContext().Vehicle.Add(vehicle);
                    FilteredVehicle.Add(vehicle);
                    VehicleLicensePlateNumber.Clear();
                    VehicleMake.Clear();
                    VehicleModel.Clear();
                    VehicleYearOfManufacture.Clear();
                }
                if (!string.IsNullOrWhiteSpace(DamageDescription.Text) &&
            int.TryParse(EstimatedCost.Text, out int Cost))
                {

                    var damage = new Material_Damage()
                    {
                        Damage_Description = DamageDescription.Text,
                        Estimated_Cost = Cost,
                        id_Accident = _currentAccident.Accident_id
                        
                    };
                    DTPEntities.GetContext().Material_Damage.Add(damage);
                    FilteredDamage.Add(damage);
                    DamageDescription.Clear();
                    EstimatedCost.Clear();
                }
                if (WeatherComboBox.SelectedItem is Weather_Conditions selectedWeather)
                {
                    accident.id_weather_conditions = selectedWeather.Weather_Condition_ID - 1;
                }
                if (WeatherComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите погодные условия!");
                    WeatherComboBox.Focus();
                    return;
                }
                DTPEntities.GetContext().SaveChanges();
                MessageBox.Show("Изменилось!");
            }
            //catch (DbUpdateException ex)
            //{
            //    MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            //}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void ButtonEditBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void AddWeather_Click(object sender, RoutedEventArgs e)
        {
            var AddWeatherWindow = new AddWeatherWindow();
            if (AddWeatherWindow.ShowDialog() == true)
            {
                var newWeather = AddWeatherWindow.NewWeatherCondition;

                DTPEntities.GetContext().Weather_Conditions.Add(newWeather);
                DTPEntities.GetContext().SaveChanges();
            }
            WeatherComboBox.ItemsSource = null;
            WeatherComboBox.ItemsSource = DTPEntities.GetContext().Weather_Conditions.ToList();
        }
        private void DeleteDriverButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedDriver != null)
                {
                    if (MessageBox.Show($"Подтвердить удаление водителя?", "Подтверждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var driverInDb = DTPEntities.GetContext().Driver.Find(SelectedDriver.Driver_ID);
                        if (driverInDb != null)
                        {
                            DTPEntities.GetContext().Driver.Remove(driverInDb);
                            DTPEntities.GetContext().SaveChanges();
                            FilteredDrivers.Remove(driverInDb);
                            MessageBox.Show("Водитель удален!");
                        }
                        else
                        {
                            MessageBox.Show("Водитель не найден в базе.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите водителя для удаления.");
                }
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                if (innerException != null)
                {
                    MessageBox.Show(innerException.Message);
                }
            }
        }
        private void DeletePassengerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedPassenger != null)
                {
                    if (MessageBox.Show($"Подтвердить удаление пассажира?", "Подтверждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var passengerInDb = DTPEntities.GetContext().Passenger.Find(SelectedPassenger.Passenger_ID);
                        if (passengerInDb != null)
                        {
                            DTPEntities.GetContext().Passenger.Remove(passengerInDb);
                            DTPEntities.GetContext().SaveChanges();
                            FilteredPassengers.Remove(passengerInDb);
                            MessageBox.Show("Пассажир удален!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите пассажира для удаления.");
                }
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                if (innerException != null)
                {
                    MessageBox.Show(innerException.Message);
                }
            }
        }
        private void DeletePoliceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedPolice != null)
                {
                    if (MessageBox.Show($"Подтвердить удаление полицейского?", "Подтверждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var policeInDb = DTPEntities.GetContext().Police_Officer.Find(SelectedPolice.Officer_ID);
                        if (policeInDb != null)
                        {
                            DTPEntities.GetContext().Police_Officer.Remove(policeInDb);
                            DTPEntities.GetContext().SaveChanges();
                            FilteredPolice.Remove(policeInDb);
                            MessageBox.Show("Полицейский удален!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите полицейского для удаления.");
                }
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                if (innerException != null)
                {
                    MessageBox.Show(innerException.Message);
                }
            }
        }

        private void DeleteVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedVehicle != null)
                {
                    if (MessageBox.Show($"Подтвердить удаление транспорта?", "Подтверждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var vehicleInDb = DTPEntities.GetContext().Vehicle.Find(SelectedVehicle.Vehicle_ID);
                        if (vehicleInDb != null)
                        {
                            DTPEntities.GetContext().Vehicle.Remove(vehicleInDb);
                            DTPEntities.GetContext().SaveChanges();
                            FilteredVehicle.Remove(vehicleInDb);
                            MessageBox.Show("Транспорт удален!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите транспорт для удаления.");
                }
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                if (innerException != null)
                {
                    MessageBox.Show(innerException.Message);
                }
            }
        }

        private void DeleteDamageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedDamage != null)
                {
                    if (MessageBox.Show($"Подтвердить удаление повреждения?", "Подтверждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var damageInDb = DTPEntities.GetContext().Material_Damage.Find(SelectedDamage.Damage_ID);
                        if (damageInDb != null)
                        {
                            DTPEntities.GetContext().Material_Damage.Remove(damageInDb);
                            DTPEntities.GetContext().SaveChanges();
                            FilteredDamage.Remove(damageInDb);
                            MessageBox.Show("Повреждение удалено!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите повреждение для удаления.");
                }
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                if (innerException != null)
                {
                    MessageBox.Show(innerException.Message);
                }
            }
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

        private void TextBoxEditName_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
        private void VehicleYearOfManufacture_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }
        private void VehicleYearOfManufacture_LostFocus(object sender, RoutedEventArgs e)
        {
            int year;
            if (int.TryParse(VehicleYearOfManufacture.Text, out year))
            {
                int currentYear = DateTime.Now.Year;
                if (year < 1950 || year > currentYear)
                {
                    MessageBox.Show($"Введите год в диапазоне от 1950 до {currentYear}");
                    VehicleYearOfManufacture.Text = "";
                }
            }
            else if (!string.IsNullOrWhiteSpace(VehicleYearOfManufacture.Text))
            {
                MessageBox.Show("Введите корректный год.");
                VehicleYearOfManufacture.Text = "";
            }
        }
        private void Dexp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }
        private void Dexp_LostFocus(object sender, RoutedEventArgs e)
        {
            int exp, max = 100;
            if (int.TryParse(Dexp.Text, out exp))
            {
                if (exp < 0 || exp > max)
                {
                    MessageBox.Show($"Стаж должен быть от 0 до {max} лет.");
                    Dexp.Text = "";
                }
            }
        }
        private void Dbirth_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Dbirth.SelectedDate.HasValue)
            {
                var date = Dbirth.SelectedDate.Value;
                var now = DateTime.Now;
                int age = now.Year - date.Year;
                if (date > now.AddYears(-age)) age--;
                if (age < 18)
                {
                    MessageBox.Show("Водитель должен быть старше 18 лет.");
                    Dbirth.SelectedDate = null;
                }
            }
        }
    }
}
