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
        private MainWindow _mainWindow;
        public LoginWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
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

            Użytkownik uzytkownik;

            using (var context = new Rent_a_toolContext())
            {
                uzytkownik = context.Użytkownicy.FirstOrDefault(u => u.Email == email);

                if (uzytkownik == null || !VerifyHaslo(haslo, uzytkownik.HasloHash))
                {
                    MessageBox.Show("Nieprawidłowy email lub hasło!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            int userId = uzytkownik.Id;
            string userName = uzytkownik.Imie;

            MessageBox.Show("Logowanie udane!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            _mainWindow.Close();
            ToolsWindow toolsWindow = new ToolsWindow(userName, userId);
            toolsWindow.Show();

            this.Close();

        }


        private bool VerifyHaslo(string inputHaslo, string storedHasloHash)
        {
            return inputHaslo == storedHasloHash;
        }
    }
}