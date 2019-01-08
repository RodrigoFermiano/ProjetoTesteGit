﻿using PrefeituraConecta.API.Utils.Enumeradores.Arquivos.PGDASD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PrefeituraConecta.API.Entidades.Arquivos.PGDASD;
using System.Linq;
using System.Reflection;
using PrefeituraConecta.API.Entidades.Arquivos;
using PrefeituraConecta.API.Entidades.Arquivos.PGDASD.Compatibilidade;

namespace PrefeituraConecta.API.Negocio
{
    public class SimplesNacionalBS
    {
        ArquivoTexto arquivo = new ArquivoTexto();

        public bool ImportarArquivo_PGDASD(string pathArquivo)
        {

            

            #region Declaração das listas

            List<REG_AAAAA_ARQUIVO_DIGITAL> Lista_REG_AAAAA = new List<REG_AAAAA_ARQUIVO_DIGITAL>();
            
            List<REG_00000_CONTRIBUINTE_DADOS_APURACAO> Lista_REG_00000 = new List<REG_00000_CONTRIBUINTE_DADOS_APURACAO>();

            List<REG_00001_PROCESSO_NAO_OPTANTE> Lista_REG_00001 = new List<REG_00001_PROCESSO_NAO_OPTANTE>();

            List<REG_01000_VALOR_APURADO_CALCULO> Lista_REG_01000 = new List<REG_01000_VALOR_APURADO_CALCULO>();

            List<REG_01100_PERFIL_DAS> Lista_REG_01100 = new List<REG_01100_PERFIL_DAS>();

            List<REG_01500_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO> Lista_REG_01500 = new List<REG_01500_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO>();

            List<REG_01501_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO_MERCADO_INTERNO> Lista_REG_01501 = new List<REG_01501_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO_MERCADO_INTERNO>();

            List<REG_01502_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO_MERCADO_EXTERNO> Lista_REG_01502 = new List<REG_01502_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO_MERCADO_EXTERNO>();

            List<REG_02000_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO_VALOR_ORIGINAL_TRIBUTOS_FIXOS> Lista_REG_02000 = new List<REG_02000_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO_VALOR_ORIGINAL_TRIBUTOS_FIXOS>();

            List<REG_03000_ESTABELECIMENTO_FILIAL> Lista_REG_03000 = new List<REG_03000_ESTABELECIMENTO_FILIAL>();

            List<REG_03100_ATIVIDADE_SELECIONADA_ESTABELECIMENTO> Lista_REG_03100 = new List<REG_03100_ATIVIDADE_SELECIONADA_ESTABELECIMENTO>();

            List<REG_03110_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_A> Lista_REG_03110 = new List<REG_03110_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_A>();

            List<REG_03111_VALOR_RECEITA_ISENCAO_FAIXA_A> Lista_REG_03111 = new List<REG_03111_VALOR_RECEITA_ISENCAO_FAIXA_A>();

            List<REG_03112_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_A> Lista_REG_03112 = new List<REG_03112_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_A>();

            List<REG_03120_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_B> Lista_REG_03120 = new List<REG_03120_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_B>();

            List<REG_03121_VALOR_RECEITA_ISENCAO_FAIXA_B> Lista_REG_03121 = new List<REG_03121_VALOR_RECEITA_ISENCAO_FAIXA_B>();

            List<REG_03122_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_B> Lista_REG_03122 = new List<REG_03122_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_B>();

            List<REG_03122_LAYOUT_ATE_2017> Lista_REG_03122_LAYOUT_ATE_2017 = new List<REG_03122_LAYOUT_ATE_2017>();


            List<REG_03130_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_C> Lista_REG_03130 = new List<REG_03130_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_C>();

            List<REG_03131_VALOR_RECEITA_ISENCAO_FAIXA_C> Lista_REG_03131 = new List<REG_03131_VALOR_RECEITA_ISENCAO_FAIXA_C>();
            
            List<REG_03132_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_C> Lista_REG_03132 = new List<REG_03132_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_C>();
            
            List<REG_03500_FOLHA_SALARIOS> Lista_REG_03500 = new List<REG_03500_FOLHA_SALARIOS>();
            
            List<REG_04000_PERFIL> Lista_REG_04000 = new List<REG_04000_PERFIL>();
            
            List<REG_99999_ENCERRAMENTO_BLOCO> Lista_REG_99999 = new List<REG_99999_ENCERRAMENTO_BLOCO>();
                       
            List<REG_ZZZZZ_ENCERRAMENTO_ARQUIVO_DIGITAL> Lista_REG_ZZZZZ = new List<REG_ZZZZZ_ENCERRAMENTO_ARQUIVO_DIGITAL>();
                       

            #endregion


            if (File.Exists(pathArquivo))
            {
                var arquivo = File.ReadAllLines(pathArquivo).ToList();

                bool inicioBloco = false;

                arquivo.ForEach(
                 linha =>
                 {
                     inicioBloco = true;

                     var dados = linha.Split('|');

                     var tipoRegistro = dados[0].ToString();


                     if (tipoRegistro.Equals(REG_AAAAA.TipoRegistro.Value))
                     {
                         var obj = new REG_AAAAA_ARQUIVO_DIGITAL();

                         AtribuirValoresMapeados(obj, dados, obj.GetType());

                         if (obj != null)
                         {
                             Lista_REG_AAAAA.Add(obj);
                         }

                     }

                     if (inicioBloco)
                     {
                         if (tipoRegistro.Equals(REG_00000.TipoRegistro.Value))
                         {
                             var obj = new REG_00000_CONTRIBUINTE_DADOS_APURACAO();
                          
                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_00000.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_00001.TipoRegistro.Value))
                         {

                             var obj = new REG_00001_PROCESSO_NAO_OPTANTE();
                             
                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_00001.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_01000.TipoRegistro.Value))
                         {

                             var obj = new REG_01000_VALOR_APURADO_CALCULO();
                           
                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_01000.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_01100.TipoRegistro.Value))
                         {

                             var obj = new REG_01100_PERFIL_DAS();
                            
                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_01100.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_01500.TipoRegistro.Value))
                         {

                             var obj = new REG_01500_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO();

                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_01500.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_01501.TipoRegistro.Value))
                         {

                             var obj = new REG_01501_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO_MERCADO_INTERNO();

                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_01501.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_01502.TipoRegistro.Value))
                         {

                             var obj = new REG_01502_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO_MERCADO_EXTERNO();

                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_01502.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_02000.TipoRegistro.Value))
                         {

                             var obj = new REG_02000_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO_VALOR_ORIGINAL_TRIBUTOS_FIXOS();

                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_02000.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_03000.TipoRegistro.Value))
                         {

                             var obj = new REG_03000_ESTABELECIMENTO_FILIAL();

                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_03000.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_03100.TipoRegistro.Value))
                         {

                             var obj = new REG_03100_ATIVIDADE_SELECIONADA_ESTABELECIMENTO();

                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_03100.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_03110.TipoRegistro.Value))
                         {

                             var obj = new REG_03110_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_A();

                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_03110.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_03112.TipoRegistro.Value))
                         {

                             var obj = new REG_03112_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_A();

                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_03112.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_03120.TipoRegistro.Value))
                         {

                             var obj = new REG_03120_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_B();

                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_03120.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_03121.TipoRegistro.Value))
                         {

                             var obj = new REG_03121_VALOR_RECEITA_ISENCAO_FAIXA_B();

                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_03121.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_03122.TipoRegistro.Value))
                         {

                             if (pathArquivo.Contains("PGDASD-"))
                             {
                                 var obj = new REG_03122_LAYOUT_ATE_2017();

                                 AtribuirValoresMapeados(obj, dados, obj.GetType());

                                 if (obj != null)
                                 {
                                     Lista_REG_03122_LAYOUT_ATE_2017.Add(obj);
                                 }
                             }

                             else
                             {
                                 var obj = new REG_03122_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_B();

                                 AtribuirValoresMapeados(obj, dados, obj.GetType());

                                 if (obj != null)
                                 {
                                     Lista_REG_03122.Add(obj);
                                 } 
                             }

                         }

                         if (tipoRegistro.Equals(REG_03130.TipoRegistro.Value))
                         {

                             var obj = new REG_03130_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_C();

                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_03130.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_03131.TipoRegistro.Value))
                         {

                             var obj = new REG_03131_VALOR_RECEITA_ISENCAO_FAIXA_C();

                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_03131.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_03132.TipoRegistro.Value))
                         {

                             var obj = new REG_03132_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_C();

                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_03132.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_03500.TipoRegistro.Value))
                         {

                             var obj = new REG_03500_FOLHA_SALARIOS();

                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_03500.Add(obj);
                             }

                         }

                         if (tipoRegistro.Equals(REG_04000.TipoRegistro.Value))
                         {

                             var obj = new REG_04000_PERFIL();

                             AtribuirValoresMapeados(obj, dados, obj.GetType());

                             if (obj != null)
                             {
                                 Lista_REG_04000.Add(obj);
                             }

                         }


                     }

                     if (tipoRegistro.Equals(REG_99999.TipoRegistro.Value))
                     {

                         inicioBloco = false;

                         var obj = new REG_99999_ENCERRAMENTO_BLOCO();

                         AtribuirValoresMapeados(obj, dados, obj.GetType());

                         if (obj != null)
                         {
                             Lista_REG_99999.Add(obj);
                         }

                     }

                     if (tipoRegistro.Equals(REG_ZZZZZ.TipoRegistro.Value))
                     {

                         inicioBloco = false;

                         var obj = new REG_ZZZZZ_ENCERRAMENTO_ARQUIVO_DIGITAL();

                         AtribuirValoresMapeados(obj, dados, obj.GetType());

                         if (obj != null)
                         {
                             Lista_REG_ZZZZZ.Add(obj);
                         }

                     }

                 });




            }


            return true;


        }

        private void AtribuirValoresMapeados(object obj, string[] dados, Type type)
        {
            try
            {
                var ContadorPropriedades = type.GetProperties().Count();


                int controleLoop = 0;

                List<PropertyInfo> campos = obj.GetType().GetProperties().Where(p => p.GetCustomAttribute(typeof(CampoArquivoPGDASD)) != null).ToList();
                campos = campos.OrderBy(c => c.GetCustomAttribute<CampoArquivoPGDASD>().Ordem).ToList();

                foreach (PropertyInfo propertyInfo in campos)
                {
                    for (int i = controleLoop; i < ContadorPropriedades; i++)
                    {
                        var valor = TratarValor(dados[controleLoop], propertyInfo);
                        propertyInfo.SetValue(obj, valor);
                    }

                    controleLoop++;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected object TratarValor(object valor, PropertyInfo propriedade)
        {
            try
            {
                if (propriedade.PropertyType == typeof(Int32))
                {
                    if (!valor.ToString().Equals(string.Empty))
                    {
                        return Convert.ToInt32(valor);
                    }

                    else
                    {
                        valor = (int)0;
                    }
                }

                if (propriedade.PropertyType == typeof(Int64))
                {
                    if (!valor.ToString().Equals(string.Empty))
                    {
                        return Convert.ToInt64(valor);
                    }

                    else
                    {
                        valor = (int)0;
                    }
                }

                if (propriedade.PropertyType == typeof(Decimal))
                {
                    if (!valor.ToString().Equals(string.Empty))
                    {
                        return Convert.ToDecimal(valor);
                    }

                    else
                    {
                        valor = (decimal)0.0;
                    }
                }


                return valor;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}

