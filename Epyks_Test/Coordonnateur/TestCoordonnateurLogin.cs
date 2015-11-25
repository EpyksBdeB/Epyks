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

            coord.Register("Olivier", "Castro", "casof@gmail.com", "castropeo", "monPassword", Genre.MALE, null, null, 0);
            coord = CoordonnateurLogin.GetInstance();
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
             /* ... */
        }
        
        [Test]
        public void testLogin()
        {
            Assert.IsTrue(coord.Login("castropeo", "monPassword"));          
        }

        [Test]
        public void testRegister()
        {
            Assert.IsTrue(coord.verifierNomUtilisateurBD("castropeo"));
        }

        [Test]
        public void testUsernameValide()
        {
            bool exist = coord.verifierNomUtilisateurBD("castropeo");
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
             Assert.AreEqual("monPassword", coord.recoverPassword("casof@gmail.com"));
         }
        
        [Test]
         public void testerEnvoieDunEmail()
         {
             Assert.IsTrue(coord.envoyerPassword("monPassword", "casof@gmail.com"));
         }
    }
}
