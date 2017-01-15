using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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

        #region Public Methods

        public async Task ListenAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                lock (_syncRoot)
                {
                    _listener = new TcpListener(Address, Port);

                    _listener.Start();

                    Listening = true;
                }

                do
                {
                    Console.WriteLine("Looking for someone to talk to...");
                    try
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        await Task.Run(async () =>
                        {
                            TcpClient newClient = await _listener.AcceptTcpClientAsync();
                            Console.WriteLine("Connected to new client");
                            await ProcessClientAsync(newClient, cancellationToken);
                        }, cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {
                        Listening = false;
                    }
                } while (Listening);
            }
            catch (SocketException se)
            {
                Console.WriteLine($"Socket exception:{se}");
            }
            finally
            {
                StopListening();
            }
        }

        public void StopListening()
        {
            if (Listening)
            {
                lock (_syncRoot)
                {
                    Listening = false;
                    try
                    {
                        if (_listener.Server.IsBound)
                        {
                            _listener.Stop();
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                        Console.WriteLine("Cancelled the listener");
                    }
                }

            }
        }

        #endregion

        #region Private Methods

        private async Task ProcessClientAsync(TcpClient client,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                byte[] bytes = new byte[1024];
                StringBuilder clientData = new StringBuilder();

                Stream stream = null;
                if (!string.IsNullOrWhiteSpace(SSLServerName))
                {
                    Console.WriteLine($"Talking to client over SSL using {SSLServerName}");
                    SslStream sslStream = new SslStream(client.GetStream());
                    sslStream.AuthenticateAsServer(GetServerCert(SSLServerName), clientCertificateRequired: false,
                        enabledSslProtocols: SslProtocols.Default, checkCertificateRevocation: true);
                    stream = sslStream;
                }
                else
                {
                    Console.WriteLine("Talking to client over regular HTTP");
                    stream = client.GetStream();
                }

                using (stream)
                {
                    stream.ReadTimeout = 600000;

                    int bytesRead = 0;
                    do
                    {
                        try
                        {
                            bytesRead = stream.Read(bytes, 0, bytes.Length);
                            if (bytesRead > 0)
                            {
                                clientData.Append(Encoding.ASCII.GetString(bytes, 0, bytesRead));
                                stream.ReadTimeout = 500;
                            }
                        }
                        catch (IOException ioe)
                        {
                            Trace.WriteLine($"Read timed out:{ioe}");
                            bytesRead = 0;
                        }
                    } while (bytesRead > 0);

                    Console.WriteLine($"Client says:{clientData}");

                    bytes = Encoding.ASCII.GetBytes("Thanks call again!");

                    await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
                }
            }
            finally
            {
                client?.Close();
            }
        }

        private static X509Certificate GetServerCert(string subjectName)
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            //using (X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            //{
            store.Open(OpenFlags.ReadOnly);
            X509CertificateCollection certificate = store.Certificates.Find(X509FindType.FindBySubjectName,
                subjectName, validOnly: true);

            if (certificate.Count > 0)
            {
                return certificate[0];
            }
            else
            {
                return null;
            }
            //}
        }

        #endregion
    }
}
