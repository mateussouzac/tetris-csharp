using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsole
{
    class Program 
    {
        static void Main(string[] args) 
        {
            Console.WriteLine("Digite o nome do jogador:");
            string nome = Console.ReadLine();
            Jogador jogador = new Jogador(nome);
            Tabuleiro tabuleiro = new Tabuleiro();

            bool gameOver = false;

            while (!gameOver)
            {
                if (tabuleiro.PecaAtual == null)
                {
                    if (tabuleiro.GerarNovaPeca())
                    {
                        gameOver = true;
                        continue;
                    }
                }

                tabuleiro.Desenhar(jogador);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    Tetrominos clone = tabuleiro.PecaAtual.Clone();

                    switch (key.Key)
                    {
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.A:
                            clone.PosicaoX--;
                            break;
                        case ConsoleKey.RightArrow:
                        case ConsoleKey.D:
                            clone.PosicaoX++;
                            break;
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.S:
                            clone.PosicaoY++;
                            break;
                        case ConsoleKey.Q:
                            clone.Rotacionar(false);
                            break;
                        case ConsoleKey.E:
                            clone.Rotacionar(true);
                            break;
                    }

                    if (!tabuleiro.VerificarColisao(clone))
                    {
                        tabuleiro.PecaAtual.PosicaoX = clone.PosicaoX;
                        tabuleiro.PecaAtual.PosicaoY = clone.PosicaoY;

                        if (key.Key == ConsoleKey.Q) tabuleiro.PecaAtual.Rotacionar(false);
                        if (key.Key == ConsoleKey.E) tabuleiro.PecaAtual.Rotacionar(true);
                    }
                    else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                    {
                        jogador.AdicionarPontos(tabuleiro.PecaAtual.PontosPorPeca);
                        tabuleiro.FixarPecaNoTabuleiro();
                        int linhasLimpas = tabuleiro.LimparLinhasCompletas();
                        if (linhasLimpas > 0)
                        {
                            int pontosLinha = 300 + (linhasLimpas - 1) * 100;
                            jogador.AdicionarPontos(pontosLinha);
                        }
                    }
                }
                else
                {
                    Thread.Sleep(150);
                }
            }

            Console.Clear();
            Console.WriteLine("Fim de Jogo!");
            Console.WriteLine($"Jogador: {jogador.Nome}");
            Console.WriteLine($"Pontuação Final: {jogador.Pontuacao}");
            jogador.SalvarPontuacao();
            Console.WriteLine("Pontuação salva em scores.txt");
            Console.WriteLine("Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }
    }
}
    


