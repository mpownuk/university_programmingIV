using System;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RENT_A_TOOL
{
    public partial class RentToolWindow : Window
    {
        private int _userId;
        private int _toolId;
        private string _toolName;
        private ToolsWindow _toolsWindow;
        private string _userName;
        private string _connectionString;

        public RentToolWindow(int userId, int toolId, string toolName, string userName, ToolsWindow toolsWindow, string connectionString)
        {
            InitializeComponent();
            _userId = userId;
            _toolId = toolId;
            _toolName = toolName;
            _toolsWindow = toolsWindow;
            _userName = userName;
            _connectionString = connectionString;

            ToolNameText.Text = $"Wypożyczenie: {_toolName}";
            CheckAdminPrivileges();
        }

        private void CheckAdminPrivileges()
        {
            if (_userName.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
            {
                RentGrid.Children.Add(CreateAdminButton("Edytuj sprzęt", Brushes.Blue, UpdateTool_Click, 5, HorizontalAlignment.Left));
                RentGrid.Children.Add(CreateAdminButton("Usuń sprzęt", Brushes.Red, DeleteTool_Click, 5, HorizontalAlignment.Right));
            }
        }

        private Button CreateAdminButton(string content, Brush color, RoutedEventHandler clickHandler, int row, HorizontalAlignment alignment)
        {
            Button button = new Button
            {
                Content = content,
                FontSize = 14,
                Width = 100,
                Padding = new Thickness(10),
                Margin = new Thickness(10),
                HorizontalAlignment = alignment,
                VerticalAlignment = VerticalAlignment.Bottom,
                Background = color,
                Foreground = Brushes.White
            };

            button.Click += clickHandler;
            Grid.SetRow(button, row);
            return button;
        }

        private void UpdateTool_Click(object sender, RoutedEventArgs e)
        {
            new UpdateToolWindow(_toolId, _toolsWindow, _connectionString).Show();
            Close();
        }

        private void DeleteTool_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Czy na pewno chcesz usunąć narzędzie: {_toolName}?",
                                                      "Potwierdzenie usunięcia",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();
                        string deleteQuery = "DELETE FROM Sprzęt WHERE Id = @ToolId";
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@ToolId", _toolId);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Sprzęt został pomyślnie usunięty!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                                _toolsWindow.RefreshSprzetList();
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Nie znaleziono sprzętu do usunięcia.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas usuwania sprzętu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ConfirmRent_Click(object sender, RoutedEventArgs e)
        {
            if (!RentDatePicker.SelectedDate.HasValue || !ReturnDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Proszę wybrać daty wypożyczenia i zwrotu.");
                return;
            }

            DateTime rentDate = RentDatePicker.SelectedDate.Value;
            DateTime returnDate = ReturnDatePicker.SelectedDate.Value;

            if (returnDate <= rentDate)
            {
                MessageBox.Show("Data zwrotu musi być późniejsza niż data wypożyczenia.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // Sprawdzenie czy użytkownik istnieje
                    string checkUserQuery = "SELECT COUNT(*) FROM Użytkownicy WHERE Id = @UserId";
                    using (SqlCommand cmd = new SqlCommand(checkUserQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", _userId);
                        int userCount = (int)cmd.ExecuteScalar();
                        if (userCount == 0)
                        {
                            MessageBox.Show("Nie znaleziono użytkownika z podanym ID.");
                            return;
                        }
                    }

                    // Sprawdzenie dostępności sprzętu
                    string checkToolQuery = "SELECT StanMagazynowy FROM Sprzęt WHERE Id = @ToolId";
                    using (SqlCommand cmd = new SqlCommand(checkToolQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ToolId", _toolId);
                        object result = cmd.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show("Nie znaleziono sprzętu z podanym ID.");
                            return;
                        }

                        int stock = Convert.ToInt32(result);
                        if (stock <= 0)
                        {
                            MessageBox.Show("Niestety, sprzęt jest niedostępny.");
                            return;
                        }
                    }

                    // Dodanie wypożyczenia
                    string rentQuery = "INSERT INTO Wypożyczenia (ID_Klienta, ID_Sprzet, DataWypozyczenia, DataZwrotu) VALUES (@UserId, @ToolId, @RentDate, @ReturnDate)";
                    using (SqlCommand cmd = new SqlCommand(rentQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", _userId);
                        cmd.Parameters.AddWithValue("@ToolId", _toolId);
                        cmd.Parameters.AddWithValue("@RentDate", rentDate);
                        cmd.Parameters.AddWithValue("@ReturnDate", returnDate);
                        cmd.ExecuteNonQuery();
                    }

                    // Aktualizacja stanu magazynowego
                    string updateStockQuery = "UPDATE Sprzęt SET StanMagazynowy = StanMagazynowy - 1 WHERE Id = @ToolId";
                    using (SqlCommand cmd = new SqlCommand(updateStockQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ToolId", _toolId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Narzędzie zostało wypożyczone pomyślnie!");
                    _toolsWindow.RefreshSprzetList();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wypożyczania: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
