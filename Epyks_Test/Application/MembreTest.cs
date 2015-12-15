using System;
using System.Collections.Generic;
using Epyks.Application;
using NUnit.Framework;
using Epyks.Coordonnateur;
using Epyks.Presentation;
using NUnit.Core;
using Assert = NUnit.Framework.Assert;

namespace Epyks.Application.Test
{
    [TestFixture]
    public class MembreTest
    {
        Membre membre;
        private MembreDTO ami;
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
            ami = new MembreDTO(2, "bob2", "bob2", "bob2", "bob2", "bob2@bob.com", Genre.MALE, null, null, 0);
            membre.UpdateMessageStacks(new List<MembreDTO>{ami});
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
            IDisposable disposableMessageStack = membre.SubscribeToStack(observer, ami.id);
            Assert.IsNotNull(disposableMessageStack);
        }

        [Test]
        public void AddMessageInStackTest()
        {
            IObserver<Message> observer = new ObserverMock();
            Message message = new Message(ami.id, 1, "bob","Hello world");
            membre.SubscribeToStack(observer, ami.id);
            membre.AddMessageInStack(message, ami.id);

            Assert.AreEqual(message.AuthorId, lastMessage.AuthorId);
            Assert.AreEqual(message.AuthorUsername, lastMessage.AuthorUsername);
            Assert.AreEqual(message.Content, lastMessage.Content);
        }
    }
}
