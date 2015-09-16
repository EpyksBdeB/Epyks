using System;
using Epyks.Application;
using NUnit.Framework;
using Epyks.Coordonnateur;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace Epyks_Test
{
    [TestFixture]
    public class UnitTest1
    {
        private MembreDAO mDao;
        
        [Test]
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
            Membre m = mDao.getMember(1);
        }

        [TestMethod]
        public void testInsert()
        {
          //  mDao = new MembreDAO();
          //  Membre m = mDao.insertMember();
          //  m = new Membre();
            
        }
    }
}
