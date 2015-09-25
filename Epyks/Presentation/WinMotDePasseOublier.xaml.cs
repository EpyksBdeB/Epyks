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

namespace Epyks
{
	/// <summary>
	/// Logique d'interaction pour WinMotDePasseOublier.xaml
	/// </summary>
	public partial class WinMotDePasseOublier : Window
	{
        private CoordonnateurLogin coordinator;
		public WinMotDePasseOublier()
		{
			this.InitializeComponent();
			
			// Insérez le code requis pour la création d’objet sous ce point.
		}

        private void BtnSendPassWord_Click(object sender, RoutedEventArgs e)
        {
            coordinator = CoordonnateurLogin.GetInstance();
            if (coordinator.VerifierEmail(TxtEmail.Text.ToString()))
            {
                string password = coordinator.recoverPassword(TxtEmail.Text.ToString());
                coordinator.envoyerPassword(password, TxtEmail.Text.ToString());
               // coordinator.envoyerPassword();
            }
            else
            {
                //error provider a mettre
            }
        }
	}
}