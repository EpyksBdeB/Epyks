using System;
using System.Windows;

namespace Epyks.Presentation
{
    /// <summary>
    /// Logique d'interaction pour WinLogin.xaml
    /// </summary>
    public partial class WinLogin : Window
    {
        public WinLogin()
        {
                InitializeComponent();
        }

        private void TxtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtPassword.Background.Opacity = 0;
        }

        private void TxtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TxtPassword.Password.Length == 0)
            {
                TxtPassword.Background.Opacity = 1;
            }
        }

        private void BtnGoRegister_Click(object sender, RoutedEventArgs e)
        {
            WinRegister winRegister = new WinRegister(this);
            Hide();
            winRegister.Show();
        }
    }
}
