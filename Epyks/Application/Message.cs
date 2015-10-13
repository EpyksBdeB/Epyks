using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epyks.Application
{
    public class Message
    {
        public string AuthorUsername { get; private set; }
        public string Content { get; private set; }

        public Message(string authorUsername, string content)
        {
            AuthorUsername = authorUsername;
            Content = content;
        }
    }
}
