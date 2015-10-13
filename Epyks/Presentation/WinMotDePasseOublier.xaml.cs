using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Epyks.Coordonnateur;
using Epyks.Presentation;

namespace Epyks
{
	/// <summary>
	/// Logique d'interaction pour WinMotDePasseOublier.xaml
	/// </summary>
	public partial class WinMotDePasseOublier : Window
	{
        private CoordonnateurLogin coordinator;
        private WinLogin winLogin;
		public WinMotDePasseOublier(WinLogin winLogin)
		{
			this.InitializeComponent();
            this.winLogin = winLogin;
			// Insérez le code requis pour la création d’objet sous ce point.
		}

        /// <summary>
        /// Appel le coordonnateurLogin pour
        /// vérifier si le email existe puis 
        /// récupère le password et l'envoi par email.
        /// Ferme la fenêtre après l'envoie.
        /// </summary>
        /// <param name="sender">Bouton send password</param>
        /// <param name="e"></param>
        private void BtnSendPassWord_Click(object sender, RoutedEventArgs e)
        {
            coordinator = CoordonnateurLogin.GetInstance();
           if (TxtEmail.Text.ToString() != "")
            {
                if (coordinator.VerifierEmail(TxtEmail.Text.ToString()))
                {
                    string password = coordinator.recoverPassword(TxtEmail.Text.ToString());
                    coordinator.envoyerPassword(password, TxtEmail.Text.ToString());
                    Hide();
                    winLogin.ResetFields();
                    winLogin.Show();
                }
                else
                {
                    //error provider 
                }
            }
            else
            {
                //message error
            }
        }

        /// <summary>
        /// Ferme la fenetre Mot de pass oublié
        /// </summary>
        /// <param name="sender">Fenêtre mot de passe oublié</param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            winLogin.ResetFields();
            winLogin.Show();
        }
	}
}