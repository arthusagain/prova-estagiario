using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace TAREFA3
{
    //resolucao da tarefa 3
    class Tarefa3 : CsvEditor
    {
        public Tarefa3(string path) : base(path)
        {
        }

        public static void Main()
        {

            //le arquivo CEPs.csv
            using (CsvEditor leitor = new CsvEditor("../../../../../CEPs.csv"))
            {
                StringBuilder novoArquivo = new StringBuilder();
                CsvLinha linha = new CsvLinha();
                char separador = ';';

                //trata primeira linha de rotulos
                leitor.LerLinha(linha, separador);
                string novaLinha = string.Format("{0}; {1}; {2}; {3}; {4}; {5}; {6}; {7}; {8}\n", linha[0], linha[1], linha[2], linha[3], linha[4], linha[5], linha[6], linha[7], linha[8]);
                novoArquivo.Append(novaLinha);


                //itera linha a linha
                while (leitor.LerLinha(linha, separador))
                {
                    //produz endereco baseado no cep contido na primeira celula da linha
                    StringBuilder url = new StringBuilder();
                    url.Append("http://viacep.com.br/ws/");

                    string cepLido = linha[0];
                    
                    url.Append(String.Format("{0}/json", cepLido));
                    using (WebClient client = new WebClient())
                    {
                        //acessa endereco gerado
                        try
                        {

                            string webData = client.DownloadString(url.ToString());

                            int pos = 0;
                            bool conteudoRelevante = false;

                            //itera sobre cada caractere do endereco, registrando conteudo relevante na string geradora do arquivo final
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
                            //pula linha caso CEP nao esteja em formato aceito pelo site
                            Console.WriteLine("O CEP informado não tem formato valido");
                            novoArquivo.Append(String.Format("{0}; ; ; ; ; ; ; \n", cepLido) );
                        }
                    }

                }

                //usa string gerada para produzir arquivo de resultado

                File.WriteAllText("../../../CEPs - cópia.csv", novoArquivo.ToString());
            }
        }
    }

}
