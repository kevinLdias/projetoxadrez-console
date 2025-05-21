using System.Reflection.Metadata.Ecma335;
using tabuleiro;

namespace jogoxadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez _partida;
        public Peao(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab)
        {
            _partida = partida;
        }
        public override string ToString()
        {
            return "P";
        }
        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = Tab.ReturnPeca(pos);
            return p != null && p.Cor != Cor;
        }

        private bool Livre(Posicao pos)
        {
            return Tab.ReturnPeca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                pos.DefinirValorDaPosicao(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValorDaPosicao(Posicao.Linha - 2, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos) && QteMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValorDaPosicao(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValorDaPosicao(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // Jogada especial EnPassant
                if (Posicao.Linha == 3)
                {
                    Posicao esq = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.PosicaoValida(esq) && ExisteInimigo(esq) && Tab.ReturnPeca(esq) == _partida.VulneravelEnPassant)
                    {
                        mat[esq.Linha - 1, esq.Coluna] = true;
                    }

                    Posicao dir = new Posicao(Posicao.Linha, Posicao.Coluna + 1);

                    if (Tab.PosicaoValida(dir) && ExisteInimigo(dir) && Tab.ReturnPeca(dir) == _partida.VulneravelEnPassant)
                    {
                        mat[dir.Linha - 1, dir.Coluna] = true;
                    }
                }
            }
            else
            {
                pos.DefinirValorDaPosicao(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValorDaPosicao(Posicao.Linha + 2, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos) && QteMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValorDaPosicao(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValorDaPosicao(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // Jogada especial EnPassant
                if (Posicao.Linha == 4)
                {
                    Posicao esq = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.PosicaoValida(esq) && ExisteInimigo(esq) && Tab.ReturnPeca(esq) == _partida.VulneravelEnPassant)
                    {
                        mat[esq.Linha + 1, esq.Coluna] = true;
                    }

                    Posicao dir = new Posicao(Posicao.Linha, Posicao.Coluna + 1);

                    if (Tab.PosicaoValida(dir) && ExisteInimigo(dir) && Tab.ReturnPeca(dir) == _partida.VulneravelEnPassant)
                    {
                        mat[dir.Linha + 1, dir.Coluna] = true;
                    }
                }
            }
            return mat;
        }
    }
}
