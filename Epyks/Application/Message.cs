using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Epyks.Application
{
    public class Message
    {
        public int AuthorId { get; private set; }
        public string AuthorUsername { get; private set; }
        public string Content { get; private set; }

        public Message(int authorId, string authorUsername, string content)
        {
            AuthorId = authorId;
            AuthorUsername = authorUsername;
            Content = content;
        }

        public Message(string xmlString)
        {
            XmlReader reader = XmlReader.Create(new StringReader(xmlString));
            reader.ReadToFollowing("authid");
            AuthorId = reader.ReadElementContentAsInt();
            reader.ReadToNextSibling("authuser");
            AuthorUsername = reader.ReadElementContentAsString();
            reader.ReadToNextSibling("msgtext");
            Content = reader.ReadElementContentAsString();
        }

        public string getXml()
        {
            return "<?xml version='1.0'?>" +
                   "<message>" +
                   "<destid>TOUS</destid>" +
                   "<authid>" + AuthorId + "</authid>" +
                   "<authuser>" + AuthorUsername + "</authuser>" +
                   "<msgtext>" + Content + "</msgtext>" +
                   "</message>";
        }
    }
}
