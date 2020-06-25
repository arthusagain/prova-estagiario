using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace TAREFA2
{
    class Tarefa2 : CsvEditor
    {
        public Tarefa2(string path) : base(path)
        {
        }

        public static void Main()
        {

            using (CsvEditor leitor = new CsvEditor("../../../../../mapa.csv"))
            {
                StringBuilder novoArquivo = new StringBuilder();
                CsvLinha linha = new CsvLinha();
                char separador = ';';

                leitor.LerLinha(linha, separador);
                string novaLinha = string.Format("{0}; {1}\n", linha[0], linha[1]);
                novoArquivo.Append(novaLinha);

                List<MapaCsv> lst = new List<MapaCsv>();

                while (leitor.LerLinha(linha, separador))
                {
                    MapaCsv elemento = new MapaCsv();
                    elemento.localizacao = linha[0];
                    elemento.populacao = linha[1];
                    lst.Add(elemento);
                }

                BubbleSort(lst);

                for (int i = 0; i < lst.Count; i++)
                {
                    string localizacao = lst[i].localizacao;
                    string populacao = lst[i].populacao;
                    novaLinha = string.Format("{0}; {1}\n", localizacao, populacao);
                    novoArquivo.Append(novaLinha);
                }

                File.WriteAllText("../../../resultado.csv", novoArquivo.ToString());

            }
        }

        public static void BubbleSort(List<MapaCsv> lista)
        {
            int tamanho = lista.Count;
            for (int i = 1; i < tamanho; i++)
            {
                for (int j = 0; j < (tamanho - i); j++)
                {
                    if (j<tamanho && Int32.Parse(lista[j].populacao) > Int32.Parse(lista[j + 1].populacao))
                    {
                        MapaCsv temp = lista[j];
                        lista[j] = lista[j + 1];
                        lista[j + 1] = temp;
                    }
                }
            }
        }
    }
}
