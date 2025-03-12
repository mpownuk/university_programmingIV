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
using RENT_A_TOOL.models;


namespace RENT_A_TOOL
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string haslo = HasloPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(haslo))
            {
                MessageBox.Show("Podaj email i hasło!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new Rent_a_toolContext())
            {
                var uzytkownik = context.Użytkownicy.FirstOrDefault(u => u.Email == email);

                if (uzytkownik == null || !VerifyHaslo(haslo, uzytkownik.HasloHash))
                {
                    MessageBox.Show("Nieprawidłowy email lub hasło!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            MessageBox.Show("Logowanie udane!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            ToolsWindow toolsWindow = new ToolsWindow();
            toolsWindow.Show();

            this.Close();
        }


        private bool VerifyHaslo(string inputHaslo, string storedHasloHash)
        {
            return inputHaslo == storedHasloHash;
        }
    }
}