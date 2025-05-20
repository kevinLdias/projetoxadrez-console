using System.Reflection.Metadata.Ecma335;
using tabuleiro;

namespace jogoxadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez _partida;
        public Rei(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab)
        {
            _partida = partida;
        }
        public override string ToString()
        {
            return "R";
        }
        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.ReturnPeca(pos);
            return p == null || p.Cor != Cor;
        }
        private bool TesteTorreParaRoque(Posicao pos)
        {
            Peca p = Tab.ReturnPeca(pos);

            return p != null && p is Torre && p.Cor == Cor && p.QteMovimentos == 0;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            // acima
            pos.DefinirValorDaPosicao(Posicao.Linha - 1, Posicao.Coluna);

            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // nordeste
            pos.DefinirValorDaPosicao(Posicao.Linha - 1, Posicao.Coluna + 1);

            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // direita
            pos.DefinirValorDaPosicao(Posicao.Linha, Posicao.Coluna + 1);

            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // sudeste
            pos.DefinirValorDaPosicao(Posicao.Linha + 1, Posicao.Coluna + 1);

            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // abaixo
            pos.DefinirValorDaPosicao(Posicao.Linha + 1, Posicao.Coluna);

            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // sudoeste
            pos.DefinirValorDaPosicao(Posicao.Linha + 1, Posicao.Coluna - 1);

            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // esquerda
            pos.DefinirValorDaPosicao(Posicao.Linha, Posicao.Coluna - 1);

            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // noroeste
            pos.DefinirValorDaPosicao(Posicao.Linha - 1, Posicao.Coluna - 1);

            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // #Jogada especial Roque

            if (QteMovimentos == 0 && (!_partida.Xeque))
            {
                // #Roque pequeno
                Posicao posicaoDaTorre1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TesteTorreParaRoque(posicaoDaTorre1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);

                    if (Tab.ReturnPeca(p1) == null && Tab.ReturnPeca(p2) == null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }

                // #Roque Grande
                Posicao posicaoDaTorre2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TesteTorreParaRoque(posicaoDaTorre2))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);

                    if (Tab.ReturnPeca(p1) == null && Tab.ReturnPeca(p2) == null && Tab.ReturnPeca(p3) == null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}
