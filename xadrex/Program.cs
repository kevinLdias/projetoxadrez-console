using tabuleiro;
using jogoxadrez;

namespace xadrex
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();
                while (!partida.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.Tab);

                        Console.WriteLine();
                        Console.WriteLine("Turno: " + partida.Turno);
                        Console.WriteLine("Aguardando joga da peça: " + partida.JogadorAtual);

                        Console.WriteLine();

                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

                        partida.ValidaPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partida.Tab.ReturnPeca(origem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.Tab, posicoesPossiveis);

                        Console.Write("\nDestino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                        partida.ValidaPosicaoDeDestino(origem, destino);

                        partida.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException tabException)
                    {
                        Console.WriteLine(tabException.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch (TabuleiroException tabException)
            {
                Console.WriteLine(tabException.Message);
            }



        }
    }
}
