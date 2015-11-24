using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System;
using System.Threading;
using System.Collections;
using System.Data;
using System.Net.Sockets;
using System.Windows;

namespace EpyksServer
{
    class Serveur
    {
        private TcpListener chatServer;
        public bool ServerUp { get; set; }

        public Dictionary<int, TcpClient> ClientList { get; private set; }



        /// <summary>
        /// Se charge d'initialiser la connection
        /// </summary>
        public Serveur()
        {
            chatServer = new TcpListener(8181);
            ServerUp = true;
            ClientList = new Dictionary<int, TcpClient>();

            chatServer.Start();
            Console.WriteLine("Server Up");
            while (ServerUp)
            {
                TcpClient chatClient = null;
                chatClient = chatServer.AcceptTcpClient();
                Console.WriteLine("You are now connected");
                StreamReader reader = new StreamReader(chatClient.GetStream());
                int id = Convert.ToInt32(reader.ReadLine());
                Console.WriteLine("Id obtained: " + id);
                ClientList.Add(id, chatClient);
                ClientManager manager = new ClientManager(this, chatClient, id);
            }
        }

    }

}
