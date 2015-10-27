using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Epyks.Application
{
    internal class GestionnaireCommunication
    {
        private const int PORT = 8181;
        private const string HOSTNAME = "localhost";

        private TcpClient tcpClient;
        private StreamReader reader;
        private StreamWriter writer;
        private Membre membreCourant;

        public GestionnaireCommunication(Membre membreCourant)
        {
            this.membreCourant = membreCourant;
            tcpClient = new TcpClient(HOSTNAME, PORT);
            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());

            writer.WriteLine(Convert.ToString(membreCourant.id));
        }

        public void EcrireMessage(Message message)
        {
            writer.WriteLine(message.getXml());
        }
    }
}
