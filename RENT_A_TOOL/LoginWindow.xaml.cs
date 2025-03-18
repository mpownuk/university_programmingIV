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
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;


namespace RENT_A_TOOL
{
    public partial class LoginWindow : Window
    {
        private MainWindow _mainWindow;
        public string _connectionString;
        public LoginWindow(MainWindow mainWindow, string connectionString)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _connectionString = connectionString;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string haslo = HasloPasswordBox.Password;
            string query = "SELECT Id, Imie FROM Użytkownicy WHERE Email = @Email AND HasloHash = @Haslo";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(haslo))
            {
                MessageBox.Show("Podaj email i hasło!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Haslo", haslo);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = reader.GetInt32(0);
                                string userName = reader.GetString(1);
                                MessageBox.Show("Logowanie udane!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);


                                _mainWindow.Close();
                                ToolsWindow toolsWindow = new ToolsWindow(userName, userId, _connectionString);
                                toolsWindow.Show();

                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Nieprawidłowy email lub hasło!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd połączenia: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}