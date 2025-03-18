using System;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace RENT_A_TOOL
{
    public partial class AddToolWindow : Window
    {
        private byte[]? _imageData;
        private ToolsWindow _parentWindow;
        private string _connectionString;

        public AddToolWindow(ToolsWindow parentWindow, string connectionString)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
            _connectionString = connectionString;
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
            string stanMagazynowyText = StanMagazynowyTextBox.Text.Trim();

            if (string.IsNullOrEmpty(nazwa) || string.IsNullOrEmpty(opis) || string.IsNullOrEmpty(stanMagazynowyText))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_imageData == null)
            {
                MessageBox.Show("Należy wybrać zdjęcie!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(stanMagazynowyText, out int stanMagazynowy) || stanMagazynowy < 0)
            {
                MessageBox.Show("Stan magazynowy musi być liczbą całkowitą większą lub równą zero!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Sprzęt (Nazwa, Opis, StanMagazynowy, Zdjecie) VALUES (@Nazwa, @Opis, @StanMagazynowy, @Zdjecie)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nazwa", nazwa);
                        cmd.Parameters.AddWithValue("@Opis", opis);
                        cmd.Parameters.AddWithValue("@StanMagazynowy", stanMagazynowy);
                        cmd.Parameters.AddWithValue("@Zdjecie", _imageData);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Sprzęt został dodany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                _parentWindow.RefreshSprzetList();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas dodawania sprzętu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
