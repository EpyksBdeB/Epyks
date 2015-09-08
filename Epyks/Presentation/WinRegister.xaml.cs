using System.Windows;
using System.Windows.Controls;

namespace Epyks.Presentation
{
	/// <summary>
	/// Logique d'interaction pour WinRegister.xaml
	/// </summary>
	public partial class WinRegister : Window
	{
		public WinRegister()
		{
			this.InitializeComponent();
			
			// Insérez le code requis pour la création d’objet sous ce point.
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
	}
}