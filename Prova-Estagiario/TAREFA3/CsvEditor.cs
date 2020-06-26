using System;
using System.IO;
using System.Collections.Generic;

namespace TAREFA3
{
    //Classe CsvLinha: padroniza o armazenamento do texto das linhas de um arquivo .csv durante a leitura
    public class CsvLinha : List<string>
    {
        public string TextoLinha { get; set; }
    }

    //Classe MapaCsv: organiza uma linha do arquivo mapa.csv como objeto para ser usado em outras estruturas de dados
    public class MapaCsv
    {
        public string localizacao { get; set; }
        public string populacao { get; set; }
    }

    //Classe CsvEditor: responsável pela leitura de arquivos .csv
    public class CsvEditor : StreamReader
    {
        public CsvEditor(string path)
        : base(path)
        {
        }

        //Método LerLinha(): processa uma linha de um arquivo .csv, transformando Stream em CsvLinha
        public bool LerLinha(CsvLinha linha, char separator)
        {
            //tenta ler arquivo
            try
            {
                //se arquivo vazio, termina
                linha.TextoLinha = ReadLine();
                if (String.IsNullOrEmpty(linha.TextoLinha))
                    return false;

                int pos = 0;
                int linhaAtual = 0;

                //itera por cada caracter do arquivo
                while (pos < linha.TextoLinha.Length)
                {
                    string valor;

                    //trata os casos em que o elemento atual da leitura é ' " '
                    if (linha.TextoLinha[pos] == '"')
                    {
                        pos++;

                        //marca inicio das aspas e busca final
                        int start = pos;
                        while (pos < linha.TextoLinha.Length)
                        {
                            //encontrando o par da ", termina busca
                            if (linha.TextoLinha[pos] == '"')
                            {
                                pos++;

                                //trata caso não exista par para as aspas duplas que iniciaram essa busca
                                if (pos >= linha.TextoLinha.Length || linha.TextoLinha[pos] != '"')
                                {
                                    pos--;
                                    break;
                                }
                            }
                            pos++;
                        }

                        //adiciona \" no inicio e fim das aspas para que perteçam a string gerada
                        valor = linha.TextoLinha.Substring(start, pos - start);
                        valor = valor.Replace("\"\"", "\"");
                    }
                    //caso não sejam aspas
                    else
                    {
                        //adiciona trecho sem aspas a string gerada
                        int inicio = pos;
                        while (pos < linha.TextoLinha.Length && linha.TextoLinha[pos] != separator)
                            pos++;
                        valor = linha.TextoLinha.Substring(inicio, pos - inicio);
                    }

                    //se nao foi a linha final, a string gerada representa esta linha, senao adiciona ao final
                    if (linhaAtual < linha.Count)
                        linha[linhaAtual] = valor;
                    else
                        linha.Add(valor);
                    linhaAtual++;

                    //trata espaçamento criado pelo caracter separador
                    while (pos < linha.TextoLinha.Length && linha.TextoLinha[pos] != separator)
                        pos++;
                    if (pos < linha.TextoLinha.Length)
                        pos++;
                }
                //remove linha excessiva
                while (linha.Count > linhaAtual)
                    linha.RemoveAt(linhaAtual);


                return (linha.Count > 0);
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }


    }
}
