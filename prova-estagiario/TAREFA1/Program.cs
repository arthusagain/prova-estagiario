using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace prova_estagiario.TAREFA1
{
    
    class Tarefa1 : CsvEditor
    {
        public Tarefa1(string path):base(path)
        {
        }

        public static void Main()
        {

            using (CsvEditor leitor = new CsvEditor("../../../../mapa.csv"))
            {
                StringBuilder novoArquivo = new StringBuilder(); 
                CsvLinha linha = new CsvLinha();
                char separador = ';';

                leitor.LerLinha(linha, separador);
                string novaLinha = string.Format("{0}; {1}\n", linha[0], linha[1]);
                novoArquivo.Append(novaLinha);

                while (leitor.LerLinha(linha, separador))
                {
                    string localizacao = linha[0];
                    string populacao = (Int32.Parse(linha[1]) * 2).ToString();
                    novaLinha = string.Format("{0}; {1}\n", localizacao, populacao);
                    novoArquivo.Append(novaLinha);
                }

                File.WriteAllText("../../../TAREFA1/resultado.csv", novoArquivo.ToString());
            }
        }
    }
}
