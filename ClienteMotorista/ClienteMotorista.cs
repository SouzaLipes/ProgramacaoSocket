using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClienteDestino
{
    class ClienteMotorista
    {
        static void Main()
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 5001);

                // Aguarde pedido de corrida do servidor
                string pedido = ReceberMensagem(client);
                Console.WriteLine($"Pedido de corrida recebido: {pedido}");

                Console.WriteLine("Deseja aceitar o pedido? [1]-Sim [2]-Não");
                string aceitaPedido = Console.ReadLine();

                bool InicarCorrida= false;

                if(aceitaPedido == "1"){
                     InicarCorrida = true;
                }else{
                    InicarCorrida = false;
                }
                
                bool finalizarPedido = false;

                if (InicarCorrida)
                {
                    // Inicie a corrida
                    Console.WriteLine("Aceitando pedido e iniciando a corrida...");
                    EnviarMensagem(client, "Pedido aceito. Corrida iniciada.");

                    do{
                        Console.WriteLine("Para finalizar o Peidido digite 'S' .");
                        string continuar = Console.ReadLine();

                        if(continuar == "S"){
                            finalizarPedido= true;
                        }

                    }while(finalizarPedido != false);

                    // Finalize a corrida (exemplo)
                    Console.WriteLine("Finalizando a corrida...");
                    EnviarMensagem(client, "Corrida finalizada pelo Motoristas.");
                }
                else
                {

                    EnviarMensagem(client, "Motoristas nao disponiveis no momento.");
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