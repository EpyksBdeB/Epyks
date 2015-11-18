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
    public partial class winModifProfil : Window
    {
        private CoordonateurMembreCourant membreCourant;
        private MembreDTO mdto;

        public winModifProfil()
        {
            InitializeComponent();
            membreCourant = CoordonateurMembreCourant.GetInstance();
            mdto = membreCourant.getMembreCourant();

            initialiserInfos();


        }

        private void initialiserInfos()
        {
            this.lblNom.Content = mdto.firstName + " " + mdto.lastName;
            this.lblNomUtilisateur.Content = mdto.username;
            this.txtBContenuNom.Text = mdto.lastName;
            this.txtBContenuPrenom.Text = mdto.firstName;
            this.lblContenuUsername.Content = mdto.username;
            this.txtBContenuEmail.Text = mdto.email;
        }

        private void btnChangeImage_Click(object sender, RoutedEventArgs e)
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

        private void image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Je voudrais modifier mes informations de mon profil!");
            txtBContenuNoTelephone.IsEnabled = true;
            txtBContenuEmail.IsEnabled = true;
            txtBContenuPrenom.IsEnabled = true;
            txtBContenuNom.IsEnabled = true;
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();


        }

        private void btnEnregistrerInfos_Click_1(object sender, RoutedEventArgs e)
        {
            mdto = membreCourant.getMembreCourant();

            MembreDAO mdao = MembreDAO.GetInstance();

            string nom = this.txtBContenuNom.Text.ToString();
            string prenom = this.txtBContenuPrenom.Text.ToString();
            string email = this.txtBContenuEmail.Text.ToString();
            string notel = this.txtBContenuNoTelephone.Text.ToString();


            mdao.modifierInfosAPartirProfil(nom, prenom, email, notel, mdto.id);
            MessageBox.Show("Vos informations ont ete modifiees avec succes!");


        }
    }
}
