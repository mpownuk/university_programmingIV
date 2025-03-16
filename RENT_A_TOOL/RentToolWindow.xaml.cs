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
    public partial class RentToolWindow : Window
    {
        private int _userId;
        private int _toolId;
        private string _toolName;
        private ToolsWindow _toolsWindow;
        private string _userName;

        public RentToolWindow(int userId, int toolId, string toolName, string userName, ToolsWindow toolsWindow)
        {
            InitializeComponent();
            _userId = userId;
            _toolId = toolId;
            _toolName = toolName;
            _toolsWindow = toolsWindow;
            _userName = userName;
            CheckAdminPrivileges();
            ToolNameText.Text = $"Wypożyczenie: {_toolName}";
        }
        private void CheckAdminPrivileges()
        {
            if (_userName.ToUpper() == "ADMIN")
            {
                Button updateButton = new Button
                {
                    Content ="Edytuj sprzęt",
                    FontSize = 14,
                    Width = 100,
                    Padding = new Thickness(10),
                    Margin  =new Thickness(10),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Background  =Brushes.Blue,
                    Foreground = Brushes.White
                };

                Button deleteButton = new Button
                {
                    Content = "Usuń sprzęt",
                    FontSize = 14,
                    Width = 100,
                    Padding = new Thickness(10),
                    Margin = new Thickness(10),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Background = Brushes.Red,
                    Foreground = Brushes.White
                };
                RentGrid.Children.Add(updateButton);
                Grid.SetRow(updateButton, 5);
                updateButton.Click += UpdateTool_Click;

                RentGrid.Children.Add(deleteButton);
                Grid.SetRow(deleteButton, 5);
                deleteButton.Click += DeleteTool_Click;
            }
        }

        private void UpdateTool_Click(object sender, RoutedEventArgs e)
        {
            UpdateToolWindow updateToolWindow = new UpdateToolWindow(_toolId, _toolsWindow);
            updateToolWindow.Show();
            this.Close();

        }
        private void DeleteTool_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Czy na pewno chcesz usunąć narzędzie: {_toolName}?",
                                                      "Potwierdzenie usunięcia",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                using (var context = new Rent_a_toolContext())
                {
                    var tool = context.Sprzęt.FirstOrDefault(t => t.Id == _toolId);
                    if (tool == null)
                    {
                        MessageBox.Show("Nie znaleziono sprzętu do usunięcia.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    context.Sprzęt.Remove(tool);
                    context.SaveChanges();

                    MessageBox.Show("Sprzęt został pomyślnie usunięty!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                    _toolsWindow.RefreshSprzetList();
                    this.Close();
                }
            }
        }

        private void ConfirmRent_Click(object sender, RoutedEventArgs e)
        {
            if (RentDatePicker.SelectedDate.HasValue && ReturnDatePicker.SelectedDate.HasValue)
            {
                DateTime rentDate = RentDatePicker.SelectedDate.Value;
                DateTime returnDate = ReturnDatePicker.SelectedDate.Value;

                if (returnDate <= rentDate)
                {
                    MessageBox.Show("Data zwrotu musi być późniejsza niż data wypożyczenia.");
                    return;
                }

                Wypożyczenie wypożyczenie = new Wypożyczenie
                    {
                        ID_Klienta = _userId,
                        ID_Sprzet = _toolId,
                        DataWypozyczenia = rentDate,
                        DataZwrotu = returnDate
                    };
                using (var context = new Rent_a_toolContext())
                {
                    bool userExists = context.Użytkownicy.Any(u => u.Id == _userId);
                    if (!userExists)
                    {
                        MessageBox.Show("Nie znaleziono użytkownika z podanym ID.");
                        return;
                    }

                    var tool = context.Sprzęt.FirstOrDefault(t => t.Id == _toolId);
                    if (tool == null)
                    {
                        MessageBox.Show("Nie znaleziono sprzętu z podanym ID.");
                        return;
                    }

                    if (tool.StanMagazynowy <= 0)
                    {
                        MessageBox.Show("Niestety, sprzęt jest niedostępny.");
                        return;
                    }



                    context.Wypożyczenia.Add(wypożyczenie);
                    tool.StanMagazynowy--;
                    context.SaveChanges();

                    MessageBox.Show("Narzędzie zostało wypożyczone pomyślnie!");
                    _toolsWindow.RefreshSprzetList();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Proszę wybrać daty wypożyczenia i zwrotu.");
            }
        }
    }
}
