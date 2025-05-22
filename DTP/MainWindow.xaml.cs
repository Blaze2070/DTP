using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;

namespace DTP
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Accident> AccidentList { get; set; }
        public Accident SelectedAccident { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            AccidentList = new ObservableCollection<Accident>(DTPEntities.GetContext().Accident.ToList());
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddInfo();
            if (addWindow.ShowDialog() == true)
            {
                MessageBox.Show("Происшествие добавлено!");
                UpdateAccidentList();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAccident != null)
            {
                var editWindow = new EditInfo(SelectedAccident);
                if (editWindow.ShowDialog() == true)
                {
                    MessageBox.Show("Происшествие обновлено!");
                    UpdateAccidentList();
                }
            }
            else
            {
                MessageBox.Show("Выберите происшествие для редактирования.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedAccident != null)
                {
                    if (MessageBox.Show($"Подтвердить удаление происшествия '{SelectedAccident.Description}'?", "Подтверждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        DTPEntities.GetContext().Accident.Remove(SelectedAccident);
                        DTPEntities.GetContext().SaveChanges();
                        AccidentList.Remove(SelectedAccident);
                        SelectedAccident = null;
                        MessageBox.Show("Происшествие удалено!");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите происшествие для удаления.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message);
            }
        }

        private void UpdateAccidentList()
        {
            AccidentList.Clear();
            foreach (var accident in DTPEntities.GetContext().Accident.ToList())
            {
                AccidentList.Add(accident);
            }
        }
    }
}