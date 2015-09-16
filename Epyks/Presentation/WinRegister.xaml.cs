using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Epyks.Application;
using Epyks.Coordonnateur;
using Microsoft.Win32;

namespace Epyks.Presentation
{
	/// <summary>
	/// Logique d'interaction pour WinRegister.xaml
	/// </summary>
	public partial class WinRegister : Window
	{
        private CoordonnateurLogin coordinator;
	    private WinLogin login;
	    private TextBox txtUsername;
	    private PasswordBox txtPassword;
	    private PasswordBox txtConfirmPassword;
	    private TextBox txtFirstName;
	    private TextBox txtLastName;
	    private TextBox txtEmail;


		public WinRegister(WinLogin login)
		{
			this.InitializeComponent();
		    this.login = login;
		}

	    private void PasswordGotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox) sender;
            passwordBox.Background.Opacity = 0;
        }

        private void PasswordLostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            if (passwordBox.Password.Length == 0)
            {
                passwordBox.Background.Opacity = 1;
            }
        }

        private void BtnLoadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select a picture";
            fileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (fileDialog.ShowDialog() == true)
            {
                ImgProfil.Source = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            login.Show();
        }

        private void BtnBackToSignIn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnRegister_Click_1(object sender, RoutedEventArgs e)
        {
            coordinator = new CoordonnateurLogin();
            String message_erreur = coordinator.verifierChamps(TxtFirstName.Text, TxtLastName.Text, TxtEmail.Text,
                TxtUsername.Text, TxtPassword.ToString(), TxtConfirmPassword.ToString());

            if (message_erreur.Equals(null))
            {
                coordinator.verifierNomUtilisateurBD(TxtUsername.Text);
            }

           // coordinator.Register(TxtFirstName.Text,TxtLastName.Text, TxtEmail.Text,
           //     TxtUsername.Text, TxtPassword.ToString(), TxtConfirmPassword.ToString());

           
        }

	    private void creerMembre()
	    {
	    }
	}
}