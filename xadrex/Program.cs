using tabuleiro;
using jogoxadrez;
using jogoxadrex;

namespace xadrex
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.ColocarPeca(new Torre(Cor.Preta, tab), new Posicao(0, 0));
                tab.ColocarPeca(new Torre(Cor.Branca, tab), new Posicao(1, 3));
                tab.ColocarPeca(new Rei(Cor.Preta, tab), new Posicao(2, 4));

                tab.ColocarPeca(new Torre(Cor.Branca, tab), new Posicao(2, 7));

                Tela.ImprimirTabuleiro(tab);
            }
            catch (TabuleiroException tabException)
            {
                Console.WriteLine(tabException.Message);
            }
            


        }
    }
}
