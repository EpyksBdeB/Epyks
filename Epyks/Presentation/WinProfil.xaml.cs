using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        private string DISCUSSION_START = "Vous êtes en discussion avec ";
        private string USERNAME_SYMBOL = "@";
        private CoordonateurMembreCourant coordinateur;

        private bool enModeRecherche = false;

        private MembreDTO mDtoCourant;

	    private IDisposable observable;

	    private WinLogin login;

	    private List<MembreDTO> listAmis;

	    private MembreDTO currentAmis;

	    public WinProfil(WinLogin winLogin)
        {
            login = winLogin;
			InitializeComponent();

            coordinateur = CoordonateurMembreCourant.GetInstance();

            CreerProfil();
            btnBack.IsEnabled = false;
		}

	    private void CreerProfil()
	    {
	        mDtoCourant = coordinateur.GetMembreCourant();
	        this.TxtNomUtilisateur.Text = mDtoCourant.firstName+ " " + mDtoCourant.lastName;
            this.txtUsername.Text = "@"+mDtoCourant.username;

            ImageSource source = coordinateur.retreiveImageProfile(mDtoCourant.id);
            if (source != null)
            {
                this.ImageProfil.Source = source;
            }
            RafraichirListDamis();
	    }

	    private void MenuStatusItem_Click(object sender, RoutedEventArgs e)
        {
            if (menuAbsent.IsChecked)
            {
                imgStatusConnection.Source = new BitmapImage(new Uri(@"Resources/EnLigne.png", UriKind.RelativeOrAbsolute));
                menuConnecte.IsChecked = false;
                menuDeconnecte.IsChecked = false;
            }
            else if (menuConnecte.IsChecked)
            {
                menuAbsent.IsChecked = false;
                menuDeconnecte.IsChecked = false;
            }
            else if (menuDeconnecte.IsChecked)
            {
                menuAbsent.IsChecked = false;
                menuConnecte.IsChecked = false;
            }
        }

        /// <summary>
        /// Ferme la fenêtre du profil et ouvre la fenêtre du login
        /// </summary>
        /// <param name="sender">est la fenêtre qui sera fermée</param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            login.Show();
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
            btnBack.IsEnabled = true;
            string caractereRecherche = textRechercher.Text;
            ArrayList listResultatRecherche = coordinateur.GetListResultatRecherche(caractereRecherche);
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
        private void RafraichirListDamis()
        {
            listViewContact.Items.Clear();
            textRechercher.Clear();
            listAmis = coordinateur.GetListAmis(mDtoCourant.id);
            foreach (MembreDTO amis in listAmis)
            {
                listViewContact.Items.Add(amis.username);
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
            resultat.Remove(mDtoCourant.username);
            foreach (string nomAmis in resultat)
            {
                listViewContact.Items.Add(nomAmis);
            }
        }

        public bool VerifierSiDejaAmis(string usernameAmis)
        {
            return coordinateur.VerifierSiAmis(mDtoCourant.id, coordinateur.GetIdAmis(usernameAmis));
        }

        /// <summary>
        /// Methode qui permet d'ajouter le contact à la liste d'amis de l'utilisateur
        /// </summary>
        /// <param name="usernameAmis">Username de l'amis à ajouter</param>
        private void AjouterCeContactAuAmis(string usernameAmis)
        {
            MessageBoxResult m = MessageBox.Show("Do you want to add " + usernameAmis 
                + " to your friends list?", "Add a friend", MessageBoxButton.YesNo);
            switch (m)
            {
                case MessageBoxResult.Yes:
                    coordinateur.AjouterAmis(mDtoCourant.id, coordinateur.GetIdAmis(usernameAmis));
                    break;
                case MessageBoxResult.No:
                    RafraichirListDamis();
                    break;
            }
        }

        // public ... event listener pour bouton back to friend list
        // enModeRecherche = false;
	    public void OnNext(Message value)
	    {
            LblBlockConversation.Dispatcher.BeginInvoke(new Action(delegate()
            {
                LblBlockConversation.Text = LblBlockConversation.Text + (value.AuthorUsername + ": " + value.Content + "\n");
            }));
	    }

	    public void OnError(Exception error)
	    {
	        MessageBox.Show(error.Message);
	    }

	    public void OnCompleted()
	    {
	        observable.Dispose();
	    }

        private void BtnSendMsg_Click(object sender, RoutedEventArgs e)
        {
            coordinateur.EnvoyerMessage(TxtMessage.Text, currentAmis.id);
            TxtMessage.Text = null;
        }

        private void modifProfil_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            WinModifProfil modifProfil = new WinModifProfil(this);
            modifProfil.Show();
        }

        private void listViewContact_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (listViewContact.SelectedItem != null)
            {
                if (enModeRecherche)
                {
                    string usernameAmis = (string)listViewContact.SelectedItems[0];
                    if (!usernameAmis.Equals("No Results Found"))
                    {
                        if (!VerifierSiDejaAmis(usernameAmis))
                        {
                            AjouterCeContactAuAmis(usernameAmis);
                        }
                        else
                        {
                            MessageBox.Show("Vous etes déja amis avec ce contact!");
                        }
                        RafraichirListDamis();
                        enModeRecherche = false;
                        btnBack.IsEnabled = false;
                    }
                }
                else
                {
                    ChangerAmis();
                }
            }
             
        }

	    private void ChangerAmis()
	    {
            LblBlockConversation.Text = null;
            TxtMessage.Clear();

            currentAmis = listAmis[listViewContact.SelectedIndex];
	        txtEnDiscussionAvec.Text = DISCUSSION_START + currentAmis.firstName + " " + currentAmis.lastName;
	        txtUsernameAmi.Text = USERNAME_SYMBOL + currentAmis.username;
            if (observable != null)
            {
                observable.Dispose();
            }
            observable = coordinateur.SubscribeToStack(this, currentAmis.id);

            PnlConversation.Visibility = Visibility.Visible;
            listViewContact.UnselectAll();
        }

        private void ListViewContact_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            ContextMenu cMenu = listViewContact.ContextMenu;
            MenuItem item = (MenuItem) cMenu.Items[0];
            if (listViewContact.SelectedItems.Count == 0 || enModeRecherche)
            {
                item.IsEnabled = false;
            }
            else
            {
                item.IsEnabled = true;
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (coordinateur.DeleteFriend(mDtoCourant.id ,coordinateur.GetIdAmis((string)listViewContact.SelectedItems[0])))
            {
                RafraichirListDamis();
                MessageBox.Show("Friend deleted!");
            }
            }

        private void ChangerMDP_Click_1(object sender, RoutedEventArgs e)
        {
            WinmodifMotDePasse modifMDP = new WinmodifMotDePasse();
            modifMDP.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RafraichirListDamis();
            btnBack.IsEnabled = false;
        }

        private void seDeconnecter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void fermerProgramme_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtMessage_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtMessage.Text = "";
        }

        private void envoyer_message_enter(object sender, KeyEventArgs e)
        {
            // TODO: Envoyer message when enter is pressed
        }
       }
    }