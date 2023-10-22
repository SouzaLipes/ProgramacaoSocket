using System;
using System.Net.Sockets;
using System.Text;

class Cliente
{
    static void Main()
    {
        try
        {
            TcpClient client = new TcpClient("127.0.0.1", 12345);

            // Aguarde pedido de corrida do servidor
            string pedido = ReceberMensagem(client);
            Console.WriteLine($"Pedido de corrida recebido: {pedido}");

            // Lógica para decidir se aceita ou não o pedido
            bool aceitaPedido = true; // Implemente sua lógica aqui

            if (aceitaPedido)
            {
                // Inicie a corrida
                Console.WriteLine("Aceitando pedido e iniciando a corrida...");
                EnviarMensagem(client, "Pedido aceito. Corrida iniciada.");

                // Lógica adicional durante a corrida aqui conforme necessário

                // Finalize a corrida (exemplo)
                Console.WriteLine("Finalizando a corrida...");
                EnviarMensagem(client, "Corrida finalizada pelo destinatário.");
            }
            else
            {
                // Informe o remetente que não há destinatário disponível
                Console.WriteLine("Recusando pedido. Destinatário não disponível.");
                EnviarMensagem(client, "Destinatário não disponível.");
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
