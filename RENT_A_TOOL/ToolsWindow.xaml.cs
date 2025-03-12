using System;
using System.Collections.Generic;
using System.IO;
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
    /// <summary>
    /// Logika interakcji dla klasy ToolsWindow.xaml
    /// </summary>
    public partial class ToolsWindow : Window
    {
        private string _userName;
        private int _userId;

        public ToolsWindow(string userName, int userId)
        {
            InitializeComponent();
            _userName = userName;
            _userId = userId;
            LoadSprzet();
            DisplayUserName();
            CheckAdminPrivileges();

        }

        private void DisplayUserName()
        {
            TextBlock userNameBlock = new TextBlock
            {
                Text = $"Witaj, {_userName}!",
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Right
            };

            MainGrid.Children.Add(userNameBlock);
            Grid.SetRow(userNameBlock, 0);
            Grid.SetColumn(userNameBlock, 1);
        }

        private void LoadSprzet()
        {
            using (var context = new Rent_a_toolContext())
            {
                var sprzety = context.Sprzęt.ToList();

                foreach (var sprzet in sprzety)
                {
                    SprzetPanel.Children.Add(CreateSprzetCard(sprzet));
                }
            }
        }

        private Button CreateSprzetCard(Sprzęt sprzet)
        {
            StackPanel panel = new StackPanel { Margin = new Thickness(10), Width = 200 };

            Image img = new Image { Height = 100, Margin = new Thickness(5) };
            img.Source = sprzet.Zdjecie != null ? LoadImage(sprzet.Zdjecie) : GetPlaceholderImage();

            TextBlock nameBlock = new TextBlock
            {
                Text = sprzet.Nazwa,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center
            };

            TextBlock descBlock = new TextBlock
            {
                Text = sprzet.Opis,
                TextWrapping = TextWrapping.Wrap,
                MaxHeight = 50
            };

            TextBlock stockBlock = new TextBlock
            {
                Text = $"Stan: {sprzet.StanMagazynowy}",
                FontWeight = FontWeights.Bold
            };

            panel.Children.Add(img);
            panel.Children.Add(nameBlock);
            panel.Children.Add(descBlock);
            panel.Children.Add(stockBlock);

            Button button = new Button
            {
                Content = panel,
                Width = 200,
                Height = 220,
                Background = Brushes.LightGray,
                Margin = new Thickness(5)
            };
            button.Click += (s, e) =>
            {
                RentToolWindow rentWindow = new RentToolWindow(_userId, sprzet.Id, sprzet.Nazwa, this);
                rentWindow.ShowDialog();
            };

            return button;
        }

        private ImageSource LoadImage(byte[] imageData)
        {
            using (var ms = new MemoryStream(imageData))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private ImageSource GetPlaceholderImage()
        {
            BitmapImage placeholder = new BitmapImage(new Uri("pack://application:,,,/Images/placeholder.png"));
            return placeholder;
        }

        private void CheckAdminPrivileges()
        {
            if (_userName.ToUpper() == "ADMIN")
            {
                Button addButton = new Button
                {
                    Content = "Dodaj sprzęt",
                    FontSize = 14,
                    Width = 150,
                    Padding = new Thickness(10),
                    Margin = new Thickness(10),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Background = Brushes.Green,
                    Foreground = Brushes.White
                };

                addButton.Click += AddButton_Click;
                MainGrid.Children.Add(addButton);
                Grid.SetRow(addButton, 2);
                Grid.SetColumnSpan(addButton, 2);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddToolWindow addToolWindow = new AddToolWindow(this);
            addToolWindow.ShowDialog();
        }

        public void RefreshSprzetList()
        {
            SprzetPanel.Children.Clear();
            LoadSprzet();
        }
    }
}
