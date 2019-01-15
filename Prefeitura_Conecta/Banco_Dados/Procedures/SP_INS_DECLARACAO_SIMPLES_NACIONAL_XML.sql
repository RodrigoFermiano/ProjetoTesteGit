USE [DB_PREFEITURA_CONECTA]
GO


IF EXISTS ( SELECT * 
            FROM   SYSOBJECTS 
            WHERE  ID = OBJECT_ID(N'[DBO].[SP_INS_DECLARACAO_SIMPLES_NACIONAL_XML]')  AND OBJECTPROPERTY(ID, N'ISPROCEDURE') = 1 )
BEGIN
    DROP PROCEDURE [DBO].[SP_INS_DECLARACAO_SIMPLES_NACIONAL_XML]
END

GO
CREATE PROCEDURE [DBO].[SP_INS_DECLARACAO_SIMPLES_NACIONAL_XML]
(
 
	 @COD_TOM INT,
	 @REG_AAAAA_ARQUIVO_DIGITAL xml,
	 @REG_00000_CONTRIBUINTE_DADOS_APURACAO xml,
	 @REG_00001_PROCESSO_NAO_OPTANTE xml,
	 @REG_01000_VALOR_APURADO_CALCULO xml,
	 @REG_01100_PERFIL_DAS xml,
	 @REG_01500_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO xml,
	 @REG_01501_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO_MERCADO_INTERNO xml,
	 @REG_01502_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO_MERCADO_EXTERNO xml,
	 @REG_02000_RECEITAS_BRUTAS_PERIODO_ANTERIORES_OPCAO_VALOR_ORIGINAL_TRIBUTOS_FIXOS xml,
	 @REG_03000_ESTABELECIMENTO_FILIAL xml,
	 @REG_03100_ATIVIDADE_SELECIONADA_ESTABELECIMENTO xml,
	 @REG_03110_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_A xml,
	 @REG_03111_VALOR_RECEITA_ISENCAO_FAIXA_A xml,
	 @REG_03112_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_A xml,
	 @REG_03120_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_B xml,
	 @REG_03121_VALOR_RECEITA_ISENCAO_FAIXA_B xml,
	 @REG_03122_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_B xml,
	 @REG_03122_LAYOUT_ATE_2017 xml,
	 @REG_03130_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_C xml,
	 @REG_03131_VALOR_RECEITA_ISENCAO_FAIXA_C xml,
	 @REG_03132_VALOR_RECEITA_ATIVIDADE_PERCENTUAL_FAIXA_C xml,
	 @REG_03500_FOLHA_SALARIOS xml,
	 @REG_04000_PERFIL xml,
	 @REG_99999_ENCERRAMENTO_BLOCO xml

)
AS

BEGIN
	
	SET NOCOUNT ON
	DECLARE @REG_00000_DOC AS INT

	EXEC SP_XML_PREPAREDOCUMENT @REG_00000_DOC OUTPUT , @REG_00000_CONTRIBUINTE_DADOS_APURACAO

	IF EXISTS(SELECT 1 [ID_PREFEITURA] FROM [DB_PREFEITURA_CONECTA].[dbo].[TB_PREFEITURA] WITH(NOLOCK) WHERE ID_PREFEITURA = @COD_TOM )
		BEGIN
			-- update
			SELECT 1 [ID_PREFEITURA] FROM [DB_PREFEITURA_CONECTA].[dbo].[TB_PREFEITURA] WITH(NOLOCK) WHERE ID_PREFEITURA = @COD_TOM
		END
		ELSE
		BEGIN 
			-- insert
			INSERT INTO TB_00000_CONTRIBUINTE_DADOS_APURACAO 
			(
				[REG], 						
				[PGDASD_ID_DECLARACAO],		
				[PGDASD_NUM_RECIBO], 		
				[PGDASD_NUM_AUTENTICACAO], 	
				[PGDASD_DT_TRANSMISSAO] ,	
				[PGDASD_VERSÃO] ,			
				[CNPJMATRIZ] ,				
				[NOME] 	,					
				[COD_TOM] ,					
				[OPTANTE] ,					
				[ABERTURA] ,					
				[PA] 	,					
				[RPA] 	,					
				[R] ,						
				[OPERACAO] ,					
				[REGIME] ,					
				[RPAC] 	,					
				[RPA_INT] ,					
				[RPA_EXT] 	,				
				[RPAC_INT] 	,				
				[RPAC_EXT]					
				
			)

			SELECT 

				OPER_REG_00000.REG ,
				OPER_REG_00000.Pgdasd_ID_Declaracao,
				OPER_REG_00000.Pgdasd_Num_Recibo,
				OPER_REG_00000.Pgdasd_Num_Autenticacao ,
				OPER_REG_00000.Pgdasd_Dt_Transmissao,
				OPER_REG_00000.Pgdasd_Versao,
				OPER_REG_00000.Cnpjmatriz,
				OPER_REG_00000.Nome,
				OPER_REG_00000.Cod_TOM ,
				OPER_REG_00000.Optante,
				OPER_REG_00000.Abertura,
				OPER_REG_00000.PA,
				OPER_REG_00000.Rpa,
				OPER_REG_00000.R,
				OPER_REG_00000.Operacao,
				OPER_REG_00000.Regime,
				OPER_REG_00000.RpaC,
				OPER_REG_00000.Rpa_string,
				OPER_REG_00000.Rpa_Ext,
				OPER_REG_00000.Rpac_string,
				OPER_REG_00000.Rpac_Ext

			FROM OPENXML(@REG_00000_DOC, 'ArrayOfREG_00000_CONTRIBUINTE_DADOS_APURACAO/REG_00000_CONTRIBUINTE_DADOS_APURACAO',2) WITH
			(
				REG numeric(5, 0) ,
				Pgdasd_ID_Declaracao numeric(17, 0) ,
				Pgdasd_Num_Recibo numeric(17, 0) ,
				Pgdasd_Num_Autenticacao numeric(20, 0) ,
				Pgdasd_Dt_Transmissao numeric(14, 0) ,
				Pgdasd_Versao varchar(20) ,
				Cnpjmatriz numeric(14, 0) ,
				Nome varchar(56) ,
				Cod_TOM numeric(4, 0) ,
				Optante char(2) ,
				Abertura datetime ,
				PA datetime ,
				Rpa numeric(18, 2) ,
				R numeric(18, 10) ,
				Operacao char(1) ,
				Regime numeric(1, 0) ,
				RpaC numeric(18, 2) ,
				Rpa_string numeric(18, 2) ,
				Rpa_Ext numeric(18, 2) ,
				Rpac_string numeric(18, 2) ,
				Rpac_Ext numeric(18, 2) 
				
			) AS OPER_REG_00000

			EXEC SP_XML_REMOVEDOCUMENT @REG_00000_DOC
		END
		
		SET NOCOUNT OFF
END

