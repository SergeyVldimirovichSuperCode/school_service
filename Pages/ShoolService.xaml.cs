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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace school_service.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShoolService.xaml
    /// </summary>
    public partial class ShoolService : Page
    {
        public ShoolService()
        {
            InitializeComponent();

            ComboDiscount.SelectedIndex = 0;
            ComboSortyBy.SelectedIndex = 0;
            Updateservice();
        }

        private void TBoxSearch_TextChange(object sender, TextChangedEventArgs e)
        {
            Updateservice();
        }

        private void ComboDiscount_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            Updateservice();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Updateservice();
        }
        private void ComboSorty_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            Updateservice();
        }
        private void Updateservice()
        {   
            var service = App.Context.Service.ToList();
            if (ComboSortyBy.SelectedIndex == 1)
                service = service.OrderBy(p => p.TotalCost).ToList();
            else
                service = service.OrderByDescending(p => p.TotalCost).ToList();
            if (ComboDiscount.SelectedIndex == 1)
                service = service.Where(p => p.Discount >= 0 && p.Discount < 0.05).ToList();
            if (ComboDiscount.SelectedIndex == 2)
                service = service.Where(p => p.Discount >= 0.05 && p.Discount < 0.15).ToList();
            if (ComboDiscount.SelectedIndex == 3)
                service = service.Where(p => p.Discount >= 0.15 && p.Discount < 0.30).ToList();
            if (ComboDiscount.SelectedIndex == 4)
                service = service.Where(p => p.Discount >= 0.30 && p.Discount < 0.70).ToList();
            if (ComboDiscount.SelectedIndex == 5)
                service = service.Where(p => p.Discount >= 0.70 && p.Discount <= 1).ToList();

            service = service.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewServices.ItemsSource = service;
        }

        private void BtnRed_Click(object sender, RoutedEventArgs e)
        {
            var currentService = (sender as Button).DataContext as Entities.Service;
            NavigationService.Navigate(new AddPages(currentService));
        }
        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var currentService = (sender as Button).DataContext as Entities.Service;
            if(MessageBox.Show($"Вы уверены что хотите удалить" + $"{currentService.Title}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Service.Remove(currentService);
                App.Context.SaveChanges();
                Updateservice();
            }
        }

        private void BtnAdd_Clck(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPages());
        }
    }
}
