using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epyks.Application
{
    internal class MessageStack : IObservable<Message>
    {
        internal List<Message> Messages { get; private set; }
        private List<IObserver<Message>> observers;

        public MessageStack()
        {
            Messages = new List<Message>();
        }

        public IDisposable Subscribe(IObserver<Message> observer)
        {
            observers.Add(observer);
            foreach (Message message in Messages)
            {
                observer.OnNext(message);
            }
            return new Unsubscriber<Message>(observers, observer);
        }

        internal void Add(Message message)
        {
            Messages.Add(message);
            foreach (IObserver<Message> observer in observers)
            {
                observer.OnNext(message);
            }
        }
    }
}
