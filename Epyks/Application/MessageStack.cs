using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epyks.Application
{
    internal class MessageStack : IObservable<Message>
    {
        private List<Message> messages;
        private List<IObserver<Message>> observers;

        public MessageStack()
        {
            messages = new List<Message>();
            observers = new List<IObserver<Message>>();
        }

        public IDisposable Subscribe(IObserver<Message> observer)
        {
            observers.Add(observer);
            foreach (Message message in messages)
            {
                observer.OnNext(message);
            }
            return new Unsubscriber<Message>(observers, observer);
        }

        internal void Add(Message message)
        {
            messages.Add(message);
            foreach (IObserver<Message> observer in observers)
            {
                observer.OnNext(message);
            }
        }
    }
}
