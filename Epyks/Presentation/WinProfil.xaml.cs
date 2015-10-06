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
using Epyks.Application;
using Epyks.Coordonnateur;

namespace Epyks.Presentation
{
	/// <summary>
	/// Logique d'interaction pour WinProfil.xaml
	/// </summary>
	public partial class WinProfil : Window
	{
	    private CoordonnateurLogin coordinateur;

	    private WinLogin winLogin;

        public WinProfil(WinLogin winLogin)
		{
			this.InitializeComponent();

            this.winLogin = winLogin;
		}

        private void MenuStatusItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            winLogin.Show();
        }

	    public void recevoirMembre(MembreDTO membre)
	    {
	        this.TxtNomUtilisateur.Text = membre.firstName + " " + membre.lastName;
	    }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Serveur serveur = new Serveur();
        }
	}
}