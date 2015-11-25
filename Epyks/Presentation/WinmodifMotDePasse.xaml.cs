using Epyks.Application;
using Epyks.Coordonnateur;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

namespace Epyks.Presentation
{
    /// <summary>
    /// Interaction logic for WinmodifMotDePasse.xaml
    /// </summary>
    public partial class WinmodifMotDePasse : Window
    {
        private CoordonateurMembreCourant coordinateur;
        private MembreDTO mdto;

        public WinmodifMotDePasse()
        {
            InitializeComponent();
            coordinateur = CoordonateurMembreCourant.GetInstance();

        }

        private void btnModifierMDP_Click(object sender, RoutedEventArgs e)
        {
            validerMotDePasse();

            this.Close();
        }

        private void validerMotDePasse()
        {
            mdto = coordinateur.getMembreCourant();
            string mdpCrypte = cryptPasswordEntered();
            if (mdpCrypte.Equals(mdto.password.ToString()))
            {
                if (this.passBNewMDP.Password.ToString().Equals(this.passBConfirmNewMDP.Password.ToString()))
                {

                    MembreDAO mdao = MembreDAO.GetInstance();
                    mdao.updatePassword(mdto.id, this.passBNewMDP.Password.ToString());
                    MessageBox.Show("Mot de passe modifie avec succes!");
                }
                else { MessageBox.Show("Les nouveaux mots de passe ne concordent pas"); }

            }
            else { MessageBox.Show("Le mot de passe courant entre n'est pas correct"); }


        }

        private string cryptPasswordEntered()
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            string passwordCrypte = null;
            byte[] clearBytes = Encoding.Unicode.GetBytes(this.passBCurrentPassword.Password.ToString());
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    passwordCrypte = Convert.ToBase64String(ms.ToArray());
                }
            }
            return passwordCrypte;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}
