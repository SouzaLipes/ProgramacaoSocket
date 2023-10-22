using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {
        TcpListener server = null;
        try
        {
            // Definindo o endereço IP e a porta para o servidor
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 12345;

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
    private TcpClient destinatarioClient;

    public CorridaHandler(TcpClient remetenteClient, TcpClient destinatarioClient)
    {
        this.remetenteClient = remetenteClient;
        this.destinatarioClient = destinatarioClient;
    }

    public void Start()
    {
        // Inicie a corrida, envie mensagens entre remetente e destinatário, etc.
        // Implemente a lógica de corrida aqui conforme suas necessidades.
        // Este é apenas um exemplo básico para ilustrar a estrutura.
    }
}
