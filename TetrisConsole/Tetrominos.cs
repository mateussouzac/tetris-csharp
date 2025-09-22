using System;
namespace TetrisConsole
{
    public class Tetrominos
    {
        public int[,] Matriz { get; private set; }
        public char Formato { get; private set; }
        public int PosicaoX { get; set; }
        public int PosicaoY { get; set; }
        public int PontosPorPeca { get; private set; }

        public Tetrominos(char formato, int x, int y)
        {
            this.Formato = formato;
            this.PosicaoX = x;
            this.PosicaoY = y;
            this.Matriz = new int[3, 3];

            if (formato == 'I')
            {
                for (int i = 0; i < Matriz.GetLength(0); i++)
                {
                    Matriz[i, 1] = 1;
                }
                PontosPorPeca = 3;
            }
            else if (formato == 'L')
            {
                for (int i = 0; i < Matriz.GetLength(0); i++)
                {
                    Matriz[i, 1] = 1;
                }
                Matriz[2, 2] = 1;
                PontosPorPeca = 4;
            }
            else if (formato == 'T')
            {
                for (int i = 0; i < Matriz.GetLength(0); i++)
                {
                    Matriz[i, 1] = 1;
                }
                Matriz[2, 0] = 1;
                Matriz[2, 2] = 1;
                PontosPorPeca = 5;
            }
        }

        private Tetrominos(Tetrominos other)
        {
            this.Formato = other.Formato;
            this.PosicaoX = other.PosicaoX;
            this.PosicaoY = other.PosicaoY;
            this.PontosPorPeca = other.PontosPorPeca;
            this.Matriz = (int[,])other.Matriz.Clone();
        }

        public int[,] GetMatriz()
        {
            return Matriz;
        }

        public void Rotacionar(bool sentidoHorario)
        {
            int tamanho = Matriz.GetLength(0);
            int[,] novaMatriz = new int[tamanho, tamanho];

            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    if (sentidoHorario)
                    {
                        novaMatriz[j, tamanho - 1 - i] = Matriz[i, j];
                    }
                    else
                    {
                        novaMatriz[tamanho - 1 - j, i] = Matriz[i, j];
                    }
                }
            }
            Matriz = novaMatriz;
        }

        public Tetrominos Clone()
        {
            return new Tetrominos(this);
        }

        public void VisualizarPeca()
        {
            for (int i = 0; i < Matriz.GetLength(0); i++)
            {
                for (int j = 0; j < Matriz.GetLength(1); j++)
                {
                    if (Matriz[i, j] == 1)
                    {
                        Console.Write("1 ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
