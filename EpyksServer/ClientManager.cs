﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EpyksServer
{
    class ClientManager
    {
        private Serveur serveur;
        private TcpClient client;
        private int id; 
        private StreamReader reader;

        public ClientManager(Serveur serveur, TcpClient client, int id )
        {
            this.serveur = serveur;
            this.client = client;
            this.id = id;
            reader = new StreamReader(client.GetStream());
            
            Thread thread = new Thread(new ThreadStart(ChatListener));
        }

        private void ChatListener()
        {
            try
            {
                string line = "";
                TcpClient destinationClient = null;
                StreamWriter writer =null;
                int destinationId =-1;

                while (serveur.ServerUp && client.Connected)
                {
                    line = reader.ReadLine();
                    destinationId = Convert.ToInt32(Regex.Replace(line, @"^<userid>(\d+?)</userid>.*$", "$1"));
                    destinationClient = serveur.ClientList[destinationId];
                    if (destinationClient != null)
                    {
                        writer = new StreamWriter(destinationClient.GetStream());
                        writer.WriteLine(line); 
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            serveur.ClientList.Remove(id);
        }
    }
}
