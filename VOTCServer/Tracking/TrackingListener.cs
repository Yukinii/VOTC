﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using VOTCServer.Core;

namespace VOTCServer.Tracking
{
    public static class TrackingListener
    {
        private static readonly Thread T = new Thread(Start);
        public static void Init()
        {
            T.Start();
        }
        static async void Start()
        {
            var listener = new TcpListener(IPAddress.Any, 7913);
            listener.Start();

            var bytes = new byte[1024];

            while (true)
            {
                try
                {
                    var client = listener.AcceptTcpClient();
                    var stream = client.GetStream();
                    int I;
                    while ((I = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        using (var writer = new StreamWriter("Tracking.txt", true))
                        {
                            var data = Encoding.UTF8.GetString(bytes, 0, I).Trim();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Kernel.WriteLine(DateTime.Now.ToShortTimeString() + " | " + data,ConsoleColor.Blue);
                            Console.ResetColor();
                            await writer.WriteLineAsync(DateTime.Now.ToShortTimeString() + " | " + data);
                            if(Kernel.CommandsReceived.Count > 50)
                                Kernel.CommandsReceived.Clear();

                            Kernel.CommandsReceived.Add(DateTime.Now.ToShortTimeString() + " | " + data.Split('|')[0]);
                        }
                    }
                    client.Close();
                }
                catch
                {
                    // ignored
                }
            }
        } 
    }
}