using System;
using Epyks.Application;
using NUnit.Framework;
using Epyks.Coordonnateur;
using Epyks.Presentation;
using NUnit.Core;
using Assert = NUnit.Framework.Assert;
using System.Collections;

namespace Epyks.Coordonnateur.Test
{
    /// <summary>
    /// Description résumée pour UnitTest1
    /// </summary>
    [TestFixture]
    public class TestCoordonnateurMembreCourant
    {
        [TestFixtureSetUp]
        public void Init()
        {
            coordMembre = CoordonateurMembreCourant.GetInstance();
            coord = CoordonnateurLogin.GetInstance();
            if (!coord.VerifierNomUtilisateurBD("FakeUser"))
            {
                coord.Register("FakeFirst", "FakeLast", "fake@gmail.com", "FakeUser", "FakePass", Genre.MALE, null, null, 0);

            }
            if (!coord.VerifierNomUtilisateurBD("Amis1") && !coord.VerifierNomUtilisateurBD("Amis2"))
            {
                coord.Register("FakeFirst", "FakeLast", "Amis1@gmail.com", "Amis1", "FakePass", Genre.MALE, null, null, 0);
                coord.Register("FakeFirst", "FakeLast", "Amis2@gmail.com", "Amis2", "FakePass", Genre.MALE, null, null, 0);
            }
            coord.Login("FakeUser", "FakePass");
            mdtoCourant = coordMembre.GetMembreCourant();
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            /* ... */
        }

        private Facade api;
        private MembreDTO mdtoCourant;
        CoordonnateurLogin coord;
        CoordonateurMembreCourant coordMembre;

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [Test]
        public void testerMembreCourantNotNull()
        {
            Assert.IsNotNull(coordMembre.GetMembreCourant());
        }

        [Test]
        public void testerAjoutDamis()
        {
            int idAmis1 = coordMembre.GetIdAmis("Amis1");
            int idAmis2 = coordMembre.GetIdAmis("Amis2");
            if(!coordMembre.VerifierSiAmis(mdtoCourant.id, idAmis1)
                && !coordMembre.VerifierSiAmis(mdtoCourant.id, idAmis2))
            {
                coordMembre.AjouterAmis(mdtoCourant.id, idAmis1);
                coordMembre.AjouterAmis(mdtoCourant.id, idAmis2);
            }
            Assert.IsTrue(coordMembre.VerifierSiAmis(mdtoCourant.id, idAmis1));
            Assert.IsTrue(coordMembre.VerifierSiAmis(mdtoCourant.id, idAmis2));
        }

        [Test]
        public void testerRecupererListAmis()
        {
            int idAmis1 = coordMembre.GetIdAmis("Amis1");
            int idAmis2 = coordMembre.GetIdAmis("Amis2");
            coordMembre.AjouterAmis(mdtoCourant.id, idAmis1);
            coordMembre.AjouterAmis(mdtoCourant.id, idAmis2);
            ArrayList listAmis = coordMembre.GetListAmis(mdtoCourant.id);
            Assert.IsTrue(listAmis.Count == 2 && listAmis[0].Equals("Amis1") && listAmis[1].Equals("Amis2"));
        }

        [Test]
        public void testerSupprimerUnAmis()
        {
            int idAmis1 = coordMembre.GetIdAmis("Amis1");
            int idAmis2 = coordMembre.GetIdAmis("Amis2");
            coordMembre.DeleteFriend(mdtoCourant.id, idAmis1);
            coordMembre.DeleteFriend(mdtoCourant.id, idAmis2);
            Assert.IsFalse(coordMembre.VerifierSiAmis(mdtoCourant.id, idAmis1));
            Assert.IsFalse(coordMembre.VerifierSiAmis(mdtoCourant.id, idAmis2));
        }
    }
}
