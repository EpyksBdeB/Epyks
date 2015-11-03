using System;
using System.Windows;
using Epyks.Application;
using Epyks.Coordonnateur;
namespace Epyks.Presentation
{
    /// <summary>
    /// Logique d'interaction pour WinLogin.xaml
    /// Fenêtre Login:
    /// --------------
    /// Permet à un utilisateur de se connecter à 
    /// l'aide de son username et son password
    /// </summary>
    public partial class WinLogin : Window
    {
        private CoordonnateurLogin coordinator;
        public WinLogin()
        {
                InitializeComponent();
                coordinator = CoordonnateurLogin.GetInstance();
        }

        /// <summary>
        /// Change le background du passwordBox lorsqu'il
        /// obtient le focus
        /// </summary>
        /// <param name="sender">Le passwordBox</param>
        /// <param name="e"></param>
        private void TxtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtPassword.Background.Opacity = 0;
        }

        /// <summary>
        /// Met le background du password transparent lorsque
        /// perte de focus
        /// </summary>
        /// <param name="sender">Le passwordBox</param>
        /// <param name="e"></param>
        private void TxtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TxtPassword.Password.Length == 0)
            {
                TxtPassword.Background.Opacity = 1;
            }
        }
        
        /// <summary>
        /// Affiche la fenêtre Register
        /// </summary>
        /// <param name="sender">Bouton register</param>
        /// <param name="e"></param>
        private void BtnGoRegister_Click(object sender, RoutedEventArgs e)
        {
            WinRegister winRegister = new WinRegister(this);
            Hide();
            winRegister.Show();
        }

        /// <summary>
        /// Affiche la fenêtre Profil avec
        /// les informations de l'utilisateur connecté
        /// </summary>
        /// <param name="sender">Bouton Login</param>
        /// <param name="e"></param>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            WinProfil winProfil;

            if (coordinator.Login(TxtUsername.Text, TxtPassword.Password))
            {
                winProfil = new WinProfil(this);
                LblInvalidError.Visibility = Visibility.Hidden;
                
                Hide();
                ResetFields();
                winProfil.Show();
            }
            else
            {
                LblInvalidError.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Réinitialise les champs de la
        /// fenêtre login (username, password)
        /// </summary>
        public void ResetFields()
        {
            TxtUsername.Text = null;
            TxtPassword.Password = null;
        }

        /// <summary>
        /// Affiche la fenêtre mot de passe oublié
        /// </summary>
        /// <param name="sender">Bouton Forgot Password</param>
        /// <param name="e"></param>
        private void BtnForgotPassWord_Click(object sender, RoutedEventArgs e)
        {
           WinMotDePasseOublier winForgotPassword = new WinMotDePasseOublier(this);
            Hide();
            winForgotPassword.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            coordinator.EndThreads();
        }
    }
}
