using Bookstore.Models.Users;
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

namespace Bookstore.View
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public LoginUser LoginUser { get; private set; }
        public Login()
        {
            InitializeComponent();
        }
        public Login(LoginUser loginUser)
        {
            InitializeComponent();
            LoginUser = loginUser;
            DataContext = LoginUser;
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        void PasswordChangedHandler(Object sender, RoutedEventArgs args)
        {
            LoginUser.Password = ((PasswordBox)sender).Password;
        }

    }
}
