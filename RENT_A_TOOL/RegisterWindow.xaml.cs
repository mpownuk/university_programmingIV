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
    public partial class RegisterWindow : Window
    {
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

            Użytkownik nowyUzytkownik = new Użytkownik
            {
                Imie = imie,
                Nazwisko = nazwisko,
                Email = email,
                HasloHash = haslo
            };

            using (var context = new Rent_a_toolContext())
            {
                context.Użytkownicy.Add(nowyUzytkownik);
                context.SaveChanges();
            }

            MessageBox.Show("Rejestracja zakończona sukcesem!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

    }
}
