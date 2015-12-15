using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace Epyks.Application
{
    public class Message
    {
        public int DestId { get; private set; }
        public int AuthorId { get; private set; }
        public string AuthorUsername { get; private set; }
        public string Content { get; private set; }

        public Message(int destId, int authorId, string authorUsername, string content)
        {
            DestId = destId;
            AuthorId = authorId;
            AuthorUsername = authorUsername;
            Content = content;
        }

        public Message(string xmlString)
        {
            XmlReader reader = XmlReader.Create(new StringReader(xmlString));

            reader.ReadToFollowing("destid");
            DestId = reader.ReadElementContentAsInt();
            AuthorId = reader.ReadElementContentAsInt();
            AuthorUsername = reader.ReadElementContentAsString();
            Content = reader.ReadElementContentAsString();
        }

        public string getXml()
        {
            return "<?xml version='1.0'?>" +
                   "<message>" +
                   "<destid>" + DestId +"</destid>" +
                   "<authid>" + AuthorId + "</authid>" +
                   "<authuser>" + AuthorUsername + "</authuser>" +
                   "<msgtext>" + Content + "</msgtext>" +
                   "</message>";
        }
    }
}
