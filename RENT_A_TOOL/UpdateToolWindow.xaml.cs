using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace RENT_A_TOOL
{
    public partial class UpdateToolWindow : Window
    {
        private byte[]? _imageData;
        private int _toolId;
        private ToolsWindow _parentWindow;
        private string _connectionString;

        public UpdateToolWindow(int toolId, ToolsWindow parentWindow, string connectionString)
        {
            InitializeComponent();
            _toolId = toolId;
            _parentWindow = parentWindow;
            _connectionString = connectionString;
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
            string query = "SELECT Nazwa, Opis, StanMagazynowy, Zdjecie FROM Sprzęt WHERE Id = @ToolId";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ToolId", _toolId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                NazwaTextBox.Text = reader.GetString(0);
                                OpisTextBox.Text = reader.GetString(1);
                                StanMagazynowyTextBox.Text = reader.GetInt32(2).ToString();

                                if (!reader.IsDBNull(3))
                                {
                                    _imageData = (byte[])reader[3];
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
                            else
                            {
                                MessageBox.Show("Nie znaleziono sprzętu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                                this.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas pobierania danych sprzętu: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EdytujButton_Click(object sender, RoutedEventArgs e)
        {
            string nazwa = NazwaTextBox.Text.Trim();
            string opis = OpisTextBox.Text.Trim();
            string stanMagazynowyText = StanMagazynowyTextBox.Text.Trim();

            if (string.IsNullOrEmpty(nazwa) || string.IsNullOrEmpty(opis) || string.IsNullOrEmpty(stanMagazynowyText))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(stanMagazynowyText, out int stanMagazynowy))
            {
                MessageBox.Show("Stan magazynowy musi być liczbą!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string query = _imageData != null
                ? "UPDATE Sprzęt SET Nazwa = @Nazwa, Opis = @Opis, StanMagazynowy = @StanMagazynowy, Zdjecie = @Zdjecie WHERE Id = @ToolId"
                : "UPDATE Sprzęt SET Nazwa = @Nazwa, Opis = @Opis, StanMagazynowy = @StanMagazynowy WHERE Id = @ToolId";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nazwa", nazwa);
                        cmd.Parameters.AddWithValue("@Opis", opis);
                        cmd.Parameters.AddWithValue("@StanMagazynowy", stanMagazynowy);
                        cmd.Parameters.AddWithValue("@ToolId", _toolId);

                        if (_imageData != null)
                        {
                            cmd.Parameters.Add("@Zdjecie", SqlDbType.VarBinary).Value = _imageData;
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sprzęt został zaktualizowany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                            _parentWindow.RefreshSprzetList();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Nie znaleziono sprzętu do aktualizacji.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas aktualizacji sprzętu: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
