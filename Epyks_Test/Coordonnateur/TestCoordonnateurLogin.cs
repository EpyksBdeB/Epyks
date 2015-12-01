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

        [TestFixtureSetUp]
        public void Init()
        {
            MembreDAO.TestMode = true;
            coord = CoordonnateurLogin.GetInstance();
            coord.Register("Mel", "Balvin", "casof@gmail.com", "mel007", "mel007", Genre.FEMALE, null, null, 0);
            //coord.Register("Olivier", "Castro", "casof@gmail.com", "castropeo", "monPassword", Genre.MALE, null, null, 0);
        }

        [TestFixtureTearDown]
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
            Assert.IsTrue(coord.VerifierNomUtilisateurBD("castropeo"));
        }

        [Test]
        public void testUsernameValide()
        {
            bool exist = coord.VerifierNomUtilisateurBD("castropeo");
            Assert.IsTrue(exist);
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
             Assert.AreEqual("monPassword", coord.RecoverPassword("casof@gmail.com"));
         }
        
        [Test]
         public void testerEnvoieDunEmail()
         {
             Assert.IsTrue(coord.EnvoyerPassword("monPassword", "casof@gmail.com"));
         }
    }
}
