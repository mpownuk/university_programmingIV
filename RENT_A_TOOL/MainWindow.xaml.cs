using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RENT_A_TOOL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string _connectionString = "Server=DESKTOP-2R2BO2O\\SQLEXPRESS;Database=rent-a-tool;Trusted_Connection=True;TrustServerCertificate=True;";
        public MainWindow()
        {
            InitializeComponent();
        }
           private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow(this, _connectionString);
            loginWindow.Show();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow(_connectionString);
            registerWindow.Show();
        }
    }
}