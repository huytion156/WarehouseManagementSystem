﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinFormsProject
{
    public class Client
    {
         
        private Socket client;
        private Thread clientListener;
        private bool isEndClientListener;
        private Form1 form1;

        public Client(Form1 f)
        {
            form1 = f;
        }

        #region Client
        public void Send(byte[] data)
        {
            if (client != null)
            {
                client.Send(data);
            }
            else
            {
                MessageBox.Show("Server is not connected.", "Client Send", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void Connect(string ipAddr, string port)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ipAddr), Convert.ToInt32(port));
            client.Connect(ipe);

            clientListener = new Thread(OnDataReceived);
            isEndClientListener = false;
            clientListener.Start();
        }
        public void Disconnect()
        {
            isEndClientListener = true;
            client.Close();
            client = null;
            clientListener.Abort();
        }

        private void OnDataReceived()//For Client Mode
        {
            try
            {
                while (isEndClientListener == false)
                {
                    byte[] receiveData = new byte[client.ReceiveBufferSize];
                    int iRx = client.Receive(receiveData);
                    if (iRx != 0)
                    {
                            if (iRx < receiveData.Length)
                            {
                                byte[] tempData = new byte[iRx];
                                for (int i = 0; i < iRx; i++)
                                    tempData[i] = receiveData[i];
                                receiveData = tempData;
                            }
                            
                                char[] chars = new char[iRx];
                                int charLength = Encoding.GetEncoding("GB18030").GetDecoder().GetChars(receiveData, 0, iRx, chars, 0);
                                String szData = new String(chars);
                                form1.UpdateText(szData + "\r\n");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Socket has been closed.\r\n" + e.Message, "Client Disconnection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        #endregion
    }
}
