using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
	    private bool errorShowned = false; 


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
           // BindingExpression binding = passwordBox.GetBindingExpression(PasswordHelper.PasswordProperty);
            if (passwordBox.Password.Length == 0)
            {
                passwordBox.Background.Opacity = 1;
            }
            //binding.UpdateSource();
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
            ResetFields();
            login.Show();
        }

        private void BtnBackToSignIn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnRegister_Click_1(object sender, RoutedEventArgs e)
        {
            coordinator = CoordonnateurLogin.GetInstance();
            if (ValidateFields())
            {
                coordinator.Register(TxtFirstName.Text, TxtLastName.Text, TxtEmail.Text,
                    TxtUsername.Text, TxtPassword.Password.ToString(),
                    RadMale.IsChecked == true ? Genre.MALE : Genre.FEMALE);
                this.Close();
            }
            else if (!errorShowned)
            {
                ShowErrors();
                errorShowned = true;
            }

        }

        private void ResetFields()
        {
            TxtUsername.Text = null;
            TxtPassword.Password = null;
            TxtConfirmPassword.Password = null;
            TxtFirstName.Text = null;
            TxtLastName.Text = null;
            TxtEmail.Text = null;
            RadMale.IsChecked = true;
            RadFemale.IsChecked = false;
            ImgProfil.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/profil_default.png"));
        }

	    private bool ValidateFields()
	    {
	        return !Validation.GetHasError(TxtUsername) && !Validation.GetHasError(TxtPassword) &&
                   !Validation.GetHasError(TxtConfirmPassword) && !Validation.GetHasError(TxtFirstName) &&
	               !Validation.GetHasError(TxtLastName) && !Validation.GetHasError(TxtEmail);
	    }

	    private void ShowErrors()
	    {
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri("/Epyks;component/CustomStyles.xaml",
                    UriKind.RelativeOrAbsolute);
            ControlTemplate errorTemplate = dictionary["TextBoxErrorTemplate"] as ControlTemplate;

            Validation.SetErrorTemplate(TxtUsername, errorTemplate);
            Validation.SetErrorTemplate(TxtPassword, errorTemplate);
            Validation.SetErrorTemplate(TxtConfirmPassword, errorTemplate);
            Validation.SetErrorTemplate(TxtFirstName, errorTemplate);
            Validation.SetErrorTemplate(TxtLastName, errorTemplate);
            Validation.SetErrorTemplate(TxtEmail, errorTemplate);
	    }
	}
}