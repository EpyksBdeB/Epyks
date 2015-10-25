using System;
using Epyks.Application;
using NUnit.Framework;
using Epyks.Coordonnateur;
using Epyks.Presentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Core;
using Assert = NUnit.Framework.Assert;

namespace Epyks_Test
{
    [TestFixture]
    public class TestCoordonnateurLogin
    {
        CoordonnateurLogin coord;
        [TestFixtureSetUp]
        public void Init()
        {
            coord = CoordonnateurLogin.GetInstance();
            coord.Register("Olivier", "Castro", "casof@gmail.com", "castropeo", "monPassword", Genre.MALE, null, null, 0);
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
             /* ... */
        }
        
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
        public void testerConnection()
        {
            CoordonnateurLogin coord = CoordonnateurLogin.GetInstance();
            Assert.IsNotNull(coord);
        }

        [Test]
        public void testUsernameValide()
        {
            CoordonnateurLogin log = CoordonnateurLogin.GetInstance();
            bool exist = log.verifierNomUtilisateurBD("mel07");
            Assert.IsTrue(exist);
        }

        [Test]
        public void testEmailValide()
        {
            CoordonnateurLogin log = CoordonnateurLogin.GetInstance();
            bool exist = log.VerifierEmail("ene@gmail.com");
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
    
        [Test]
        public void testGetAllMember()
        {
            //mDao.getMember();
        }

        [Test]
        public void testUpdateInfoMembre()
        {
            //mDao.updateMember();
        }
    }
}
