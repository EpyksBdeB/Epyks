using System;
using System.IO;
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
    /// Fenêtre Register
    /// ----------------
    /// Permet à une nouvel utilisateur d'entrer ses
    /// informations pour se créer un compte Epyks
	/// </summary>
	public partial class WinRegister : Window
	{
        private const string ERR_PASSWORD_EMPTY = "Le mot de passe est obligatoire";
        private const string ERR_CONFIRM_PASSWORD_NOT_MATCH = "Les deux mots de passe sont différents";

        private CoordonnateurLogin coordinator;
	    private WinLogin login;
	    private bool errorShowned = false;
	    private String filename;
	    private byte[] imageData;
	    private int fileSize;
	    private ControlTemplate passwordDefaultTemplate;
	    private ControlTemplate confirmPasswordDefaultTemplate;

		public WinRegister(WinLogin login)
		{
			this.InitializeComponent();
		    this.login = login;
		    passwordDefaultTemplate = TxtPassword.Template;
		    confirmPasswordDefaultTemplate = TxtConfirmPassword.Template;
		}

        /// <summary>
        /// Change le background du passwordBox lorsqu'il
        /// obtient le focus
        /// </summary>
        /// <param name="sender">Le passwordBox</param>
        /// <param name="e"></param>
	    private void PasswordGotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox) sender;
            passwordBox.Background.Opacity = 0;
        }

        /// <summary>
        /// Met le background du password transparent lorsque
        /// perte de focus et fait la validation
        /// </summary>
        /// <param name="sender">Le passwordBox</param>
        /// <param name="e"></param>
        private void TxtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
           // ValidationError validationError = new ValidationError(new DataErrorValidationRule(), TxtPassword.GetBindingExpression(PasswordHelper.PasswordProperty));
            //validationError.ErrorContent = ERR_CONFIRM_PASSWORD_NOT_MATCH;
            if (TxtPassword.Password.Length == 0)
            {
                TxtPassword.Background.Opacity = 1;
              //  Validation.MarkInvalid(TxtConfirmPassword.GetBindingExpression(PasswordHelper.PasswordProperty), validationError);
            }
            else
            {
            //    Validation.ClearInvalid(TxtConfirmPassword.GetBindingExpression(PasswordHelper.PasswordProperty));
            }
        }

        /// <summary>
        /// Met le background du password transparent lorsque
        /// perte de focus et fait la validation
        /// </summary>
        /// <param name="sender">Le passwordBox</param>
        /// <param name="e"></param>
        private void TxtConfirmPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            //ValidationError validationError = new ValidationError(new DataErrorValidationRule(), TxtConfirmPassword.GetBindingExpression(PasswordHelper.PasswordProperty));
            //validationError.ErrorContent = ERR_CONFIRM_PASSWORD_NOT_MATCH;
            if (TxtConfirmPassword.Password.Length == 0)
            {
                TxtConfirmPassword.Background.Opacity = 1;
            }

            if (!TxtConfirmPassword.Password.Equals(TxtPassword.Password))
            {
             //   Validation.MarkInvalid(TxtConfirmPassword.GetBindingExpression(PasswordHelper.PasswordProperty),validationError);
            }
            else
            {
             //   Validation.ClearInvalid(TxtConfirmPassword.GetBindingExpression(PasswordHelper.PasswordProperty));
            }
        }

        /// <summary>
        /// Ouverture de la fenêtre de fichier windows
        /// lorsque l'on clique sur le boutton pour uploader
        /// une photo et enresgistre l'image sélectionnée
        /// </summary>
        /// <param name="sender">Bouton upload</param>
        /// <param name="e"></param>
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

            enregistrerImage();
        }

        /// <summary>
        /// Enregistre l'image sélectionnée
        /// dans la variable fileSize
        /// </summary>
        private void enregistrerImage()
        {
            filename = new Uri(ImgProfil.Source.ToString()).LocalPath;
            FileStream fs = null;
            BinaryReader br = null;
                try
                {
                    fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
                    fileSize = Convert.ToInt32(filename.Length);
                    imageData = br.ReadBytes((int)fs.Length);

                    br.Close();
                    fs.Close();
                }
                catch (DirectoryNotFoundException)
                {
                    //
                }                           
        }

        /// <summary>
        /// Réinitialise les champs de la fenêtre register
        /// et ouvre la fenêtre de Login
        /// </summary>
        /// <param name="sender">La fenêtre register</param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ResetFields();
            login.Show();
        }

        /// <summary>
        /// Ferme la fenêtre en cours (Register)
        /// </summary>
        /// <param name="sender">Bouton back to signIn</param>
        /// <param name="e"></param>
        private void BtnBackToSignIn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Crée une nouvelle instance du coordonnateurLogin
        /// et enregistre le nouveau membre
        /// </summary>
        /// <param name="sender">Bouton Register</param>
        /// <param name="e"></param>
        private void BtnRegister_Click_1(object sender, RoutedEventArgs e)
        {
            coordinator = CoordonnateurLogin.GetInstance();
            if (ValidateFields())
            {
                coordinator.Register(TxtFirstName.Text, TxtLastName.Text, TxtEmail.Text,
                    TxtUsername.Text, TxtPassword.Password.ToString(),
                    RadMale.IsChecked == true ? Genre.MALE : Genre.FEMALE, filename, imageData, fileSize);
                this.Close();
            }
            else if (!errorShowned)
            {
                ShowErrors();
                errorShowned = true;
            }

        }

        /// <summary>
        /// Réinitialise tous les champs 
        /// de la fenêtre Register
        /// </summary>
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

        /// <summary>
        /// Vérifie si tous les 
        /// champs sont valident
        /// </summary>
        /// <returns>True: valide, False: invalide</returns>
	    private bool ValidateFields()
	    {
	        return !Validation.GetHasError(TxtUsername) && !Validation.GetHasError(TxtPassword) &&
                   !Validation.GetHasError(TxtConfirmPassword) && !Validation.GetHasError(TxtFirstName) &&
	               !Validation.GetHasError(TxtLastName) && !Validation.GetHasError(TxtEmail);
	    }

        /// <summary>
        /// Affiche les errors si
        /// validateFields() == false
        /// </summary>
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