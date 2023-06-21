IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_Dashboard')
    DROP PROCEDURE sp_Select_Dashboard
GO

CREATE PROCEDURE sp_Select_Dashboard
(
    @filtro VARCHAR(50) = ''
)
AS
SET NOCOUNT OFF;

    SELECT dispositivo_s_dispositivo				AS dashboard_s_dispositivo
	     , dispositivo_i_id							AS dashboard_i_dispositivo
	     , CONVERT(FLOAT,0)					        AS dashboard_f_temperatura
         , CONVERT(FLOAT,0)					        AS dashboard_f_umidade 
		 , CONVERT(DATETIME,'')						AS dashboard_d_ultimo_sinal
		 , dispositivo_f_delay						AS dashboard_f_delay
		 , CONVERT(VARCHAR(20),'')					AS dashboard_s_status
		 , CONVERT(VARCHAR(20),'')					AS dashboard_s_cor_borda
		 , CONVERT(VARCHAR(20),'')					AS dashboard_s_cor_temperatura
		 , CONVERT(VARCHAR(20),'')					AS dashboard_s_cor_umidade
		 , CONVERT(VARCHAR(20),'')					AS dashboard_s_icone_temperatura
		 , CONVERT(VARCHAR(20),'')					AS dashboard_s_icone_umidade
	  INTO #TMP
      FROM tb_dispositivo A(NOLOCK)
     WHERE dispositivo_b_deleted = 0 
	   AND dispositivo_s_status = 'Ativo'

	UPDATE #TMP SET dashboard_f_temperatura = (SELECT SUM(monitoramento_f_temperatura)/5 AS soma
												 FROM (SELECT TOP 5 monitoramento_f_temperatura
													     FROM tb_monitoramento (NOLOCK)
														WHERE monitoramento_i_dispositivo = dashboard_i_dispositivo
													    ORDER 
														   BY monitoramento_d_data DESC) AS TOP5)

	UPDATE #TMP SET dashboard_f_umidade = (SELECT SUM(monitoramento_f_umidade)/5 AS soma
												             FROM (SELECT TOP 5 monitoramento_f_umidade
												             	     FROM tb_monitoramento (NOLOCK)
												             		WHERE monitoramento_i_dispositivo = dashboard_i_dispositivo
												             	    ORDER 
												             		   BY monitoramento_d_data DESC) AS TOP5)

	UPDATE #TMP SET dashboard_d_ultimo_sinal = (SELECT TOP 1 monitoramento_d_data
										          FROM tb_monitoramento (NOLOCK)
											     WHERE monitoramento_i_dispositivo = dashboard_i_dispositivo
												 ORDER
												    BY monitoramento_d_data DESC)


	UPDATE #TMP SET dashboard_s_cor_temperatura = 'primary' WHERE dashboard_f_temperatura <= 22
	UPDATE #TMP SET dashboard_s_cor_temperatura = 'warning' WHERE dashboard_f_temperatura > 22 AND dashboard_f_temperatura <= 24
	UPDATE #TMP SET dashboard_s_cor_temperatura = 'danger' WHERE dashboard_f_temperatura >= 24


	UPDATE #TMP SET dashboard_s_cor_umidade = 'primary' WHERE dashboard_f_umidade <= 60
	UPDATE #TMP SET dashboard_s_cor_umidade = 'warning' WHERE dashboard_f_umidade > 60 AND dashboard_f_umidade < 80
	UPDATE #TMP SET dashboard_s_cor_umidade = 'danger' WHERE dashboard_f_umidade > 80

	UPDATE #TMP SET dashboard_s_icone_umidade = 'fa-check'  WHERE dashboard_f_umidade <= 60
	UPDATE #TMP SET dashboard_s_icone_umidade = 'fa-arrow-up' WHERE dashboard_f_umidade > 60

	UPDATE #TMP SET dashboard_s_icone_temperatura = 'fa-check'  WHERE dashboard_f_temperatura <= 22
	UPDATE #TMP SET dashboard_s_icone_temperatura = 'fa-arrow-up' WHERE dashboard_f_temperatura > 22

	SELECT dashboard_s_dispositivo
	     , dashboard_i_dispositivo
		 , CONVERT(VARCHAR(20),dashboard_f_temperatura) + ' °C' AS dashboard_s_temperatura
		 , CONVERT(VARCHAR(20),dashboard_f_umidade)		+ ' %'  AS dashboard_s_umidade
		 , dashboard_d_ultimo_sinal							    AS dashboard_d_ultimo_sinal
		 , CASE WHEN (dashboard_d_ultimo_sinal > DATEADD(MILLISECOND,-(dashboard_f_delay),GETDATE()))
		        THEN 'Ativo'
				ELSE 'Inativo'
		   END AS dashboard_s_status
		 , CASE WHEN (dashboard_d_ultimo_sinal > DATEADD(MILLISECOND,-(dashboard_f_delay),GETDATE()))
		        THEN 'primary'
				ELSE 'danger'
		   END AS dashboard_s_cor_borda
		 , dashboard_s_cor_temperatura
		 , dashboard_s_cor_umidade
		 , dashboard_s_icone_temperatura
		 , dashboard_s_icone_umidade
	  FROM #TMP

	DROP TABLE #TMP

SET NOCOUNT ON;
GO

exec sp_Select_Dashboard ''