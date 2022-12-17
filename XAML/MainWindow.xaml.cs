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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.IO;

namespace XAML
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppContext db;

        bool loginTrue = false;
        bool passTrue = false;
        bool pass_2True = false;
        bool emailTrue = false;
        bool adminPassTrue = true;

        public MainWindow()
        {
            InitializeComponent();

            db = new AppContext();

            DoubleAnimation btnAnimation = new DoubleAnimation();
            btnAnimation.From = 0;
            btnAnimation.To = 450;
            btnAnimation.Duration = TimeSpan.FromSeconds(3);
            regButton.BeginAnimation(Button.WidthProperty, btnAnimation);
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();
            string pass_2 = passBox2.Password.Trim();
            string email = TextBoxEmail.Text.Trim().ToLower();
            bool adminModeBox = (bool)CheckAdminModeBox.IsChecked;
            string adminMode = "User";
            string adminPassEntered = passBoxAdminPass.Password.Trim();
            string adminPass;
            string path = @"C:\Users\User\source\repos\XAML\XAML\TextFile1.txt";

            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                adminPass = sr.ReadLine();
            }

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

            if (pass != pass_2)
            {
                passBox2.ToolTip = "Это поле введено не корректно";
                passBox2.Background = Brushes.Red;
                pass_2True = false;
            }

            else
            {
                passBox2.ToolTip = "";
                passBox2.Background = Brushes.Transparent;
                pass_2True = true;
            }

            if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                TextBoxEmail.ToolTip = "Это поле введено не корректно";
                TextBoxEmail.Background = Brushes.Red;
                emailTrue = false;
            }

            else
            {
                TextBoxEmail.ToolTip = "";
                TextBoxEmail.Background = Brushes.Transparent;
                emailTrue = true;
            }

            if (adminModeBox == true)
            {
                if (adminPassEntered == adminPass)
                {
                    adminMode = "Admin";
                    adminPassTrue = true;
                }
                else
                {
                    passBoxAdminPass.ToolTip = "Это поле введено не корректно";
                    passBoxAdminPass.Background = Brushes.Red;
                    adminPassTrue = false;
                }
            }
            else
            {
                adminPassTrue = true;
            }


            if (loginTrue && passTrue && pass_2True && emailTrue && adminPassTrue)
            {
                User user = new User(login, email, pass, adminMode);

                db.Users.Add(user);
                db.SaveChanges();

                MessageBox.Show("Вы успешно зарегистрировались");

                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();
                Hide();
            }
        }

        private void Button_Window_Auth_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Hide();
        }

        private void CheckAdminModeBox_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation passBoxAnimationOpen = new DoubleAnimation();
            passBoxAnimationOpen.From = 0;
            passBoxAnimationOpen.To = 37;
            passBoxAnimationOpen.Duration = TimeSpan.FromSeconds(1);

            DoubleAnimation passBoxAnimationClose = new DoubleAnimation();
            passBoxAnimationClose.From = 37;
            passBoxAnimationClose.To = 0;
            passBoxAnimationClose.Duration = TimeSpan.FromSeconds(1);

            if (CheckAdminModeBox.IsChecked == true)
            {
                passBoxAdminPass.BeginAnimation(PasswordBox.HeightProperty, passBoxAnimationOpen);
                passBoxAdminPass.Visibility = Visibility.Visible;
            }

            if (CheckAdminModeBox.IsChecked == false)
            {
                passBoxAdminPass.BeginAnimation(PasswordBox.HeightProperty, passBoxAnimationClose);
                passBoxAdminPass.Visibility = Visibility.Hidden;
            }
        }
    }
}
