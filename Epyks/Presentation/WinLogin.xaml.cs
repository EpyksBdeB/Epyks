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
            initialiserFenetre();
        }

        private void initialiserFenetre()
        {
            TxtUsername.Text = Properties.Settings.Default.Username.ToString();
            txtVousNetesPas.Text = "Vous n'etes pas " + Properties.Settings.Default.Username.ToString() + "?";

            if (!(Properties.Settings.Default.Username.ToString().Equals("")))
            {
                chBRememberMe.IsChecked = true;
                TxtPassword.Focus();
                
            }
            else if (Properties.Settings.Default.Username.ToString().Equals(""))
            {
                chBRememberMe.IsChecked = false;
                TxtUsername.Focus();
                txtVousNetesPas.Visibility = Visibility.Hidden;
            }
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
            seConnecter();
            
        }

        private void seConnecter()
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

        private void seConnecter_enter_click(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                seConnecter();
            }

        }

        /// <summary>
        /// Enregistre le nom d'utilisateur dans le champ approprie lorsque precedemment la case
        /// "Se souvenir de moi" a ete "checke"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chBRememberMe_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Username = TxtUsername.Text.ToString();
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Retire le username de son textbox lorsque l'utilisateur ne souhaite pas le garder en memoire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chBRememberMe_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Username = "";
            Properties.Settings.Default.Save();

        }

        private void txtVousNetesPas_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TxtUsername.Focus();
            TxtUsername.Text = "";
            TxtPassword.Password = "";
            txtVousNetesPas.Visibility = Visibility.Hidden;
            
        }
    }
}
