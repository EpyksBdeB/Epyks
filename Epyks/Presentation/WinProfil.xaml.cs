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
    /// Fenêtre Profil:
    /// ---------------
    /// Contient les informations relatives à
    /// l'utilisateur connecté.
    /// Contient la liste des amis de l'utilisateur,
    /// et la fenêtre de conversation.
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

        /// <summary>
        /// Ferme la fenêtre du profil et ouvre la fenêtre du login
        /// </summary>
        /// <param name="sender">est la fenêtre qui sera fermée</param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

<<<<<<< HEAD
        /// <summary>
        /// Affiche les informations de l'utilisateur connecté
        /// </summary>
        /// <param name="membre">Instance de membreDTO du membre se connectant</param>
	    public void recevoirMembre(MembreDTO membre)
=======
/*	    public void recevoirMembre(MembreDTO membre)
>>>>>>> 22b901a0d0e98eb8d106a2b8dc0e17c62243d73d
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