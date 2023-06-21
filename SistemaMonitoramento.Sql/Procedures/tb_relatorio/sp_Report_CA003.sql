IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Report_CA003')
    DROP PROCEDURE sp_Report_CA003
GO

CREATE PROCEDURE sp_Report_CA003
(
     @dispositivo_i_dispositivo INT  
    ,@data_inicial              DATE
    ,@data_final                DATE
)

AS
SET NOCOUNT OFF;

	SELECT * 
	  FROM tb_monitoramento A(NOLOCK)
	 INNER
	  JOIN tb_dispositivo B (NOLOCK) ON A.monitoramento_i_dispositivo = B.dispositivo_i_id 
	 WHERE CONVERT(DATE,monitoramento_d_data) BETWEEN @data_inicial AND @data_final
	   AND monitoramento_i_dispositivo = @dispositivo_i_dispositivo
      
SET NOCOUNT ON;
GO

exec sp_Report_CA003 1 , '2023-06-21', '2023-06-21'