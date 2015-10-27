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
using System.Collections;

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
	public partial class WinProfil : Window, IObserver<Message>
	{
	    private CoordonateurMembreCourant coordinateur;

        private bool enModeRecherche = false;

        private MembreDTO mDtoCourant;

	    private IDisposable observable;

        public WinProfil(WinLogin winLogin)
		{
			this.InitializeComponent();
            coordinateur = CoordonateurMembreCourant.GetInstance();
            this.creerProfil();
            observable = coordinateur.SubscribeToStack(this);
		}

	    private void creerProfil()
	    {
	        mDtoCourant = coordinateur.getMembreCourant();
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

        /// <summary>
        /// Affiche les informations de l'utilisateur connecté
        /// </summary>
        /// <param name="membre">Instance de membreDTO du membre se connectant</param>
        /// 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Serveur serveur = new Serveur();
        }

        private void Bouton_rechercher_click(object sender, RoutedEventArgs e)
        {
            enModeRecherche = true;
            string caractereRecherche = textRechercher.Text;
            ArrayList listResultatRecherche = coordinateur.getListResultatRecherche(caractereRecherche);
            if (listResultatRecherche.Count == 0)
            {
                AfficherAucunResultat();
            }
            else
            {
                AfficherResultatRecherche(listResultatRecherche);
            }
        }


        /// <summary>
        /// Methode qui rempli la liste d'amis de l'utilisateur
        /// </summary>
        private void RemplirListDamis()
        {
            ArrayList listAmis = coordinateur.getListAmis(mDtoCourant.id);
            foreach (string nomAmis in listAmis)
            {
                listViewContact.Items.Add(nomAmis);
            }
        }

        /// <summary>
        /// Methode qui affiche qu'aucun resultat n'a été trouvé
        /// </summary>
        private void AfficherAucunResultat()
        {
            listViewContact.Items.Clear();
            listViewContact.Items.Add("No Results Found");
        }

        /// <summary>
        /// Methode qui affiche les resultat de la recherche
        /// </summary>
        /// <param name="resultat">list des amis trouvé</param>
        private void AfficherResultatRecherche(ArrayList resultat)
        {
            listViewContact.Items.Clear();
            foreach (string nomAmis in resultat)
            {
                listViewContact.Items.Add(nomAmis);
            }
        }

        /// <summary>
        /// Listener pour le choix ajout d'un amis.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (enModeRecherche)
            {
                string usernameAmis = (string)listViewContact.SelectedItems[0];
                if (!usernameAmis.Equals("No Results Found"))
                {
                    AjouterCeContactAuAmis(usernameAmis);
                }
            }
            else
            {
                //Selection d'un amis pour conversation
            }
        }

        /// <summary>
        /// Methode qui permet d'ajouter le contact à la liste d'amis de l'utilisateur
        /// </summary>
        /// <param name="usernameAmis"></param>
        private void AjouterCeContactAuAmis(string usernameAmis)
        {
            enModeRecherche = false;
            coordinateur.AjouterAmis(mDtoCourant.id, coordinateur.getIdAmis(usernameAmis));
        }

        // public ... event listener pour bouton back to friend list
        // enModeRecherche = false;
	    public void OnNext(Message value)
	    {
	        throw new NotImplementedException();
	    }

	    public void OnError(Exception error)
	    {
	        MessageBox.Show(error.Message);
	    }

	    public void OnCompleted()
	    {
	        observable.Dispose();
	    }
	}
}