using System;
using Epyks.Application;
using NUnit.Framework;
using Epyks.Coordonnateur;
using Epyks.Presentation;
using NUnit.Core;
using Assert = NUnit.Framework.Assert;

namespace Epyks_Test.Application
{
    [TestFixture]
    public class MembreTest
    {

        [TestFixtureSetUp]
        public void Init()
        {
            MembreDAO.TestMode = true;
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            MembreDAO.TestMode = false;
            MembreDAO.GetInstance().TruncateAll();
        }

        [Test]
        public void testerGetXml()
        {
            int destId = 2;
            string resultXml = "<?xml version='1.0'?>" +
       "<message>" +
       "<destid>" + destId +"</destid>" +
       "<authid>10</authid>" +
       "<authuser>Olivier</authuser>" +
       "<msgtext>Bonjour</msgtext>" +
       "</message>";

            Message expected = new Message(destId,10, "Olivier", "Bonjour");

            Assert.AreEqual(expected.getXml(), resultXml);
        }

        [Test]
        public void testerCreationMessageAPartirDunStringXml()
        {
            int destId = 2;
            string xml = "<?xml version='1.0'?>" +
       "<message>" +
       "<destid>" + destId + "</destid>" +
       "<authid>10</authid>" +
       "<authuser>Olivier</authuser>" +
       "<msgtext>Bonjour</msgtext>" +
       "</message>";

            Message expected = new Message(destId, 10, "Olivier", "Bonjour");
            Message result = new Message(xml);

            Assert.AreEqual(10, result.AuthorId);
            Assert.AreEqual("Olivier", result.AuthorUsername);
            Assert.AreEqual("Bonjour", result.Content);
        }
    }
}
