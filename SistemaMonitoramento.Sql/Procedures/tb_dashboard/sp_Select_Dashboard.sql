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
	     , dispositivo_i_id					
	     , CONVERT(VARCHAR(100),'')					AS dashboard_s_temperatura
         , CONVERT(VARCHAR(100),'')					AS dashboard_s_umidade 
	  INTO #TMP
      FROM tb_dispositivo A(NOLOCK)
     WHERE dispositivo_b_deleted = 0 
	   AND dispositivo_s_status = 'Ativo'

	UPDATE #TMP SET dashboard_s_temperatura = (SELECT CONVERT(VARCHAR(20),(SELECT TOP 5 ROUND(AVG(monitoramento_f_temperatura),2) 
	                                                                         FROM tb_monitoramento 
																		 	WHERE monitoramento_i_dispositivo = dispositivo_i_id)) + ' °C')

	UPDATE #TMP SET dashboard_s_umidade =     (SELECT CONVERT(VARCHAR(20),(SELECT TOP 5 ROUND(AVG(monitoramento_f_umidade),2)     
	                                                                         FROM tb_monitoramento 
																			WHERE monitoramento_i_dispositivo = dispositivo_i_id)) + ' %')

	SELECT *
	  FROM #TMP

	DROP TABLE #TMP

SET NOCOUNT ON;
GO

exec sp_Select_Dashboard ''