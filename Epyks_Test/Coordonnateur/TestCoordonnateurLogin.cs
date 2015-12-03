using System;
using Epyks.Application;
using NUnit.Framework;
using Epyks.Coordonnateur;
using Epyks.Presentation;
using NUnit.Core;
using Assert = NUnit.Framework.Assert;

namespace Epyks.Coordonnateur.Test
{
    [TestFixture]
    public class TestCoordonnateurLogin
    {
        CoordonnateurLogin coord;

        [SetUp]
        public void Init()
        {
            MembreDAO.TestMode = true;
            coord = CoordonnateurLogin.GetInstance();
            Console.WriteLine(MembreDAO.TestMode);
            //coord.Register("Mel", "Balvin", "casof@gmail.com", "mel007", "mel007", Genre.FEMALE, null, null, 0);
            //coord.Register("Olivier", "Castro", "casof@gmail.com", "castropeo", "monPassword", Genre.MALE, null, null, 0);
            //coord.Register("Mel", "Balvin", "casof@gmail.com", "mel0007", "mel007", Genre.FEMALE, null, null, 0);
            //coord.Register("Mel", "Balvin", "casof@gmail.com", "mel00007", "mel007", Genre.FEMALE, null, null, 0);
            coord.Register("Mel", "Balvin", "casof@gmail.com", "mel007", "mel007", Genre.FEMALE, null, null, 0);
        }

        [TearDown]
        public void Cleanup()
        {
            MembreDAO.TestMode = false;
            MembreDAO.GetInstance().TruncateAll();
            
        }
        
        [Test]
        public void testLogin()
        {
            Assert.IsTrue(coord.Login("mel007", "mel007"));          
        }

        [Test]
        public void testRegister()
        {
            Assert.IsTrue(coord.VerifierNomUtilisateurBD("mel007"));
        }

        //[Test]
        //public void TestInsertFonctionnel()
        //{
        //    CoordonnateurLogin log = CoordonnateurLogin.GetInstance();

        //    log.validerEntrees("Christelle", "s", "epyks@gmail.com", "Epyks", "s", "s");
        //    int nbRow = mDao.trouverUsername("Epyks");
        //    Assert.AreEqual(nbRow, 1);
        //}

    [Test]
        public void testMethodeVerifierEmail()
        {
            Assert.IsTrue(coord.VerifierEmail("casof@gmail.com"));
        }

         //[Test]
         //public void testDeleteUser()
         //{
         //    CoordonnateurLogin log = CoordonnateurLogin.GetInstance();
         //    mDao.deleteMember("Epyks");
         //    int nbRow = mDao.trouverUsername("Epyks");

         //    Assert.AreEqual(nbRow, 0);
         //}

         [Test]
         public void testGetUserPassword()
         {
            String passwordCrypte;
            passwordCrypte = MembreDAO.GetInstance().CryptPassword("mel007");
           
             Assert.AreEqual(passwordCrypte, coord.RecoverPassword("casof@gmail.com"));
         }
        
        [Test]
         public void testerEnvoieDunEmail()
         {
             Assert.IsTrue(coord.EnvoyerPassword("mel007", "casof@gmail.com"));
         }
    }
}
