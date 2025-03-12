
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
using System.Windows.Shapes;
using Microsoft.Win32;
using RENT_A_TOOL.models;

namespace RENT_A_TOOL
{
    /// <summary>
    /// Logika interakcji dla klasy AddToolWindow.xaml
    /// </summary>
    public partial class AddToolWindow : Window
    {
        private byte[]? _imageData;

        private ToolsWindow _parentWindow;

        public AddToolWindow(ToolsWindow parentWindow)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
        }
        private void WybierzZdjecieButton_Click(object sender, RoutedEventArgs e)
        {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Title = "Wybierz zdjęcie",
            Filter = "Obrazy (*.jpg;*.png;*.jpeg)|*.jpg;*.png;*.jpeg"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            _imageData = File.ReadAllBytes(openFileDialog.FileName);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(openFileDialog.FileName);
            bitmap.EndInit();

            PreviewImage.Source = bitmap;
            PreviewImage.Visibility = Visibility.Visible;
        }
    }

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            string nazwa = NazwaTextBox.Text.Trim();
            string opis = OpisTextBox.Text.Trim();
            string stanMagazynowy = StanMagazynowyTextBox.Text.Trim();

            if (string.IsNullOrEmpty(nazwa) || string.IsNullOrEmpty(opis) || string.IsNullOrEmpty(stanMagazynowy))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_imageData == null)
            {
                MessageBox.Show("Należy wybrać zdjęcie!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new Rent_a_toolContext())
            {
                Sprzęt nowySprzet = new Sprzęt
                {
                    Nazwa = nazwa,
                    Opis = opis,
                    StanMagazynowy = stanMagazynowy,
                    Zdjecie = _imageData
                };

                context.Sprzęt.Add(nowySprzet);
                context.SaveChanges();
            }

            MessageBox.Show("Sprzęt został dodany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            _parentWindow.RefreshSprzetList();
            this.Close();
        }
        }
    }