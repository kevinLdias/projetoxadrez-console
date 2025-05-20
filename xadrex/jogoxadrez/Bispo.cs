using tabuleiro;

namespace jogoxadrez
{
    class Bispo : Peca
    {
        public Bispo(Cor cor, Tabuleiro tab) : base(cor, tab)
        {
        }
        public override string ToString()
        {
            return "B";
        }
        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.ReturnPeca(pos);
            return p == null || p.Cor != Cor;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            // noroeste
            pos.DefinirValorDaPosicao(Posicao.Linha - 1, Posicao.Coluna - 1);

            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;

                if (Tab.ReturnPeca(pos) != null && Tab.ReturnPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.DefinirValorDaPosicao(pos.Linha - 1, pos.Coluna - 1);
            }

            // nordeste
            pos.DefinirValorDaPosicao(Posicao.Linha - 1, Posicao.Coluna + 1);

            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;

                if (Tab.ReturnPeca(pos) != null && Tab.ReturnPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.DefinirValorDaPosicao(pos.Linha - 1, pos.Coluna + 1);
            }

            // sudeste
            pos.DefinirValorDaPosicao(Posicao.Linha + 1, Posicao.Coluna + 1);

            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;

                if (Tab.ReturnPeca(pos) != null && Tab.ReturnPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.DefinirValorDaPosicao(pos.Linha + 1, pos.Coluna + 1);
            }

            // sudoeste
            pos.DefinirValorDaPosicao(Posicao.Linha + 1, Posicao.Coluna - 1);

            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;

                if (Tab.ReturnPeca(pos) != null && Tab.ReturnPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.DefinirValorDaPosicao(pos.Linha + 1, pos.Coluna - 1);
            }
            return mat;
        }

    }
}

