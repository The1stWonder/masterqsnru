﻿using System;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using MasterQ.Helpers;
using MasterQ.Droid.Resources;

[assembly: Xamarin.Forms.Dependency(typeof(SocketClient))]
namespace MasterQ.Droid.Resources
{
    public class SocketClient : IFSocket
    {
        public static string sendText;

        public SocketClient()
        {
            
        }

        class StateObject
        {
            internal byte[] sBuffer;
            internal Socket sSocket;
            internal StateObject(int size, Socket sock)
            {
                sBuffer = new byte[size];
                sSocket = sock;
            }
        }

        public void SendMessage(string argText, string ip, int port)
        {
            //App.CheckSocket = true;
            sendText = argText;

            IPAddress ipAddress = IPAddress.Parse(ip);


            IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, port);

            Socket clientSocket = new Socket(
              AddressFamily.InterNetwork,
              SocketType.Stream,
              ProtocolType.Tcp);

            IAsyncResult asyncConnect = clientSocket.BeginConnect(
              ipEndpoint,
              new AsyncCallback(connectCallback),
              clientSocket);

            Console.Write("Connection in progress.");
            if (writeDot(asyncConnect) == true)
            {
                // allow time for callbacks to
                // finish before the program ends
                Thread.Sleep(1000);
                //App.CheckConnectSocket = "Socket is connected";
            }
            else
            {
                App.CheckSocket = false;
                App.CheckConnectSocket = "Socket is not connected";
            }

        }

        public static void
        connectCallback(IAsyncResult asyncConnect)
        {
            try
            {
                App.CheckSocket = false;
                App.CheckConnectSocket = "Socket is not connected";
                Socket clientSocket =
                  (Socket)asyncConnect.AsyncState;
                clientSocket.EndConnect(asyncConnect);
                // arriving here means the operation completed
                // (asyncConnect.IsCompleted = true) but not
                // necessarily successfully
                if (clientSocket.Connected == false)
                {
                    App.CheckSocket = false;
                    App.CheckConnectSocket = "Socket is not connected";
                    Console.WriteLine(".client is not connected.");
                    return;
                }
                else
                    App.CheckSocket = true;
                    Console.WriteLine(".client is connected.");
                App.CheckConnectSocket = "Socket is connected";

                //byte[] sendBuffer = Encoding.ASCII.GetBytes(sendText);
                byte[] sendBuffer = Encoding.Unicode.GetBytes(sendText);
                IAsyncResult asyncSend = clientSocket.BeginSend(
                  sendBuffer,
                  0,
                  sendBuffer.Length,
                  SocketFlags.None,
                  new AsyncCallback(sendCallback),
                  clientSocket);

                Console.Write("Sending data.");
                writeDot(asyncSend);
            }
            catch
            {
                App.CheckSocket = false;
            }
        }

        public static void sendCallback(IAsyncResult asyncSend)
        {
            Socket clientSocket = (Socket)asyncSend.AsyncState;
            int bytesSent = clientSocket.EndSend(asyncSend);
            Console.WriteLine(
              ".{0} bytes sent.",
              bytesSent.ToString());

            StateObject stateObject =
              new StateObject(16, clientSocket);

            // this call passes the StateObject because it
            // needs to pass the buffer as well as the socket
            IAsyncResult asyncReceive =
              clientSocket.BeginReceive(
                stateObject.sBuffer,
                0,
                stateObject.sBuffer.Length,
                SocketFlags.None,
                new AsyncCallback(receiveCallback),
                stateObject);

            Console.Write("Receiving response.");
            writeDot(asyncReceive);
        }

        public static void
          receiveCallback(IAsyncResult asyncReceive)
        {
            StateObject stateObject =
             (StateObject)asyncReceive.AsyncState;

            int bytesReceived =
              stateObject.sSocket.EndReceive(asyncReceive);

            //App.ShowMassageSocket = Encoding.ASCII.GetString(stateObject.sBuffer);
            App.ShowMassageSocket = Encoding.Unicode.GetString(stateObject.sBuffer);
            //  ".{0} bytes received: {1}{2}{2}Shutting down.",
            //  bytesReceived.ToString(),
            //Encoding.ASCII.GetString(stateObject.sBuffer);
            //Environment.NewLine;

            //stateObject.sSocket.Shutdown(SocketShutdown.Both);
            stateObject.sSocket.Close();
        }

        // times out after 2 seconds but operation continues
        internal static bool writeDot(IAsyncResult ar)
        {
            int i = 0;
            while (ar.IsCompleted == false)
            {
                if (i++ > 20)
                {
                    Console.WriteLine("Timed out.");
                    return false;
                }
                Console.Write(".");
                Thread.Sleep(100);
            }
            return true;
        }

    }
}
