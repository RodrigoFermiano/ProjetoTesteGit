using System;
using System.Collections.Generic;
using System.IO;
using PrefeituraConecta.API.Negocio;
using System.Configuration;

namespace PrefeituraConecta.CMD.Carga.PGDASD
{
    class Program
    {
        static void Main(string[] args)
        {

        

            var arquivos = Directory.GetFiles(@"C:\Projetos\GIT_HUB\Prefeitura_Conecta\Arquivos\ParaCarga");

            var ArquivosParaCarregar = @"C:\Projetos\Prefeitura_Conecta\Arquivos\ParaCarga";
            var ArquivosCarregados = @"C:\Projetos\Prefeitura_Conecta\Arquivos\CargaOK";

            SimplesNacionalBS simplesNacionalBS = new SimplesNacionalBS();

            Console.WriteLine(" ----- INICIO DO PROCESSAMENTO ----- {0},", DateTime.Now);

            foreach (var arquivo in arquivos)
            {
               

                Console.WriteLine("Inicio da carga do arquivo: {0}", arquivo);

                simplesNacionalBS.ImportarArquivo_PGDASD(arquivo);

                Console.WriteLine("Fim da carga do arquivo: {0}", arquivo);

                //File.Move(ArquivosParaCarregar + "\\" + arquivo, ArquivosCarregados + "\\" + arquivo, );
                          
            }

            Console.WriteLine(" ----- FINAL DO PROCESSAMENTO ----- {0},", DateTime.Now);
            Console.ReadLine();
        }
    }
}
