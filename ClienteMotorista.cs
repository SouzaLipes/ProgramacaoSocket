using System;
using System.Net.Sockets;
using System.Text;

class ClienteMotorista
{
    static void Main()
    {
        try
        {
            TcpClient client = new TcpClient("127.0.0.1", 12345);

            // Envie mensagem para iniciar a corrida
            Console.WriteLine("Enviando pedido para iniciar a corrida...");
            EnviarMensagem(client, "Pedido de corrida");

            // Aguarde a resposta do servidor
            string resposta = ReceberMensagem(client);
            Console.WriteLine($"Resposta do servidor: {resposta}");

            // Lógica adicional aqui conforme necessário
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
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
