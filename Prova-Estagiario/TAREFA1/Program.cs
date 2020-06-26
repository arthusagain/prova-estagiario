using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace TAREFA1
{
    class Tarefa1 : CsvEditor
    {
        public Tarefa1(string path) : base(path)
        {
        }

        public static void Main()
        {
            //acessa arquivo necessario
            using (CsvEditor leitor = new CsvEditor("../../../../../mapa.csv"))
            {
                StringBuilder novoArquivo = new StringBuilder();
                CsvLinha linha = new CsvLinha();
                char separador = ';';

                //trata primeira linha de rotulos
                leitor.LerLinha(linha, separador);
                string novaLinha = string.Format("{0}; {1}\n", linha[0], linha[1]);
                novoArquivo.Append(novaLinha);

                //le cada linha, dobrando valores da coluna 'populacao'. escreve resultados em string
                while (leitor.LerLinha(linha, separador))
                {
                    string localizacao = linha[0];
                    string populacao = (Int32.Parse(linha[1]) * 2).ToString();
                    novaLinha = string.Format("{0}; {1}\n", localizacao, populacao);
                    novoArquivo.Append(novaLinha);
                }

                //usa string gerada para produzir arquivo de resultados
                File.WriteAllText("../../../resultado.csv", novoArquivo.ToString());
            }
        }
    }
}
