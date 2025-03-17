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
    public partial class RegisterWindow : Window
    {
        private string connectionString = "Server=DESKTOP-2R2BO2O\\SQLEXPRESS;Database=rent-a-tool;Trusted_Connection=True;TrustServerCertificate=True;";

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string imie = ImieTextBox.Text;
            string nazwisko = NazwiskoTextBox.Text;
            string email = EmailTextBox.Text;
            string haslo = HasloPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(imie) || string.IsNullOrWhiteSpace(nazwisko) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(haslo))
            {
                MessageBox.Show("Wszystkie pola są wymagane!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ZarejestrujUzytkownika(imie, nazwisko, email, haslo))
            {
                MessageBox.Show("Rejestracja zakończona sukcesem!","Sukces",MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Wsytąpił błąd podczas rejestracji użytkownika!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private bool ZarejestrujUzytkownika(string imie, string nazwisko, string email, string haslo)
        {
            string query = "INSERT INTO Użytkownicy (imie, nazwisko, email, haslohash) VALUES (@imie, @nazwisko, @email, @haslo)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@imie", imie);
                        cmd.Parameters.AddWithValue("@Nazwisko", nazwisko);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Haslo", haslo);
                        
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }

                } catch ( Exception ex)
                {

                    MessageBox.Show("Błąd:", ex.Message);
                    return false;
                }
            }
        }
    }
}
