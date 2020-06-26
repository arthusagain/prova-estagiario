using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace TAREFA3
{
    class Tarefa3 : CsvEditor
    {
        public Tarefa3(string path) : base(path)
        {
        }

        public static void Main()
        {

            using (CsvEditor leitor = new CsvEditor("../../../../../CEPs.csv"))
            {
                StringBuilder novoArquivo = new StringBuilder();
                CsvLinha linha = new CsvLinha();
                char separador = ';';

                leitor.LerLinha(linha, separador);
                string novaLinha = string.Format("{0}; {1}; {2}; {3}; {4}; {5}; {6}; {7}; {8}\n", linha[0], linha[1], linha[2], linha[3], linha[4], linha[5], linha[6], linha[7], linha[8]);
                novoArquivo.Append(novaLinha);

                while (leitor.LerLinha(linha, separador))
                {
                    StringBuilder url = new StringBuilder();
                    url.Append("http://viacep.com.br/ws/");

                    string cepLido = linha[0];
                    
                    url.Append(String.Format("{0}/json", cepLido));
                    using (WebClient client = new WebClient())
                    {
                        try
                        {

                            string webData = client.DownloadString(url.ToString());

                            int pos = 0;
                            bool conteudoRelevante = false;


                            Console.WriteLine(webData);
                            while (pos < webData.Length)
                            {
                                if(webData[pos] == ':')
                                {
                                    pos += 3;
                                    conteudoRelevante = true;
                                }
                                if(conteudoRelevante && webData[pos] != '"')
                                {
                                    novoArquivo.Append(webData[pos]);
                                }
                                else if(conteudoRelevante)
                                {
                                    novoArquivo.Append("; ");
                                    conteudoRelevante = false;
                                }
                                pos++;
                            }
                            novoArquivo.Append("\n");

                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("O CEP informado não tem formato valido");
                            novoArquivo.Append(String.Format("{0}; ; ; ; ; ; ; \n", cepLido) );
                        }
                    }

                }

                File.WriteAllText("../../../CEPs - cópia.csv", novoArquivo.ToString());
            }
        }
    }

}
