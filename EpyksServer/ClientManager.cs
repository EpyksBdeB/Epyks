using System;
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
			thread.Start();
        }

        private void ChatListener()
        {
            Console.WriteLine("Listening started");
            try
            {
                string line = "";
                string tempDestId = "";
                TcpClient destinationClient = null;
                StreamWriter writer =null;
                int destId =-1;

                while (serveur.ServerUp && client.Connected)
                {
                    line = reader.ReadLine();
                    if (!String.IsNullOrEmpty(line))
                    {
                        tempDestId = Regex.Replace(line, @"^.*?<destid>(\d+?|TOUS)</destid>.*$", "$1");
                        if (tempDestId.Equals("TOUS"))
                        {
                            foreach (int userId in serveur.ClientList.Keys)
                            {
                                if (userId != id)
                                {
                                    Console.WriteLine("Send to " + userId);
                                    writer = new StreamWriter(serveur.ClientList[userId].GetStream());
                                    writer.AutoFlush = true;
                                    writer.WriteLine(line);
                                }
                            }
                        }
                        else
                        {
                            destId = Convert.ToInt32(tempDestId);
                            destinationClient = serveur.ClientList[destId];
                            if (destinationClient != null)
                            {
                                writer = new StreamWriter(destinationClient.GetStream());
                                writer.WriteLine(line);
                            }
                        }
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
