using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsole
{
    public class Jogador
    {
        public string Nome { get; private set; }
        public int Pontuacao { get; private set; }

        public Jogador(string nome)
        {
            Nome = nome;
            Pontuacao = 0;
        }

        public void AdicionarPontos(int pontos)
        {
            Pontuacao += pontos;
        }

        public void SalvarPontuacao()
        {
            try
            {
                string registro = $"{Nome};{Pontuacao}{Environment.NewLine}";
                File.AppendAllText("scores.txt", registro);
            }
            catch (Exception)
            {
                Console.WriteLine("Erro ao salvar a pontuação.");
            }
        }
    }

}
