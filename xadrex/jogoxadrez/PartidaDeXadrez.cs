using tabuleiro;
using xadrex;

namespace jogoxadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            ColocarAsPecas();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }
        public void ValidaPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.ReturnPeca(pos) == null) 
            {
                throw new TabuleiroException("\nNão existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != Tab.ReturnPeca(pos).Cor)
            {
                throw new TabuleiroException("\nA peça de origem escolhida não é sua!");
            }
            if (!Tab.ReturnPeca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("\nNão há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidaPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.ReturnPeca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("\nPosição de destino inválida");
            }

        }
        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Azul;
            }
            else { JogadorAtual = Cor.Branca; }
        }
        private void ColocarAsPecas()
        {
            Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('c', 1).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('c', 2).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('d', 2).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('e', 2).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('e', 1).ToPosicao());
            Tab.ColocarPeca(new Rei(Cor.Branca, Tab), new PosicaoXadrez('d', 1).ToPosicao());
            //----------------------------------------------------------------------------------
            Tab.ColocarPeca(new Torre(Cor.Azul, Tab), new PosicaoXadrez('c', 7).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Azul, Tab), new PosicaoXadrez('c', 8).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Azul, Tab), new PosicaoXadrez('d', 7).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Azul, Tab), new PosicaoXadrez('e', 7).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Azul, Tab), new PosicaoXadrez('e', 8).ToPosicao());
            Tab.ColocarPeca(new Rei(Cor.Azul, Tab), new PosicaoXadrez('d', 8).ToPosicao());
        }

    }
}
