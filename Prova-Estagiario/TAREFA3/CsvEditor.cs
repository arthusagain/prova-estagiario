﻿using System;
using System.IO;
using System.Collections.Generic;

namespace TAREFA3
{
    public class CsvLinha : List<string>
    {
        public string TextoLinha { get; set; }
    }

    public class CEPsCsv
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string unidade { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }
    }

    public class MapaCsv
    {
        public string localizacao { get; set; }
        public string populacao { get; set; }
    }


    public class CsvEditor : StreamReader
    {
        public CsvEditor(string path)
        : base(path)
        {
        }

        public bool LerLinha(CsvLinha linha, char separator)
        {
            try
            {
                linha.TextoLinha = ReadLine();
                if (String.IsNullOrEmpty(linha.TextoLinha))
                    return false;

                int pos = 0;
                int linhaAtual = 0;

                while (pos < linha.TextoLinha.Length)
                {
                    string valor;

                    if (linha.TextoLinha[pos] == '"')
                    {
                        pos++;

                        int start = pos;
                        while (pos < linha.TextoLinha.Length)
                        {
                            if (linha.TextoLinha[pos] == '"')
                            {
                                pos++;

                                if (pos >= linha.TextoLinha.Length || linha.TextoLinha[pos] != '"')
                                {
                                    pos--;
                                    break;
                                }
                            }
                            pos++;
                        }
                        valor = linha.TextoLinha.Substring(start, pos - start);
                        valor = valor.Replace("\"\"", "\"");
                    }
                    else
                    {
                        int inicio = pos;
                        while (pos < linha.TextoLinha.Length && linha.TextoLinha[pos] != separator)
                            pos++;
                        valor = linha.TextoLinha.Substring(inicio, pos - inicio);
                    }

                    if (linhaAtual < linha.Count)
                        linha[linhaAtual] = valor;
                    else
                        linha.Add(valor);
                    linhaAtual++;

                    while (pos < linha.TextoLinha.Length && linha.TextoLinha[pos] != separator)
                        pos++;
                    if (pos < linha.TextoLinha.Length)
                        pos++;
                }
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
