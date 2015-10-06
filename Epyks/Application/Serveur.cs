using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System;
using System.Threading;
using Chat = System.Net;
using System.Collections;
using System.Windows;

namespace Epyks.Application
{
    class Serveur
    {
        System.Net.Sockets.TcpListener chatServer;
        public static Hashtable nickName;
        public static Hashtable nickNameByConnect;

        /// <summary>
        /// Se charge d'initialiser la connection
        /// </summary>
        public Serveur()
        {
            //create our nickname and nickname by connection variables
            nickName = new Hashtable(100);
            nickNameByConnect = new Hashtable(100);
            //create our TCPListener object
            chatServer = new System.Net.Sockets.TcpListener(4296);
            //check to see if the server is running
            //while (true) do the commands
            while (true)
            {
                //start the chat server
                chatServer.Start();
                //check if there are any pending connection requests
                if (chatServer.Pending())
                {
                    //if there are pending requests create a new connection
                    Chat.Sockets.TcpClient chatConnection = chatServer.AcceptTcpClient();
                    //display a message letting the user know they're connected
                    Console.WriteLine("You are now connected");
                    MessageBox.Show("You are now connected");
                    //create a new DoCommunicate Object
                    // DoCommunicate comm = new DoCommunicate(chatConnection);
                }
            }
        }

    }
    
}
