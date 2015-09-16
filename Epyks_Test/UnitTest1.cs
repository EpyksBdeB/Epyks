using System;
using NUnit.Framework;
using Epyks.Coordonnateur;

namespace Epyks_Test
{
    [TestFixture]
    public class UnitTest1
    {
        
        [Test]
        public void TestLogin()
        {
            CoordonnateurLogin coord = new CoordonnateurLogin();
            Assert.IsTrue(coord.Login("Olivier Castro", "gr007,,"));          
        }
    }
}
