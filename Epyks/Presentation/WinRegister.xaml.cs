using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Epyks.Presentation
{
	/// <summary>
	/// Logique d'interaction pour WinRegister.xaml
	/// </summary>
	public partial class WinRegister : Window
	{
	    private WinLogin login;

		public WinRegister(WinLogin login)
		{
			this.InitializeComponent();
		    this.login = login;
		}

        private void PasswordGotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox) sender;
            passwordBox.Background.Opacity = 0;
        }

        private void PasswordLostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            if (passwordBox.Password.Length == 0)
            {
                passwordBox.Background.Opacity = 1;
            }
        }

        private void BtnLoadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select a picture";
            fileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (fileDialog.ShowDialog() == true)
            {
                ImgProfil.Source = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            login.Show();
        }
	}
}