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

namespace XAML
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            bool loginTrue = false;
            bool passTrue = false;

            string login = TextBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();

            if (login.Length < 5)
            {
                TextBoxLogin.ToolTip = "Это поле введено не корректно";
                TextBoxLogin.Background = Brushes.Red;
                loginTrue = false;
            }
            else
            {
                TextBoxLogin.ToolTip = "";
                TextBoxLogin.Background = Brushes.Transparent;
                loginTrue = true;
            }

            if (pass.Length < 5)
            {
                passBox.ToolTip = "Это поле введено не корректно";
                passBox.Background = Brushes.Red;
                passTrue = false;
            }

            else
            {
                passBox.ToolTip = "";
                passBox.Background = Brushes.Transparent;
                passTrue = true;
            }

            if (loginTrue && passTrue)
            {
                User authUser = null;
                using (AppContext db = new AppContext())
                {
                    authUser = db.Users.Where(b => b.Login == login && b.Pass == pass).FirstOrDefault();
                }

                if (authUser != null)
                {
                    if (authUser.AdminMode == "Admin")
                    {
                        MessageBox.Show("Вы успешно вошли");
                        AdminPageWindow adminPageWindow = new AdminPageWindow();
                        adminPageWindow.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Вы успешно вошли");
                        UserPageWindow userPageWindow = new UserPageWindow();
                        userPageWindow.Show();
                        Hide();
                    }                
                }
                else
                {
                    MessageBox.Show("Вы ввели что-то некорректно");
                }
            }
        }

        private void Button_Window_Reg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            Hide();
        }
    }
}
