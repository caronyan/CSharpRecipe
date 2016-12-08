using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Web
{
    public class TcpServer
    {
        #region Private Members

        private TcpListener _listener;
        private IPAddress _address;
        private int _port;
        private bool _listening;
        private string _sslServerName;
        private object _syncRoot = new object();

        #endregion

        public TcpServer(IPAddress address, int port, string sslServerName = null)
        {
            _port = port;
            _address = address;
            _sslServerName = sslServerName;
        }

        #region Properties

        public IPAddress Address { get; }
        public int Port { get; }
        public bool Listening { get; private set; }
        public string SSLServerName { get; }

        #endregion


    }
}
