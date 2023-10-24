using System;
using System.Net.Sockets;
using System.Text;

namespace ClienteRemetente
{
    class Cliente
    {
        static void Main()
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 5001);

                // Envie mensagem para iniciar a corrida
                Console.WriteLine("Enviando pedido para iniciar a corrida...");
                EnviarMensagem(client, "Pedido de corrida");


                while (true)
                {
                    string resposta = ReceberMensagem(client);
                    Console.WriteLine($"Resposta do servidor: {resposta}");
                }

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

}