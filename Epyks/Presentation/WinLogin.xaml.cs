using System;
using System.Windows;
using Epyks.Application;
using Epyks.Coordonnateur;
namespace Epyks.Presentation
{
    /// <summary>
    /// Logique d'interaction pour WinLogin.xaml
    /// </summary>
    public partial class WinLogin : Window
    {
        private CoordonnateurLogin coordinator;
        public WinLogin()
        {
                InitializeComponent();
                coordinator = CoordonnateurLogin.GetInstance();
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

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            WinProfil winProfil = new WinProfil(this);
            if (coordinator.Login(TxtUsername.Text, TxtPassword.Password))
            {
                Hide();
                ResetFields();
                winProfil.Show();
            }
        }

        public void ResetFields()
        {
            TxtUsername.Text = null;
            TxtPassword.Password = null;
        }

        private void BtnForgotPassWord_Click(object sender, RoutedEventArgs e)
        {
           WinMotDePasseOublier winForgotPassword = new WinMotDePasseOublier(this);
            Hide();
            winForgotPassword.Show();
        }
    }
}
