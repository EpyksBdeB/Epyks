using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Epyks.Application
{
    internal class GestionnaireCommunication
    {
        private const int PORT = 8080;
        private const string HOSTNAME = "aegaur.ddns.net";

        private TcpClient tcpClient;
        private StreamReader reader;
        private StreamWriter writer;
        private Membre membreCourant;
        private bool isReading;

        public GestionnaireCommunication(Membre membreCourant)
        {
            this.membreCourant = membreCourant;
            isReading = true;
            tcpClient = new TcpClient(HOSTNAME, PORT);
            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());
            writer.AutoFlush = true;
            writer.WriteLine(Convert.ToString(membreCourant.id));
        }

        public void EcrireMessage(Message message)
        {
            writer.WriteLine(message.getXml());
        }

        public void StartReading()
        {
            Thread thread = new Thread(new ThreadStart(Reading));
            thread.Start();
        }

        private void Reading()
        {
            string line = "";
            Message message = null;

            while (tcpClient.Connected && isReading)
            {
                line = reader.ReadLine();
                if (!String.IsNullOrEmpty(line))
                {
                    message = new Message(line);
                    membreCourant.AddMessageInStack(message);
                }
            }
        }

        internal void EndThread()
        {
            isReading = false;
            tcpClient.Close();
        }
    }
}
