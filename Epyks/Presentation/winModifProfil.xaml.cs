using Epyks.Application;
using Epyks.Coordonnateur;
using Epyks.Presentation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Epyks
{
    /// <summary>
    /// Interaction logic for winModifProfil.xaml
    /// </summary>
    public partial class WinModifProfil : Window
    {
        private CoordonateurMembreCourant membreCourant;
        private MembreDTO mdto;
        private WinProfil profil;

        public WinModifProfil(WinProfil winProfil)
        {
            profil = winProfil;
            InitializeComponent();
            membreCourant = CoordonateurMembreCourant.GetInstance();
            mdto = membreCourant.GetMembreCourant();

            InitialiserInfos();


        }

        private void InitialiserInfos()
        {
            this.lblNom.Content = mdto.firstName + " " + mdto.lastName;
            this.lblNomUtilisateur.Content = mdto.username;
            this.txtBContenuNom.Text = mdto.lastName;
            this.txtBContenuPrenom.Text = mdto.firstName;
            this.lblContenuUsername.Content = mdto.username;
            this.txtBContenuEmail.Text = mdto.email;
        }

        private void BtnChangeImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select a picture";
            fileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (fileDialog.ShowDialog() == true)
            {
                imgProfil.Source = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Je voudrais modifier mes informations de mon profil!");
            txtBContenuNoTelephone.IsEnabled = true;
            txtBContenuEmail.IsEnabled = true;
            txtBContenuPrenom.IsEnabled = true;
            txtBContenuNom.IsEnabled = true;
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            profil.Show();


        }

        private void BtnEnregistrerInfos_Click_1(object sender, RoutedEventArgs e)
        {
            mdto = membreCourant.GetMembreCourant();

            MembreDAO mdao = MembreDAO.GetInstance();

            string nom = this.txtBContenuNom.Text.ToString();
            string prenom = this.txtBContenuPrenom.Text.ToString();
            string email = this.txtBContenuEmail.Text.ToString();
            string notel = this.txtBContenuNoTelephone.Text.ToString();
            if (rdBMasculin.IsChecked == true)
            {
                Genre genre = Genre.MALE;
            }
            else if(rdB_Feminin.IsChecked == true)
            {
                Genre genre = Genre.FEMALE;
            }


            mdao.ModifierInfosAPartirProfil(nom, prenom, email, notel, mdto.id);
            MessageBox.Show("Vos informations ont ete modifiees avec succes!");


        }

        private void modifierMessagePerso_Click(object sender, RoutedEventArgs e)
        {
           string messagePrive =  txtBoxMessagePrive.Text.ToString();
            bool correct = membreCourant.verifierInput(messagePrive);
            if (correct)
            {
                MembreDAO mdao = MembreDAO.GetInstance();
                mdao.modifierMessagePersonnel(mdto.id, messagePrive);
            }
            
            
        }

        private void supprimer_compte_click(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment supprimer votre compte?", "Supprimer mon compte", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                // TODO: Supprimer compte de la BD 
                MembreDAO mdao = MembreDAO.GetInstance();
                mdao.DeleteMember(mdto.id);
                MessageBox.Show("Votre compte a ete supprime avec succes! Vous serez ridirige vers la fenetre de connection");
                this.Close();
                WinLogin login = new WinLogin();
                login.Show();
            }
            else
            {
                //CLOSE MESSAGEBOX
            }
        }
    }
}
