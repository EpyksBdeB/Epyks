using System;
using Epyks.Application;
using NUnit.Framework;
using Epyks.Coordonnateur;
using Epyks.Presentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace Epyks_Test
{
    [TestFixture]
    public class UnitTest1
    {
        private MembreDAO mDao;
        [TestFixtureSetUp]
        public void Init()
        {
             mDao = new MembreDAO();
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
             /* ... */
        }
        
        public void TestLogin()
        {
            CoordonnateurLogin coord = new CoordonnateurLogin();
            Assert.IsTrue(coord.Login("Olivier Castro", "gr007,,"));          
        }

        [TestMethod]
        public void testerConnection()
        {
            MembreDAO connection = new MembreDAO();
        }

        [TestMethod]
        public void testFindById()
        {
            //Membre m = mDao.getMember(1);
        }

        [Test]
        public void testInsertUsernameAlreadyTaken()
        {
            CoordonnateurLogin log = new CoordonnateurLogin();
            log.validerEntrees("s", "s", "s", "s", "s", "s");
            int nbRow = log.verifierNomUtilisateurBD("s");
            Assert.AreEqual(nbRow, 1);

        }
        [Test]
        public void TestInsertFonctionnel()
        {
            CoordonnateurLogin log = new CoordonnateurLogin();
            log.validerEntrees("Christelle", "s", "epyks@gmail.com", "Epyks", "s", "s");
            int nbRow = mDao.trouverUsername("Epyks");
            Assert.AreEqual(nbRow, 1);
        }

        [Test]
        public void testDeleteUser()
        {
            CoordonnateurLogin log = new CoordonnateurLogin();
            mDao.deleteMember("Epyks");
            int nbRow = mDao.trouverUsername("Epyks");

            Assert.AreEqual(nbRow, 0);
        }

        [Test]
        public void testGetUserPassword()
        {
            CoordonnateurLogin log = new CoordonnateurLogin();
            //log.Login("m", "m");
            Assert.AreEqual("m", mDao.getMember("m").getPassword());

           

        }
    }
}
