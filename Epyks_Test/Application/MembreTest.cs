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
    public class MembreTest
    {
        Membre membre;
        static Message lastMessage;

        private class ObserverMock : IObserver<Message>
        {
            public void OnNext(Message value)
            {
                MembreTest.lastMessage = value;
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnCompleted()
            {
                throw new NotImplementedException();
            }
        }
        [TestFixtureSetUp]
        public void Init()
        {
            membre = new Membre(1,"bob","bob","bob","bob","bob@bob.com",Genre.MALE, null, null, 0);
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            /* ... */
        }

        [Test]
        public void SubscribeToStackNotNull()
        {
            IObserver<Message> observer = new ObserverMock();
            IDisposable disposableMessageStack = membre.SubscribeToStack(observer);
            Assert.IsNotNull(disposableMessageStack);
        }

        [Test]
        public void AddMessageInStackTest()
        {
            IObserver<Message> observer = new ObserverMock();
            Message message = new Message(1,"bob","Hello world");
            membre.SubscribeToStack(observer);
            membre.AddMessageInStack(message);

            Assert.AreEqual(message.AuthorId, lastMessage.AuthorId);
            Assert.AreEqual(message.AuthorUsername, lastMessage.AuthorUsername);
            Assert.AreEqual(message.Content, lastMessage.Content);
        }
    }
}
