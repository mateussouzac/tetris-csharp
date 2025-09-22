using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsole
{
    internal class Tabuleiro
    {
        private readonly int[,] grade;
        private const int Altura = 20;
        private const int Largura = 10;
        public Tetrominos PecaAtual { get; private set; }
        private readonly Random random;

        public Tabuleiro()
        {
            grade = new int[Altura, Largura];
            random = new Random();
        }

        public bool GerarNovaPeca()
        {
            char formato;
            int tipoPeca = random.Next(3);
            switch (tipoPeca)
            {
                case 0:
                    formato = 'I';
                    break;
                case 1:
                    formato = 'L';
                    break;
                case 2:
                default:
                    formato = 'T';
                    break;
            }

            int centro = Largura / 2 - 1;
            PecaAtual = new Tetrominos(formato, centro, 0);

            return VerificarColisao(PecaAtual);
        }

        public bool VerificarColisao(Tetrominos peca)
        {
            int[,] matrizPeca = peca.GetMatriz();
            int tamanho = matrizPeca.GetLength(0);

            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    if (matrizPeca[i, j] == 1)
                    {
                        int xAbsoluto = peca.PosicaoX + j;
                        int yAbsoluto = peca.PosicaoY + i;

                        if (xAbsoluto < 0 || xAbsoluto >= Largura || yAbsoluto >= Altura)
                        {
                            return true;
                        }

                        if (yAbsoluto < 0)
                        {
                            continue;
                        }

                        if (grade[yAbsoluto, xAbsoluto] == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void FixarPecaNoTabuleiro()
        {
            int[,] matrizPeca = PecaAtual.GetMatriz();
            int tamanho = matrizPeca.GetLength(0);

            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    if (matrizPeca[i, j] == 1)
                    {
                        int xAbsoluto = PecaAtual.PosicaoX + j;
                        int yAbsoluto = PecaAtual.PosicaoY + i;
                        if (yAbsoluto >= 0 && yAbsoluto < Altura && xAbsoluto >= 0 && xAbsoluto < Largura)
                        {
                            grade[yAbsoluto, xAbsoluto] = 1;
                        }
                    }
                }
            }
            PecaAtual = null;
        }

        public int LimparLinhasCompletas()
        {
            int linhasLimpas = 0;
            for (int i = Altura - 1; i >= 0; i--)
            {
                bool linhaCompleta = true;
                for (int j = 0; j < Largura; j++)
                {
                    if (grade[i, j] == 0)
                    {
                        linhaCompleta = false;
                        break;
                    }
                }

                if (linhaCompleta)
                {
                    linhasLimpas++;
                    for (int k = i; k > 0; k--)
                    {
                        for (int j = 0; j < Largura; j++)
                        {
                            grade[k, j] = grade[k - 1, j];
                        }
                    }
                    for (int j = 0; j < Largura; j++)
                    {
                        grade[0, j] = 0;
                    }
                    i++;
                }
            }
            return linhasLimpas;
        }

        public void Desenhar(Jogador jogador)
        {
            Console.Clear();
            Console.WriteLine($"Jogador: {jogador.Nome} | Pontuação: {jogador.Pontuacao}");
            for (int i = 0; i < Largura + 2; i++) Console.Write("#");
            Console.WriteLine();

            for (int i = 0; i < Altura; i++)
            {
                Console.Write("1");
                for (int j = 0; j < Largura; j++)
                {
                    Console.Write(grade[i, j] == 1 ? "1" : " ");
                }
                Console.WriteLine("1");
            }

            for (int i = 0; i < Largura + 2; i++) Console.Write("#");
            Console.WriteLine();
            Console.WriteLine("Controles: A (Esquerda), D (Direita), S (Descer), Q (Girar), E (Girar)");


            if (PecaAtual != null)
            {
                int[,] matrizPeca = PecaAtual.GetMatriz();
                int tamanho = matrizPeca.GetLength(0);

                for (int i = 0; i < tamanho; i++)
                {
                    for (int j = 0; j < tamanho; j++)
                    {
                        if (matrizPeca[i, j] == 1)
                        {
                            int screenX = PecaAtual.PosicaoX + j + 1;
                            int screenY = PecaAtual.PosicaoY + i + 1;
                            if (screenY > 0)
                            {
                                Console.SetCursorPosition(screenX, screenY);
                                Console.Write("1");
                            }
                        }
                    }
                }
            }
        }
    }

}
