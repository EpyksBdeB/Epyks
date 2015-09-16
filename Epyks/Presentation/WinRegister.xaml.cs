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
	    private CoordonnateurLogin coord;
	    private WinLogin login;
	    private TextBox txtUsername;
	    private PasswordBox txtPassword;
	    private PasswordBox txtConfirmPassword;
	    private TextBox txtFirstName;
	    private TextBox txtLastName;
	    private TextBox txtEmail;
        private String emailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                   + "@"
                                   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";


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

	    private bool verifierChampsRemplis()
	    {
	        bool champsRemplis = true;
            if (TxtUsername.Text.Length == 0 || TxtEmail.Text.Length == 0
                || TxtFirstName.Text.Length == 0 || TxtLastName.Text.Length == 0
            || TxtPassword.Password.Length == 0 || TxtConfirmPassword.Password.Length == 0)
            {
                champsRemplis = false;
                MessageBox.Show("All fields are required");
            }
            else if (!(TxtPassword.Password.Equals(TxtConfirmPassword.Password)))
            {
                champsRemplis = false;
                MessageBox.Show("Passwords are not the same");
            }

            return champsRemplis;
	    }

	    private bool validerChamps()
	    {
	        bool champsValides = true;
	        if (!Regex.IsMatch(TxtEmail.Text, emailPattern))
	        {
	            champsValides = false;
	            MessageBox.Show("Email format is not valid");
	        }
	        return champsValides;
	    }

        private void BtnRegister_Click_1(object sender, RoutedEventArgs e)
        {
            bool champsRemplis = verifierChampsRemplis();
            bool champsValides = validerChamps();

            if (champsRemplis && champsValides)
            {
                creerMembre();
                this.Hide();
                WinProfil profil = new WinProfil();
                profil.Show();
            }

            //MessageBox.Show("Click!");
        }

	    private void creerMembre()
	    {
	    }
	}
}