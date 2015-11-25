using System;
using Epyks.Application;
using NUnit.Framework;
using Epyks.Coordonnateur;
using Epyks.Presentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Core;
using Assert = NUnit.Framework.Assert;

namespace Epyks.Application.Test
{
    [TestFixture]
    public class MembreDAOTest
    {
        Membre membre;
        static Message lastMessage;

        [TestFixtureSetUp]
        public void Init()
        {
            
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            /* ... */
        }
    }
}
