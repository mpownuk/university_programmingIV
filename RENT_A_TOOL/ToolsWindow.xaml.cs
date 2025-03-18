using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RENT_A_TOOL
{
    public partial class ToolsWindow : Window
    {
        private string _userName;
        private int _userId;
        private string _connectionString;

        public ToolsWindow(string userName, int userId, string connectionString)
        {
            InitializeComponent();
            _userName = userName;
            _userId = userId;
            _connectionString = connectionString;
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
            SprzetPanel.Children.Clear();
            List<Sprzet> sprzety = PobierzSprzetZBazy();

            foreach (var sprzet in sprzety)
            {
                SprzetPanel.Children.Add(CreateSprzetCard(sprzet));
            }
        }

        private List<Sprzet> PobierzSprzetZBazy()
        {
            List<Sprzet> sprzety = new List<Sprzet>();
            string query = "SELECT Id, Nazwa, Opis, StanMagazynowy, Zdjecie FROM Sprzęt";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                sprzety.Add(new Sprzet
                                {
                                    Id = reader.GetInt32(0),
                                    Nazwa = reader.GetString(1),
                                    Opis = reader.GetString(2),
                                    StanMagazynowy = reader.GetInt32(3),
                                    Zdjecie = reader.IsDBNull(4) ? null : (byte[])reader[4]
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd podczas pobierania sprzętu: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return sprzety;
        }

        private Button CreateSprzetCard(Sprzet sprzet)
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
                RentToolWindow rentWindow = new RentToolWindow(_userId, sprzet.Id, sprzet.Nazwa, _userName, this, _connectionString);
                rentWindow.ShowDialog();
            };

            return button;
        }

        private ImageSource LoadImage(byte[] imageData)
        {
            if (imageData == null) return null;

            try
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
            catch
            {
                return GetPlaceholderImage();
            }
        }

        private ImageSource GetPlaceholderImage()
        {
            return new BitmapImage(new Uri("pack://application:,,,/Images/placeholder.png"));
        }

        private void CheckAdminPrivileges()
        {
            if (_userName.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
            {
                MainGrid.Children.Add(UtworzAdminButton());
            }
        }

        private Button UtworzAdminButton()
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
            Grid.SetRow(addButton, 2);
            Grid.SetColumnSpan(addButton, 2);

            return addButton;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddToolWindow addToolWindow = new AddToolWindow(this, _connectionString);
            addToolWindow.ShowDialog();
        }

        public void RefreshSprzetList()
        {
            LoadSprzet();
        }
    }

    public class Sprzet
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public int StanMagazynowy { get; set; }
        public byte[] Zdjecie { get; set; }
    }
}
