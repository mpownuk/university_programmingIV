using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using RENT_A_TOOL.models;

namespace RENT_A_TOOL
{
    public partial class UpdateToolWindow : Window
    {
        private byte[]? _imageData;
        private int _toolId;
        private ToolsWindow _parentWindow;
        private Sprzęt _aktualizowanySprzet;

        public UpdateToolWindow(int toolId, ToolsWindow parentWindow)
        {
            InitializeComponent();
            _toolId = toolId;
            _parentWindow = parentWindow;
            WczytajDaneSprzetu();
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

        private void WczytajDaneSprzetu()
        {
            using (var context = new Rent_a_toolContext())
            {
                _aktualizowanySprzet = context.Sprzęt.FirstOrDefault(t => t.Id == _toolId);
                if (_aktualizowanySprzet == null)
                {
                    MessageBox.Show("Nie znaleziono sprzętu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                    return;
                }

                NazwaTextBox.Text = _aktualizowanySprzet.Nazwa;
                OpisTextBox.Text = _aktualizowanySprzet.Opis;
                StanMagazynowyTextBox.Text = _aktualizowanySprzet.StanMagazynowy.ToString();

                if (_aktualizowanySprzet.Zdjecie != null)
                {
                    _imageData = _aktualizowanySprzet.Zdjecie;
                    BitmapImage bitmap = new BitmapImage();
                    using (MemoryStream ms = new MemoryStream(_imageData))
                    {
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();
                    }
                    PreviewImage.Source = bitmap;
                    PreviewImage.Visibility = Visibility.Visible;
                }
            }
        }

        private void EdytujButton_Click(object sender, RoutedEventArgs e)
        {
            string nazwa = NazwaTextBox.Text.Trim();
            string opis = OpisTextBox.Text.Trim();
            string stanMagazynowy = StanMagazynowyTextBox.Text.Trim();

            if (string.IsNullOrEmpty(nazwa) || string.IsNullOrEmpty(opis) || string.IsNullOrEmpty(stanMagazynowy))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new Rent_a_toolContext())
            {
                var sprzet = context.Sprzęt.FirstOrDefault(t => t.Id == _toolId);
                if (sprzet == null)
                {
                    MessageBox.Show("Nie znaleziono sprzętu do aktualizacji.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                sprzet.Nazwa = nazwa;
                sprzet.Opis = opis;
                sprzet.StanMagazynowy = int.Parse(stanMagazynowy);
                if (_imageData != null)
                {
                    sprzet.Zdjecie = _imageData;
                }

                context.SaveChanges();
            }

            MessageBox.Show("Sprzęt został zaktualizowany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            _parentWindow.RefreshSprzetList();
            this.Close();
        }
    }
}
