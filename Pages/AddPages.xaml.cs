using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddPages.xaml
    /// </summary>
    public partial class AddPages : Page
    {
        private Entities.Service _currentService = null;
        private byte[] _mainImagePath;
        public AddPages()
        {
            InitializeComponent();
        }
        public AddPages(Entities.Service service)
        {
            InitializeComponent();
            _currentService = service;
            Title = "Редактирвоание услуги";
            TBoxName.Text = _currentService.Title;
            TBoxCost.Text = _currentService.Cost_s;
            TBoxDiscription.Text = _currentService.Description;
            TBoxDuration.Text = _currentService.DurationInSeconds.ToString();
            
            if (_currentService.Discount > 0)
            {
                TBoxDiscount.Text = (_currentService.Discount.Value * 100).ToString();
            }
            else
            {
                TBoxDiscount.Text = null;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(_currentService == null)
            {
                var service = new Entities.Service
                {
                    Title = TBoxName.Text,
                    Cost = decimal.Parse(TBoxCost.Text),
                    Description = TBoxDiscription.Text,
                    Discount = string.IsNullOrWhiteSpace(TBoxDiscount.Text) ? 0 : double.Parse(TBoxDiscount.Text) / 100,
                    DurationInSeconds = int.Parse(TBoxDuration.Text) *60,
                    
                };
                App.Context.Service.Add(service);
                App.Context.SaveChanges();
            }
            else
            {
                _currentService.Title = TBoxName.Text;
                _currentService.Cost = decimal.Parse(TBoxCost.Text);
                _currentService.Description = TBoxDiscription.Text;
                _currentService.DurationInSeconds = int.Parse(TBoxDuration.Text) * 60;
                _currentService.Discount = string.IsNullOrWhiteSpace(TBoxDiscount.Text) ? 0 : double.Parse(TBoxDiscount.Text) / 100;

                App.Context.SaveChanges();
            }
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image |*.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                _mainImagePath = File.ReadAllBytes(ofd.FileName);
                IMGService.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_mainImagePath);
            }
        }
    }
}
