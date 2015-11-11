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
        public winModifProfil()
        {
            InitializeComponent();
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
        }
    }
}
