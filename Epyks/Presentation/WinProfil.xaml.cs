using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
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
	    private CoordonateurMembreCourant coordinateur;

        public WinProfil(WinLogin winLogin)
		{
			this.InitializeComponent();
            coordinateur = CoordonateurMembreCourant.GetInstance();
            this.creerProfil();
		}

	    private void creerProfil()
	    {
	        MembreDTO mDtoCourant = coordinateur.getMembreCourant();
	        this.TxtNomUtilisateur.Text = mDtoCourant.username;
	    }

	    private void MenuStatusItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

/*	    public void recevoirMembre(MembreDTO membre)
	    {
	        this.TxtNomUtilisateur.Text = membre.firstName + " " + membre.lastName;
	        this.txtUsername.Text = membre.username;
	    }
 * */

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Serveur serveur = new Serveur();
        }
	}
}