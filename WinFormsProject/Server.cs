using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace WinFormsProject
{
    public class Server
    {
        private Socket server;
        private Socket tempSocket;
        private AsyncCallback asyncCallBack;

        private Form1 form1;

        public Server(Form1 f)
        {
            form1 = f;
        }

        #region Server
        public void Send(byte[] data)
        {
            if (tempSocket != null)
            {
                tempSocket.Send(data);
            }
            else
            {
                MessageBox.Show("There is no client connected.\r\nOr server is not connected.", "Server Send", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void Connect(string ipAddr, string port)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, Convert.ToInt32(port));

            server.Bind(ipLocal);//bind to the local IP Address...
            server.Listen(5);//start listening...

            // create the call back for any client connections...
            server.BeginAccept(new AsyncCallback(OnClientConnect), null);
        }
        public void Disconnect()
        {
            server.Close();
            server = null;
            tempSocket = null;
        }

        public void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                if (server != null)
                {
                    tempSocket = server.EndAccept(asyn);

                    WaitForData(tempSocket);

                    server.BeginAccept(new AsyncCallback(OnClientConnect), null);
                }
            }
            catch (ObjectDisposedException)
            {
                Debugger.Log(0, "1", "OnClientConnect: Socket has been closed.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "OnClientConnect Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void WaitForData(Socket soc)
        {
            try
            {
                if (asyncCallBack == null)
                    asyncCallBack = new AsyncCallback(OnDataReceived);

                KeyValuePair aKeyValuePair = new KeyValuePair();
                aKeyValuePair.socket = soc;

                // now start to listen for incoming data...
                aKeyValuePair.dataBuffer = new byte[soc.ReceiveBufferSize];

                soc.BeginReceive(aKeyValuePair.dataBuffer, 0, aKeyValuePair.dataBuffer.Length, SocketFlags.None, asyncCallBack, aKeyValuePair);
            }
            catch (SocketException e)
            {
                MessageBox.Show(e.Message, "WaitForData Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OnDataReceived(IAsyncResult asyn)//For Server Mode
        {
            try
            {
                if (tempSocket != null)
                {
                    KeyValuePair aKeyValuePair = (KeyValuePair)asyn.AsyncState;
                    //end receive...
                    int iRx = 0;
                    iRx = aKeyValuePair.socket.EndReceive(asyn);  
                    if (iRx != 0)
                    {
                        char[] chars = new char[iRx];
                        int charLength = Encoding.GetEncoding("GB18030").GetDecoder().GetChars(aKeyValuePair.dataBuffer, 0, iRx, chars, 0);
                        String text = new String(chars);
                        form1.UpdateText(text + "\r\n");
                        WaitForData(tempSocket);
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
            }
            catch (SocketException e)
            {
                if (e.ErrorCode == 10054)//Connection reset
                {
                    string text = "Client Disconnected \r\n";
                    form1.UpdateText(text + "\r\n");
                    tempSocket = null;
                }
            }
        }
        #endregion
    }
}
