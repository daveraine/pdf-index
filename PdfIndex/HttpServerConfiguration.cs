using System.Net;
using System.Net.Sockets;
using uhttpsharp;
using uhttpsharp.Handlers;
using uhttpsharp.Listeners;
using uhttpsharp.RequestProviders;

namespace PdfIndex
{
    internal static class HttpServerConfiguration
    {
        public static readonly int Port = 8001;

        public static HttpServer Create()
        {
            var httpServer = new HttpServer(new HttpRequestProvider());
            FileHandler.MimeTypes.Add(".pdf", "application/pdf");
            httpServer.Use(new TcpListenerAdapter(new TcpListener(IPAddress.Loopback, Port)));
            httpServer.Use(new FileHandler());
            return httpServer;
        }
    }
}
