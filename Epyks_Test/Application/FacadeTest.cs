using System;
using Epyks.Application;
using NUnit.Framework;
using Epyks.Presentation;
using NUnit.Core;
using Assert = NUnit.Framework.Assert;

namespace Epyks.Application.Test
{
    [TestFixture]
    public class FacadeTest
    {

        Facade facade;

        [TestFixtureSetUp]
        public void Init()
        {
            facade = Facade.GetInstance();
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            /* ... */
        }

        
        [Test]
        public void TesterRegister()
        {
        }

        [Test]
        public void TesterLogin()
        {
        }

        [Test]
        public void TesterVerifierUsername()
        {
        }

        [Test]
        public void TesterVerifierEmail()
        {
        }

        [Test]
        public void TesterRecupererPassword()
        {
        }

        [Test]
        public void TesterGetMembreCourant()
        {
        }

        [Test]
        public void TesterGetListDamis()
        {
        }

        [Test]
        public void TesterAjoutDamis()
        {
        }

        [Test]
        public void TesterGetAmisId()
        {
        }

        [Test]
        public void TesterVerifierSiAmis()
        {
        }

        [Test]
        public void TesterDeleteAmis()
        {
        }

        [Test]
        public void TesterEnvoyerPasswordByEmail()
        {
        }
    }
}
