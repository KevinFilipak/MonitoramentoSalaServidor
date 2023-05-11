IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_Dashboard')
    DROP PROCEDURE sp_Select_Dashboard
GO

CREATE PROCEDURE sp_Select_Dashboard
(
    @filtro VARCHAR(50) = ''
)
AS
SET NOCOUNT OFF;

    SELECT dispositivo_s_dispositivo	AS dashboard_s_dispositivo
	     , '25 °C'							AS dashboard_s_temperatura
         , '30 %'							AS dashboard_s_umidade 
      FROM tb_dispositivo (NOLOCK)
     WHERE dispositivo_b_deleted = 0 
	   AND dispositivo_s_status = 'Ativo'

SET NOCOUNT ON;
GO
