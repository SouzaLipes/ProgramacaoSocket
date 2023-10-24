using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Program
{
    class Server
    {
        static void Main()
        {
            TcpListener server = null;
            try
            {
                // Definindo o endereço IP e a porta para o servidor
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                int port = 5001;

                // Inicializando o servidor
                server = new TcpListener(ipAddress, port);
                server.Start();

                Console.WriteLine("Servidor iniciado...");

                while (true)
                {
                    Console.WriteLine("Aguardando conexão do remetente...");
                    TcpClient remetenteClient = server.AcceptTcpClient();
                    Console.WriteLine("Remetente conectado!");

                    Console.WriteLine("Aguardando conexão do destinatário...");
                    TcpClient destinatarioClient = server.AcceptTcpClient();
                    Console.WriteLine("Destinatário conectado!");

                    // Inicia uma nova thread para gerenciar a corrida entre o remetente e o destinatário
                    new CorridaHandler(remetenteClient, destinatarioClient).Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                server?.Stop();
            }
        }
    }

    class CorridaHandler
    {
        private TcpClient remetenteClient;
        private TcpClient clienteMotorista;



        public CorridaHandler(TcpClient remetenteClient, TcpClient clienteMotorista)
        {
            this.remetenteClient = remetenteClient;
            this.clienteMotorista = clienteMotorista;
        }

        public void Start()
        {
            try{

                while (true)
                {
                    string mensagemRemetente = ReceberMensagem(remetenteClient);
                    Console.WriteLine($"Mensagem do remetente: {mensagemRemetente}");
                    EnviarMensagem(clienteMotorista, "Deseja aceitar o pedido?");

                    string mensagemDestinatario = ReceberMensagem(clienteMotorista);
                    Console.WriteLine($"Mensagem do destinatário: {mensagemDestinatario}");

                    EnviarMensagem(remetenteClient, mensagemDestinatario);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante a corrida: {ex.Message}");
            }
        }
        static void EnviarMensagem(TcpClient client, string mensagem)
        {
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(mensagem);
            stream.Write(data, 0, data.Length);
        }

        static string ReceberMensagem(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] data = new byte[256];
            int bytes = stream.Read(data, 0, data.Length);
            return Encoding.ASCII.GetString(data, 0, bytes);
        }
    }
}