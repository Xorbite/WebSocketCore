﻿using System;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Server.Main
{
    internal class Server : WebSocketBehavior
    {

        // Little handler framework to keep things clean.
        protected override void OnOpen() => Handlers.OnConnect.Handle(Context);
        protected override void OnMessage(MessageEventArgs m) => Handlers.OnMessage.Handle(m, Context);
        protected override void OnClose(CloseEventArgs c) => Handlers.OnDisconnect.Handle(c, Context);

        internal static void Main()
        {
            Console.Title = "Server";

            // Add servicename if present.
            if (Settings.servicename.Length != 0)
                Settings.server.AddWebSocketService<Server>(Settings.servicename);

            // Start the server.
            Settings.server.Start();

            // Check if the server was able to start successfully.
            if (!Settings.server.IsListening)
            {
                Console.Write("Unable to start server.\n");
                Console.ReadKey();
                return;
            }
            else
                Console.Write($"Server started: {Settings.hostname}\n");

            Thread.Sleep(-1); // Keeps current thread (the console window) open.
        }
    }
}