using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;
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
        private HashSet<Peca> _pecas;
        private HashSet<Peca> _pecasCapturadas;
        public bool Xeque { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            _pecas = new HashSet<Peca>();
            _pecasCapturadas = new HashSet<Peca>();
            ColocarAsPecas();
        }
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);

            if (pecaCapturada != null)
            {
                _pecasCapturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }
        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.IncrementarQteMovimentos();

            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                _pecasCapturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p, origem);

        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (EstaEmXeque(CorAdversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else { Xeque = false; }

            if (TesteXequeMate(CorAdversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
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
            if (!Tab.ReturnPeca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("\n" +
                    "Posição de destino inválida");
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

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca p in _pecasCapturadas)
            {
                if (p.Cor == cor)
                {
                    aux.Add(p);
                }
            }
            return aux;
        }
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca p in _pecas)
            {
                if (p.Cor == cor)
                {
                    aux.Add(p);
                }
            }
            aux.ExceptWith(_pecasCapturadas);
            return aux;

        }

        private Cor CorAdversaria(Cor cor)
        {
            return (cor == Cor.Branca) ? Cor.Azul : Cor.Branca;
        }
        private Peca Rei(Cor cor)
        {
            foreach (Peca p in PecasEmJogo(cor))
            {
                if (p is Rei)
                {
                    return p;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            foreach (Peca p in PecasEmJogo(CorAdversaria(cor)))
            {
                Peca r = Rei(cor);
                if (r == null)
                {
                    throw new TabuleiroException($"Não existe rei da cor {cor} no tabuleiro!");
                }
                bool[,] mat = p.MovimentosPossiveis();
                if (mat[r.Posicao.Linha, r.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca p in PecasEmJogo(cor))
            {
                bool[,] mat = p.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = p.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);

                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }

        private void ColocarAsPecas()
        {
            // Brancas
            ColocarNovaPeca('a', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('b', 1, new Cavalo(Cor.Branca, Tab));
            ColocarNovaPeca('c', 1, new Bispo(Cor.Branca, Tab));
            ColocarNovaPeca('d', 1, new Dama(Cor.Branca, Tab));
            ColocarNovaPeca('e', 1, new Rei(Cor.Branca, Tab));
            ColocarNovaPeca('f', 1, new Bispo(Cor.Branca, Tab));
            ColocarNovaPeca('g', 1, new Cavalo(Cor.Branca, Tab));
            ColocarNovaPeca('h', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('a', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('b', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('c', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('d', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('e', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('f', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('g', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('h', 2, new Peao(Cor.Branca, Tab));

            //----------------------------------------------------------------------------------

            // Azuis
            ColocarNovaPeca('a', 8, new Torre(Cor.Azul, Tab));
            ColocarNovaPeca('b', 8, new Cavalo(Cor.Azul, Tab));
            ColocarNovaPeca('c', 8, new Bispo(Cor.Azul, Tab));
            ColocarNovaPeca('d', 8, new Dama(Cor.Azul, Tab));
            ColocarNovaPeca('e', 8, new Rei(Cor.Azul, Tab));
            ColocarNovaPeca('f', 8, new Bispo(Cor.Azul, Tab));
            ColocarNovaPeca('g', 8, new Cavalo(Cor.Azul, Tab));
            ColocarNovaPeca('h', 8, new Torre(Cor.Azul, Tab));
            ColocarNovaPeca('a', 7, new Peao(Cor.Azul, Tab));
            ColocarNovaPeca('b', 7, new Peao(Cor.Azul, Tab));
            ColocarNovaPeca('c', 7, new Peao(Cor.Azul, Tab));
            ColocarNovaPeca('d', 7, new Peao(Cor.Azul, Tab));
            ColocarNovaPeca('e', 7, new Peao(Cor.Azul, Tab));
            ColocarNovaPeca('f', 7, new Peao(Cor.Azul, Tab));
            ColocarNovaPeca('g', 7, new Peao(Cor.Azul, Tab));
            ColocarNovaPeca('h', 7, new Peao(Cor.Azul, Tab));
        }
    }
}
