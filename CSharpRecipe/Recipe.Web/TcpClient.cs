using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Web
{
    public class CustomTcpClient : IDisposable
    {
        private TcpClient _client;
        private IPEndPoint _endPoint;
        private bool _disposed;

        #region Properties

        public IPAddress Address { get; }
        public int Port { get; }
        public string SSLServerName { get; }

        #endregion

        public CustomTcpClient(IPAddress address, int port, string sslServerName = null)
        {
            Address = address;
            Port = port;
            _endPoint = new IPEndPoint(Address, Port);
            SSLServerName = sslServerName;
        }

    }
}
